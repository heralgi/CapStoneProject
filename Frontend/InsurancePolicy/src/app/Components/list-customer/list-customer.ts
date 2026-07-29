import { Component, OnInit, signal } from '@angular/core';
import { CustomerService } from '../../services/customer-service';
import { DatePipe } from '@angular/common';
import { CustomerResponse } from '../../Models/Customer-model';

@Component({
  selector: 'app-list-customer',
  imports: [DatePipe],
  templateUrl: './list-customer.html',
  styleUrl: './list-customer.css',
})
export class ListCustomer implements OnInit {
onViewCustomer(_t20: any) {
throw new Error('Method not implemented.');
}
  ngOnInit(): void {
    this.loadCustomers();
  }

  customers = signal<CustomerResponse[]>([]);

  constructor(private customerService: CustomerService) {}
  
  loadCustomers():void {
    this.customerService.getAll().subscribe(
      (data: CustomerResponse[]) => {
        this.customers.set(data);
      },
      (error) => {
        console.error('Error fetching customers:', error);
      }
    );
  }
}


