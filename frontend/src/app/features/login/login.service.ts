import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

export interface LoginResponse {
    jwtToken: string;
    email: string;
}

@Injectable({
    providedIn: 'root'
})

export class LoginService {
    constructor(private http: HttpClient) {}
    private baseUrl = 'https://localhost:7200/api/'

    submitLoginData(loginData: { email: string; password: string; }){
        return this.http.post<LoginResponse>(this.baseUrl + 'login', loginData);
    }
}