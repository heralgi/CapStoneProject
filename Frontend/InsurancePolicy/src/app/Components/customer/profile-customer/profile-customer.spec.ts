import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileCustomer } from './profile-customer';

describe('ProfileCustomer', () => {
  let component: ProfileCustomer;
  let fixture: ComponentFixture<ProfileCustomer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileCustomer],
    }).compileComponents();

    fixture = TestBed.createComponent(ProfileCustomer);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
