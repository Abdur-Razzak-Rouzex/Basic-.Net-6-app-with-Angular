import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

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

  constructor(private fb: FormBuilder) { }

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
    console.log('test');

  }

}
