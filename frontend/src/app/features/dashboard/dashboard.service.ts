import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  constructor(private http: HttpClient) {}
  private baseUrl = 'https://localhost:7200/api/clients';

  searchClients(searchPayload: {
    keyword: string,
    selectedFields: string[],
    sortBy: string,
    sortDirection: string,
    pageIndex: number,
    pageSize: number
  }) {

    return this.http.post(`${this.baseUrl}`, searchPayload);
  }
}
