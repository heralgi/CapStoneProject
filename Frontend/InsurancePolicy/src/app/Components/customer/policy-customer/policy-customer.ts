import { Component, OnInit, signal } from '@angular/core';
import { PolicyResponse } from '../../../Models/policy-model';
import { PolicyService } from '../../../services/policy-service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-policy-customer',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './policy-customer.html',
  styleUrl: './policy-customer.css',
})
export class PolicyCustomer implements OnInit{

  policies = signal<PolicyResponse[]>([]); 

  constructor(private service: PolicyService){

  }

  ngOnInit(): void {
    this.loadPolicies();
  }

  loadPolicies(): void{
    this.service.getPoliciesByUserId().subscribe({
      next: data => {
        this.policies.set(data);
        console.log(data);
      },
      error: err => console.error(err),
    });
  }
}
