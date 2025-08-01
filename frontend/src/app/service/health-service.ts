import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HealthService {
  private readonly healthUrl = 'https://localhost:7200/api/health';

  constructor(private http: HttpClient) { }

  checkHealth(): Observable<string> {
    return this.http.get(this.healthUrl, { responseType: 'text' });
  }
}
