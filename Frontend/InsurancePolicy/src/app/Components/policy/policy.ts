import { Component, inject, OnInit, signal } from '@angular/core';
import { PolicyService } from '../../services/policy-service';
import { PolicyResponse } from '../../Models/policy-model';
import { CommonModule } from '@angular/common';
import { PaymentMode, PaymentStatus, PolicyPaymentResponse } from '../../Models/payment-model';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { PaymentsService } from '../../services/payment-service';

@Component({
  selector: 'app-policy',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './policy.html',
  styleUrl: './policy.css',
})
export class Policy implements OnInit{

  private paymentService = inject(PaymentsService);
  
  protected errorMessage = signal<string | null>(null);
  policies = signal<PolicyResponse[]>([]);
  toggleTable = signal<boolean>(false);
  protected paymentHistory = signal<PolicyPaymentResponse[]>([]);


    // Expose Enums arrays to the template structure safely
  protected paymentModes = Object.values(PaymentMode).filter(v => typeof v === 'number');
  protected paymentStatuses = Object.values(PaymentStatus).filter(v => typeof v === 'number');
  
  private fb = inject(FormBuilder);

  constructor(private service: PolicyService) { }

    protected paymentForm = this.fb.nonNullable.group({
    policyId: [0, [Validators.required, Validators.min(1)]],
    amount: [0.00, [Validators.required, Validators.min(0.01)]],
    paymentMode: [PaymentMode.Cash, [Validators.required]],
    transactionReference: ['', [Validators.required, Validators.maxLength(100)]],
    paymentDate: [new Date().toISOString().substring(0, 16), [Validators.required]],
    paymentStatus: [PaymentStatus.Pending, [Validators.required]]
  });

  ngOnInit(): void {
    this.loadPolicies();
  }

  loadPolicies(): void {
    this.service.getPolicies().subscribe({
      next: data => {
        this.policies.set(data);
      },
      error: err => console.error(err)
    });
  }

  pay():void{
    this.toggleTable.update(value => !value);
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
