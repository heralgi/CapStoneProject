import { Component, OnInit, signal, computed, inject } from '@angular/core';
import { ClaimResponse, ClaimReviewRequest } from '../../Models/claim-model';
import { ClaimService } from '../../services/claim-service';
import { AuthService } from '../../services/auth';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface ReviewFormValues {
  remarks: string;
}

@Component({
  selector: 'app-claim',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './claim.html',
  styleUrl: './claim.css',
})
export class Claim implements OnInit {
  private readonly service = inject(ClaimService);
  private readonly authService = inject(AuthService);

  // Reactive primary dataset mapping
  public readonly claims = signal<ClaimResponse[]>([]);

  // Roles processing pipeline matching the session payload
  public readonly currentUserRole = computed<string | null>(() => {
    if (!this.authService.isAuthenticated()) return null;
    return this.authService.getRole();
  });

  public readonly isAdminLoggedIn = computed<boolean>(() => this.currentUserRole() === 'Admin');
  public readonly isInternalStaffLoggedIn = computed<boolean>(() => this.currentUserRole() === 'InternalStaff');

  // Overlay control signals
  public readonly isActionModalOpen = signal<boolean>(false);
  public readonly selectedClaim = signal<ClaimResponse | null>(null);

  ngOnInit(): void {
    this.loadClaims();
  }

  loadClaims(): void {
    this.service.getAllClaims().subscribe({
      next: data => {
        this.claims.set(data);
      },
      error: err => console.error('Failed to load global data registry:', err)
    });
  }

  // Direct Admin Action Endpoint Strategy mapping `approveClaim`
  approve(p: ClaimResponse): void {
    if (!this.isAdminLoggedIn()) return;

    // Direct mapping to the specific claim ID primary key property (assuming id or claimId maps from claimNumber)
    // Note: If your back-end database explicitly uses a dedicated numeric identifier mapping, substitute p.id below.
    const numericId = parseInt(p.claimNumber.replace(/\D/g, ''), 10) || (p as any).id || (p as any).claimId;

    this.service.approveClaim(p.claimId).subscribe({
      next: (updatedRecord) => {
        this.updateLocalState(p.claimNumber, updatedRecord);
      },
      error: (err) => console.error(`Admin approval operation failed for claim ${p.claimNumber}:`, err)
    });
  }

  // Direct Admin Action Endpoint Strategy mapping `rejectClaim`
  reject(p: ClaimResponse): void {
    if (!this.isAdminLoggedIn()) return;

    const numericId = parseInt(p.claimNumber.replace(/\D/g, ''), 10) || (p as any).id || (p as any).claimId;

    this.service.rejectClaim(p.claimId).subscribe({
      next: (updatedRecord) => {
        this.updateLocalState(p.claimNumber, updatedRecord);
      },
      error: (err) => console.error(`Admin rejection operation failed for claim ${p.claimNumber}:`, err)
    });
  }

  // Modal Launcher utility for internal review personnel
  onTakeAction(p: ClaimResponse): void {
    if (!this.isInternalStaffLoggedIn()) {
      console.warn('Action denied: Internal staff credential mismatch error.');
      return;
    }
    this.selectedClaim.set({ ...p });
    this.isActionModalOpen.set(true);
  }

  closeActionModal(): void {
    this.isActionModalOpen.set(false);
    this.selectedClaim.set(null);
  }

  // Modal Submission pipeline utilizing `reviewClaim` API route
  submitStaffReview(formValues: ReviewFormValues): void {
    const activeClaim = this.selectedClaim();
    if (!activeClaim) return;

    const numericId = parseInt(activeClaim.claimNumber.replace(/\D/g, ''), 10) || (activeClaim as any).id || (activeClaim as any).claimId;

    // Structuring payload strictly matching the expected backend model structure
    const reviewRequestPayload: ClaimReviewRequest = {
      recommendedStatus: 1, // Programmatically locked fixed status sequence
      remarks: formValues.remarks
    };

    this.service.reviewClaim(activeClaim.claimId, reviewRequestPayload).subscribe({
      next: (updatedRecord) => {
        this.updateLocalState(activeClaim.claimNumber, updatedRecord);
        this.closeActionModal();
      },
      error: (err) => console.error(`Staff workflow processing failed for claim ${activeClaim.claimNumber}:`, err)
    });
  }

  // Helper method keeping UI mutations DRY and deterministic
  private updateLocalState(claimNumber: string, updatedRecord: ClaimResponse): void {
    this.claims.update((currentClaims) =>
      currentClaims.map((item) => item.claimNumber === claimNumber ? updatedRecord : item)
    );
  }
}
