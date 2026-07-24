import { Component, inject, OnInit, signal } from '@angular/core';
import { ProductResponse, ProductType } from '../../../Models/Product';
import { Router } from '@angular/router';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductService } from '../../../services/product-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-component',
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './product-component.html',
  styleUrl: './product-component.css',
})
export class ProductComponent implements OnInit{

  products = signal<ProductResponse[]>([]);

  productId = 0;
  editingId: number | null = null;

  isEditMode = false;

  ProductType = ProductType;

  private readonly router = inject(Router);
  private readonly fb = inject(FormBuilder);

  form = this.fb.nonNullable.group({

    productName: ['', Validators.required],

    productType: [ProductType.Health, Validators.required],

    description: ['', Validators.required],

    isActive: [true]

  });

  constructor(private service: ProductService) { }

  ngOnInit(): void {

    this.loadProducts();

  }

  loadProducts(): void {

    this.service.getAll().subscribe({

      next: data => {

        this.products.set(data);

      },

      error: err => console.error(err)

    });

  }

  viewPlan(productId: Number): void{
    this.router.navigate(['/dashboard/plans', productId]);
  }
}
