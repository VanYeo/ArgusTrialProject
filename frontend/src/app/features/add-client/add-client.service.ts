import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { Client } from '../view-client/client-model';

// export interface Client {
//   accountNumber?: number;
//   companyName: string;
//   phone: string;
//   mobile: string;
//   contact: string;
//   tradingName: string;
//   loginEmail: string;
//   accountName: string;
//   accountEmail: string;
//   accountEmailBill: string;
//   accountPhone: string;
//   accountCompanyName: string;
//   accountManager: string;
//   billingAddress: Address;
//   deliveryAddress: Address;
//   startDate: string;
//   contractTerm: string;
//   customValue: number;
//   roadRedPlan: string;
//   iotPlan: string;
//   softwarePlan: string;
//   notes: string;
//   [key: string]: any;
// }

// export interface Address {
//   street: string;
//   city: string;
//   state: string;
//   zip: string;
//   country: string;
// }

@Injectable({
  providedIn: 'root',
})
export class AddClientService {
  private baseUrl = 'https://localhost:7200/api/clients/'; // Replace with your backend endpoint

  private http = inject(HttpClient);

  /**
   * POST: Add a new client
   */
  addClient(client: Client): Observable<Client> {
    return this.http.post<Client>(this.baseUrl + 'create', client).pipe(
      tap((response) => {
        console.log('[AddClient Success]', response);
      }),
      catchError((error) => {
        console.error('[AddClient Error]', error);
        return throwError(() => error);
      })
    );
  }

  updateClient(clientData: any): Observable<any> {
    return this.http.put(
      `${this.baseUrl}${clientData.clientID}`,
      clientData
    );
  }

  /**
   * GET: Fetch a newly generated account number from backend
   */
  getNewAccountNumber(): Observable<string> {
    return this.http.get<string>(`${this.baseUrl}new-clientID`);
  }
}
