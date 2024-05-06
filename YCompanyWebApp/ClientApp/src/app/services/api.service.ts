import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  baseUrl = 'https://localhost:';

  constructor(private http: HttpClient) {}

  getAllPolicies() {
    return this.http.get<any>(this.baseUrl + '7143/allPolicies');
  }

  getAllVendors() {
    return this.http.get<any>(this.baseUrl + '7033/allVendors');
  }
}
