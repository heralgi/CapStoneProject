import { Component, inject, OnInit, signal } from '@angular/core';
import { UserResponse, UserRole } from '../../Models/user-model';
import { UserService } from '../../services/user-service';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-component',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './user-component.html',
  styleUrl: './user-component.css',
})
export class UserComponent implements OnInit{

  users = signal<UserResponse[]>([]);
  private readonly fb = inject(FormBuilder);
  UserRole = UserRole;
  
  userForm = this.fb.nonNullable.group({
  fullName: ['', [
    Validators.required,
    Validators.minLength(2),
    Validators.maxLength(150)
  ]],

  email: ['', [
    Validators.required,
    Validators.email,
    Validators.maxLength(256)
  ]],

  password: ['', [
    Validators.required,
    Validators.minLength(8),
    Validators.maxLength(100),
    Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$/)
  ]],

  mobileNumber: ['', [
    Validators.required,
    Validators.maxLength(20),
    Validators.pattern(/^[0-9]{10}$/)
  ]],

  role: [UserRole.Customer, Validators.required]
});
  constructor(private service: UserService) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.service.getAll().subscribe({
      next: data => {
        this.users.set(data);
      },
      error: err => console.error(err)
    });
  }

  onSubmit(): void{
    if (this.userForm.invalid)
      return;
console.log(this.userForm.getRawValue());
    this.service.postUser(this.userForm.getRawValue()).subscribe({
      next: data=>{
        this.loadUsers();
      },
      error: err=> console.error(err)
      
    })
  }

}
