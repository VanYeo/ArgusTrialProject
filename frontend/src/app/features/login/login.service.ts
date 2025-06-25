import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})

export class LoginService {
    constructor(private http: HttpClient) {}
    private baseUrl = 'http://localhost:5143/api/'

    submitLoginData(loginData: { email: string; password: string; }){
        return this.http.post<{token: string}>(this.baseUrl + 'login', loginData);
    }
}