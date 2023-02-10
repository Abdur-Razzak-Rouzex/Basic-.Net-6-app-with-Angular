import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl: string = "https://localhost:7237/api/User/";

  constructor(private http: HttpClient) { }

  signUp(signupInfo: any) {
    return this.http.post<any>(`${this.baseUrl}register`, signupInfo)
  }

  login(loginInfo: any) {
    return this.http.post<any>(`${this.baseUrl}authenticate`, loginInfo)
  }
}
