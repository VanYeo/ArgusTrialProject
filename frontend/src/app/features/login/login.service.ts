import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})

export class LoginService {
    constructor(private http: HttpClient) {}
    private baseUrl = 'http://localhost:5143/api/'

    submitLoginData(loginData: { email: string; password: string; }){
        return this.http.post(this.baseUrl + 'login', loginData, {
            responseType: 'text' as 'json'
        });
    }
}