import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  type: string = "password"
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash";

  loginForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
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

  onSubmit() {
    if (!this.loginForm.valid) {
      return;
    }

    this.authService.login(this.loginForm.value).subscribe((res => {
      if (res.success) {
        alert(res.message);
        this.loginForm.reset();
        this.router.navigate(['dashboard'])
      }
    }))
  }

}
