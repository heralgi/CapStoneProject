import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanCustomer } from './plan-customer';

describe('PlanCustomer', () => {
  let component: PlanCustomer;
  let fixture: ComponentFixture<PlanCustomer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PlanCustomer],
    }).compileComponents();

    fixture = TestBed.createComponent(PlanCustomer);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
