import { Component, OnInit, signal } from '@angular/core';
import { ClaimResponse } from '../../../Models/claim-model';
import { ClaimService } from '../../../services/claim-service';
import { CurrencyPipe, DatePipe } from '@angular/common';

@Component({
  selector: 'app-claim-customer',
  imports: [CurrencyPipe, DatePipe],
  templateUrl: './claim-customer.html',
  styleUrl: './claim-customer.css',
})
export class ClaimCustomer implements OnInit {
  claims = signal<ClaimResponse[]>([]);

  constructor(private service: ClaimService) {}

  ngOnInit(): void {
    this.loadClaims();  
  }

  loadClaims(): void {
    this.service.getClaims().subscribe({
      next: (response) => {
        this.claims.set(response);
      },
      error: (error) => {
        console.error('Error fetching claims:', error);
      }
    });
  }
}
