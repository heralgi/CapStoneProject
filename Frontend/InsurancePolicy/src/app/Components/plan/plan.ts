import { Component, inject, OnInit, signal } from '@angular/core';
import { PlanResponse } from '../../Models/Plan';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { PlanService } from '../../services/plan';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../services/product-service';
import { ProductResponse } from '../../Models/Product';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-plan',
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './plan.html',
  styleUrl: './plan.css',
})
export class Plan implements OnInit {
   plans = signal<PlanResponse[]>([]);
   Products = signal<ProductResponse[]>([]);
   auth = inject(AuthService);
   premiumTypeLabels = ['One-Time', 'Annual'];

  isEditMode = false;

  editId = 0;
private readonly router = inject(Router);
  private readonly fb = inject(FormBuilder);

  selectedProduct: ProductResponse | undefined;
  selectedProduct1: number = 1;
constructor(private service: PlanService, private proService: ProductService) { }

  form = this.fb.nonNullable.group({

    productId: [1, Validators.required],

    planName: ['', Validators.required],

    coverageAmount: [0, Validators.required],

    premiumAmount: [0, Validators.required],

    premiumType: [0, Validators.required],

    durationYears: [1, Validators.required],

    termsAndConditions: ['', Validators.required],

    isActive: [true]

  });

  ngOnInit(): void {

    this.loadPlans();
console.log(this.auth.getRole());
  }

  loadPlans() {

    const productId = this.form.controls.productId.value;

    this.proService.getAll().subscribe({

      next: data => {

        this.Products.set(data);

      },

      error: err => console.error(err)

    });
    this.service.getByProduct(productId).subscribe({

      next: data => {

        this.plans.set(data);

      },

      error: err => console.error(err)

    });

  }

  create() {

    if (this.form.invalid)
      return;
    console.log(this.form.getRawValue());
    this.service.add(this.form.getRawValue())
      .subscribe({

        next: () => {

          this.loadPlans();

          this.reset();

        }

      });

  }

  edit(plan: PlanResponse) {

    this.editId = plan.planId;

    this.isEditMode = true;

    this.form.patchValue({

      productId: plan.productId,

      planName: plan.planName,
      coverageAmount: plan.coverageAmount,
      premiumAmount: plan.premiumAmount,
      durationYears: plan.durationYears,
      premiumType: plan.premiumType,
      termsAndConditions: plan.termsAndConditions,

      isActive: plan.isActive

    });

  }

  update() {

    this.service.update(
      this.editId,
      this.form.getRawValue()
    ).subscribe({

      next: () => {

        this.loadPlans();

        this.reset();

      }

    });

  }

  deactivate(id: number) {

    if (!confirm('Deactivate this plan?'))
      return;

    this.service.deactivate(id)
      .subscribe(() => this.loadPlans());

  }

  reset() {

    this.isEditMode = false;

    this.editId = 0;

    this.form.reset({

      productId: 1,

      planName: '',

      coverageAmount: 0,

      premiumAmount: 0,

      premiumType: 0,

      durationYears: 1,

      termsAndConditions: '',

      isActive: true

    });

  }
  onChange(){
    this.service.getByProduct(this.selectedProduct1).subscribe({

      next: data => {

        this.plans.set(data);

      },

      error: err => console.error(err)

    });
  }

}

