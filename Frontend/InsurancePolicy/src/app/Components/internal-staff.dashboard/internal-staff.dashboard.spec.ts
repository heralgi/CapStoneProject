import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InternalStaffDashboard } from './internal-staff.dashboard';

describe('InternalStaffDashboard', () => {
  let component: InternalStaffDashboard;
  let fixture: ComponentFixture<InternalStaffDashboard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InternalStaffDashboard],
    }).compileComponents();

    fixture = TestBed.createComponent(InternalStaffDashboard);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
