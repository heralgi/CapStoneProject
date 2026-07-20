import { Component, OnInit, signal } from '@angular/core';
import { ClaimResponse } from '../../Models/claim-model';
import { ClaimService } from '../../services/claim-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-claim',
  imports: [CommonModule],
  templateUrl: './claim.html',
  styleUrl: './claim.css',
})
export class Claim implements OnInit{

  claims = signal<ClaimResponse[]>([]);

  constructor(private service: ClaimService) { }

  ngOnInit(): void {
    this.loadClaims();
  }

  loadClaims(): void{
    this.service.getAllClaims().subscribe({
      next: data => {
        this.claims.set(data);
      },
      error: err => console.error(err)
    });
  }
}
