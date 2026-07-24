import { Component, inject, OnInit, signal } from '@angular/core';
import { PaymentsService } from '../../services/payment-service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { PaymentMode, PaymentStatus, PolicyPaymentResponse } from '../../Models/payment-model';
import { CommonModule, NgClass } from '@angular/common';

@Component({
  selector: 'app-payment',
  imports: [CommonModule, NgClass, ReactiveFormsModule],
  templateUrl: './payment.html',
  styleUrl: './payment.css',
})
export class Payment implements OnInit{

  private paymentService = inject(PaymentsService);
  private fb = inject(FormBuilder);

  // Reactive state management signals
  protected paymentHistory = signal<PolicyPaymentResponse[]>([]);
  protected isLoading = signal<boolean>(false);
  protected errorMessage = signal<string | null>(null);

  // Expose Enums arrays to the template structure safely
  protected paymentModes = Object.values(PaymentMode).filter(v => typeof v === 'number');
  protected paymentStatuses = Object.values(PaymentStatus).filter(v => typeof v === 'number');

  // Form mapping explicitly built matching your .NET Validation annotations
  protected paymentForm = this.fb.nonNullable.group({
    policyId: [0, [Validators.required, Validators.min(1)]],
    amount: [0.00, [Validators.required, Validators.min(0.01)]],
    paymentMode: [PaymentMode.Cash, [Validators.required]],
    transactionReference: ['', [Validators.required, Validators.maxLength(100)]],
    paymentDate: [new Date().toISOString().substring(0, 16), [Validators.required]],
    paymentStatus: [PaymentStatus.Pending, [Validators.required]]
  });

  ngOnInit(): void {
    this.loadHistory();
  }

  loadHistory(): void {
    this.isLoading.set(true);
    this.paymentService.getPaymentHistory().subscribe({
      next: history => {
        this.paymentHistory.set(history);
        this.isLoading.set(false);
      },
      error: (err) => {
        this.errorMessage.set('Failed to load history context. Ensure you are authenticated.');
        this.isLoading.set(false);
      }
    });
  }

  submitPayment(): void {
    if (this.paymentForm.invalid) {
      this.paymentForm.markAllAsTouched();
      return;
    }

    const formRaw = this.paymentForm.getRawValue();
    
    // Format JSON model perfectly to match your exact backend contracts
    const requestPayload = {
      ...formRaw,
      paymentDate: new Date(formRaw.paymentDate).toISOString()
    };

    this.paymentService.recordPayment(requestPayload).subscribe({
      next: (newPayment) => {
        // Optimistically update your locally rendered signal array immediately
        this.paymentHistory.update(current => [newPayment, ...current]);
        this.paymentForm.reset({ paymentDate: new Date().toISOString().substring(0, 16) });
        this.errorMessage.set(null);
      },
      error: () => this.errorMessage.set('Could not record your premium transaction profile.')
    });
}
}