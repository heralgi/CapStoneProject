import { Component, inject } from '@angular/core';
import { ProductService } from '../../services/product-service';
import { ProductResponse, ProductType } from '../../Models/Product';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products',
  imports: [ CommonModule, ReactiveFormsModule],
  templateUrl: './products.html',
  styleUrl: './products.css',
})
export class Products {
  products: ProductResponse[] = [];
  private readonly router = inject(Router);
  
  private readonly fb = inject(FormBuilder);
  form = this.fb.nonNullable.group({

    productName: ['', Validators.required],

    productType: [ProductType.Health],

    description: ['', Validators.required],

    isActive: [true]

});

constructor(private service: ProductService) {}

ngOnInit(): void {

  this.loadProducts();

}

loadProducts() {

  this.service.getAll().subscribe({

    next: data => {

      this.products = data;

    }

  });

}
delete(id: number) {

  if (!confirm("Deactivate Product?"))
    return;

  this.service.deactivate(id).subscribe({

    next: () => {

      this.loadProducts();

    }

  });

}
create() {

  if (this.form.invalid)
    return;

  this.service.add(this.form.getRawValue())
      .subscribe({

          next: () => {

              this.router.navigate(['/admin/products']);

          }

      });

}
update() {

  if (this.form.invalid)
      return;

  this.service.update(this.productId, this.form.getRawValue())
      .subscribe({

          next: () => {

              this.router.navigate(['/admin/products']);

          }

      });

}
}
