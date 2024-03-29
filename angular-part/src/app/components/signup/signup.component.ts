import { AuthService } from './../../services/auth.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  type: string = "password"
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash";

  signUpForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.signUpForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  hideShowPass() {
    this.isText = !this.isText;

    if (this.isText) {
      this.eyeIcon = "fa-eye";
      this.type = "text"
    } else {
      this.eyeIcon = "fa-eye-slash";
      this.type = "password"
    }
  }

  onSignUp() {
    if (!this.signUpForm.valid) {
      return;
    }

    this.authService.signUp(this.signUpForm.value).subscribe((res => {
      if (res.success) {
        alert(res.message);
        this.signUpForm.reset();
        this.router.navigate(['login'])
      }
    }))
  }

}
