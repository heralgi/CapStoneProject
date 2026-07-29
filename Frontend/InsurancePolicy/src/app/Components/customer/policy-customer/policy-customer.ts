import { Component, OnInit, signal } from '@angular/core';
import { PolicyResponse } from '../../../Models/policy-model';
import { PolicyService } from '../../../services/policy-service';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ClaimRequest } from '../../../Models/claim-model'; // Adjust this path to where your ClaimRequest interface lives
import { ClaimService } from '../../../services/claim-service';

@Component({
  selector: 'app-policy-customer',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './policy-customer.html',
  styleUrl: './policy-customer.css',
})
export class PolicyCustomer implements OnInit {

  policies = signal<PolicyResponse[]>([]); 
  
  // Modal tracking states
  claimForm!: FormGroup;
  selectedPolicy: PolicyResponse | null = null;
  isModalOpen = false;

  constructor(
    private service: PolicyService,
    private fb: FormBuilder, // Injected FormBuilder framework dependency
    private claimService: ClaimService // Injected ClaimService for claim operations
  ) {}

  ngOnInit(): void {
    this.loadPolicies();
    this.initClaimForm();
  }

  // Define reactive form controls with clear safety constraints
  private initClaimForm(): void {
    this.claimForm = this.fb.group({
      policyId: [null, Validators.required],
      claimAmount: [null, [Validators.required, Validators.min(1)]],
      claimReason: ['', [Validators.required, Validators.minLength(10)]],
      incidentDate: ['', Validators.required]
    });
  }

  loadPolicies(): void {
    this.service.getPoliciesByUserId().subscribe({
      next: data => {
        this.policies.set(data);
        console.log(data);
      },
      error: err => console.error(err),
    });
  }

  // Triggers when user clicks 'Raise Claim' in the template
  onRaiseClaim(policy: PolicyResponse): void {
    this.selectedPolicy = policy;
    this.claimForm.reset({
      policyId: policy.policyId, // Auto-binds primary key id mapping safely
      claimAmount: null,
      claimReason: '',
      incidentDate: new Date().toISOString().substring(0, 10) // Set current date as default
    });
    this.isModalOpen = true;
  }

  closeModal(): void {
    this.isModalOpen = false;
    this.selectedPolicy = null;
  }

  // Executed on form submit action trigger
  onSubmitClaim(): void {
    if (this.claimForm.valid) {
      const claimPayload: ClaimRequest = this.claimForm.value;
      
      console.log('Submitting claim mapping sequence payload:', claimPayload);

      // TODO: Connect this to your claim service handler logic pipeline
      // Example:
      this.claimService.raiseClaim(claimPayload).subscribe({
        next: () => this.closeModal(),
        error: (err) => console.error(err)
      });

      this.closeModal(); // Temporary closure handler fallback pass
    } else {
      this.claimForm.markAllAsTouched(); // Force UI validation warning reveals
    }
  }
}
