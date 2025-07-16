import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { Client } from './client-model';

@Injectable({
  providedIn: 'root',
})
export class ViewClientService {
  private http = inject(HttpClient);
  apiUrl = 'https://localhost:7200/api/clients/';
  getClientById(id: number): Observable<Client> {
    return this.http
      .get<Client>(this.apiUrl + id)
      .pipe(tap((response) => console.log('[Client]', response)));
  }
}
