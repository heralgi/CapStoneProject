import { Component, inject, OnInit, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs';

import { PlanResponse } from '../../../Models/Plan';
import { PlanService } from '../../../services/plan';
import { PolicyService } from '../../../services/policy-service';
import { CustomerPolicyPurchaseRequest } from '../../../Models/policy-model';

@Component({
  selector: 'app-plan-customer',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './plan-customer.html',
  styleUrl: './plan-customer.css',
})
export class PlanCustomer implements OnInit {
  // Services injected cleanly using the standard inject() function
  private readonly planService = inject(PlanService);
  private readonly policyService = inject(PolicyService);
  private readonly route = inject(ActivatedRoute);
  private readonly fb = inject(FormBuilder);

  // Constants & Configuration
  protected readonly premiumTypeLabels = ['One-Time', 'Annual'];
  private readonly eitherAadhaarOrVehicleRegex = /^\d{12}$|^[A-Z]{2}\d{2}[A-Z]{1,2}\d{4}$/;

  // State Management using Signals
  private readonly plansSignal = signal<PlanResponse[]>([]);
  protected readonly isTableHidden = signal<boolean>(false);
  protected readonly selectedPlanId = signal<number | null>(null);

  // Memoized derivation to keep template logical filtering out of the raw state
  protected readonly activePlans = computed(() => 
    this.plansSignal().filter(plan => plan.isActive)
  );

  // Form Initialized with camelCase naming convention matching the updated template
  protected readonly purchaseForm = this.fb.nonNullable.group({
    identifier: ['', [
      Validators.required, 
      Validators.pattern(this.eitherAadhaarOrVehicleRegex)
    ]]
  });

  // Short getter clean access in template validation blocks
  protected get identifierControl() {
    return this.purchaseForm.controls.identifier;
  }

  ngOnInit(): void {
    const productId = Number(this.route.snapshot.paramMap.get('id'));
    if (productId) {
      this.loadPlans(productId);
    }
  }

  private loadPlans(productId: number): void {
    this.planService.getByProduct(productId)
      .pipe(take(1)) // Auto-unsubscribes to prevent memory leaks
      .subscribe({
        next: (plans) => this.plansSignal.set(plans),
        error: (err) => console.error('Failed to load plans:', err)
      });
  }

  protected onSelectPlan(planId: number): void {
    this.selectedPlanId.set(planId);
    this.toggleViewState();
  }

  protected onPurchaseSubmit(): void {
    const planId = this.selectedPlanId();
    if (this.purchaseForm.invalid || !planId) {
      this.purchaseForm.markAllAsTouched();
      return;
    }

    const purchaseRequest: CustomerPolicyPurchaseRequest = {
      planId: planId,
      identifier: this.identifierControl.value,
      startDate: new Date().toISOString().split('T')[0]
    };

    this.policyService.purchase(purchaseRequest)
      .pipe(take(1))
      .subscribe({
        next: (response) => {
          // Pro tip: Add user notification toast or navigation rules here
          this.purchaseForm.reset();
          this.toggleViewState();
        },
        error: (err) => console.error('Purchase processing failed:', err)
      });
  }

  protected onCancelPurchase(): void {
    this.purchaseForm.reset();
    this.toggleViewState();
  }

  private toggleViewState(): void {
    this.isTableHidden.update(hidden => !hidden);
  }
}
