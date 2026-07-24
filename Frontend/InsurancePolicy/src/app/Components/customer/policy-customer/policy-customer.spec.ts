import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PolicyCustomer } from './policy-customer';

describe('PolicyCustomer', () => {
  let component: PolicyCustomer;
  let fixture: ComponentFixture<PolicyCustomer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PolicyCustomer],
    }).compileComponents();

    fixture = TestBed.createComponent(PolicyCustomer);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
