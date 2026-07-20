import { Component, inject, OnInit, signal } from '@angular/core';
import { ProductService } from '../../services/product-service';
import { ProductResponse, ProductType, ProductRequest } from '../../Models/Product';
import { FormBuilder, ReactiveFormsModule, Validators, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './products.html',
  styleUrls: ['./products.css']
})
export class Products implements OnInit {

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

  create(): void {

    if (this.form.invalid)
      return;

    if (this.isEditMode){
      this.update();
      return;
    }
      
    this.service.add(this.form.getRawValue()).subscribe({

      next: () => {

        alert('Product added successfully.');

        this.form.reset({
          productName: '',
          productType: ProductType.Health,
          description: '',
          isActive: true
        });

        this.loadProducts();

      },

      error: err => console.error(err)

    });

  }

  edit(product: ProductResponse): void {

    this.isEditMode = true;

    this.productId = product.productId;

    this.form.patchValue({

      productName: product.productName,

      productType: ProductType[product.productType as keyof typeof ProductType],

      description: product.description,

      isActive: product.isActive

    });

  }

  update(): void {

    if (this.form.invalid)
      return;
    console.log(this.form.getRawValue());
    this.service.update(this.productId, this.form.getRawValue()).subscribe({

      next: () => {

        alert('Product updated successfully.');

        this.isEditMode = false;

        this.productId = 0;

        this.form.reset({

          productName: '',
          productType: ProductType.Health,
          description: '',
          isActive: true

        });

        this.loadProducts();

      },

      error: err => console.error(err)

    });

  }

  delete(id: number): void {

    if (!confirm('Deactivate Product?'))
      return;

    this.service.deactivate(id).subscribe({

      next: () => {

        this.loadProducts();

      },

      error: err => console.error(err)

    });

  }
  resetForm() {

    this.editingId = null;

    this.form.reset({

      productName: '',
      productType: 0,
      description: '',
      isActive: true

    });

  }

}