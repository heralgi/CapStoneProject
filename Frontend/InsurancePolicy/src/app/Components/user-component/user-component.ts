import { Component, inject, OnInit, signal } from '@angular/core';
import { UpdateUserRequest, UserResponse, UserRole } from '../../Models/user-model';
import { UserService } from '../../services/user-service';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { disabled } from '@angular/forms/signals';

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
  isEditMode = signal<boolean>(false);
  currentUserId = -1;
  
  userForm = this.fb.nonNullable.group({
  fullName: ['', [
    Validators.required,
    Validators.minLength(2),
    Validators.maxLength(150)
  ]],

  email: ['admin@example.com', [
    Validators.required,
    Validators.email,
    Validators.maxLength(256)
  ]],

  password: ['Admin@123', [
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

  role: [UserRole.Admin, Validators.required]
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
    
    if(!this.isEditMode()){
      if (this.userForm.invalid)
      return;
      this.service.postUser(this.userForm.getRawValue()).subscribe({
      next: data=>{
        this.loadUsers();
      },
      error: err=> console.error(err)
    })
    }
    else{
      this.service.putUser(this.currentUserId,this.userForm.value as UpdateUserRequest).subscribe({
      next: data=>{
        this.loadUsers();
      },
      error: err=> console.error(err)
    })
    }
    
  }

  edit(user: UserResponse): void{
    this.isEditMode.set(true);
    this.userForm.controls.email.disable();
    this.userForm.controls.email.setValue('');
    this.userForm.controls.password.disable();
    this.userForm.controls.password.setValue('');

    this.userForm.controls.fullName.patchValue(user.fullName);
    this.userForm.controls.mobileNumber.patchValue(user.mobileNumber);
    this.userForm.controls.role.patchValue(UserRole[user.role as keyof typeof UserRole]);
    console.log(this.userForm.value , user);

    this.currentUserId = user.userId;
  }

  deactivate(userId: Number): void{

  }

  reset(): void{
    this.isEditMode.set(false);
  }

}
