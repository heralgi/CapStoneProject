import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClaimCustomer } from './claim-customer';

describe('ClaimCustomer', () => {
  let component: ClaimCustomer;
  let fixture: ComponentFixture<ClaimCustomer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClaimCustomer],
    }).compileComponents();

    fixture = TestBed.createComponent(ClaimCustomer);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
