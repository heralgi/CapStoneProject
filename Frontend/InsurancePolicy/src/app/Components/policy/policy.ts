import { Component, OnInit, signal } from '@angular/core';
import { PolicyService } from '../../services/policy-service';
import { PolicyResponse } from '../../Models/policy-model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-policy',
  imports: [CommonModule],
  templateUrl: './policy.html',
  styleUrl: './policy.css',
})
export class Policy implements OnInit{

  policies = signal<PolicyResponse[]>([]);
  constructor(private service: PolicyService) { }

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




}
