import { Component, inject, OnInit, signal } from '@angular/core';
import { PlanResponse } from '../../../Models/Plan';
import { PlanService } from '../../../services/plan';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { PolicyService } from '../../../services/policy-service';
import { CustomerPolicyPurchaseRequest } from '../../../Models/policy-model';

@Component({
  selector: 'app-plan-customer',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './plan-customer.html',
  styleUrl: './plan-customer.css',
})
export class PlanCustomer implements OnInit{
  plans = signal<PlanResponse[]>([]);  
  premiumTypeLabels = ['One-Time', 'Annual'];
  toggleTable = signal<boolean>(false);
  private readonly fb = inject(FormBuilder);
  currentPlanId = 1;
  public purchaseRequest!: CustomerPolicyPurchaseRequest;

  eitherAadhaarOrVehicleRegex = /^\d{12}$|^[A-Z]{2}\d{2}[A-Z]{1,2}\d{4}$/;

    purchaseform = this.fb.nonNullable.group({
      identifier: ['', [
        Validators.required, 
        Validators.pattern(this.eitherAadhaarOrVehicleRegex) // Accepts either format
      ]]
    });


  constructor(private service: PlanService,private policyService: PolicyService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const productId: number = Number(this.route.snapshot.paramMap.get('id'));
    this.loadPlans(productId);

  }

  loadPlans(productId: number) {
    this.service.getByProduct(productId).subscribe({

      next: data => {

        this.plans.set(data);

      },

      error: err => console.error(err)

    });

  }

  purchaseForm(planId: number): void{
    this.currentPlanId = planId;
    this.toggle();
  }

  purchase(){
    this.purchaseRequest = {
      planId: this.currentPlanId,
      identifier: this.purchaseform.controls.identifier.value, 
      startDate: new Date().toISOString().split('T')[0] // Safely generates today's date string
    };
    this.policyService.purchase(this.purchaseRequest).subscribe({
      next: data=>{
        
      },
      error: err=> console.error(err),
    });
    this.toggle();
  }

  toggle(): void{
    // .update() flips true to false and false to true in a single line
  this.toggleTable.update(value => !value);
  }
}
