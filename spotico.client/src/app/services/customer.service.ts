import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user.model';
import { Observable } from 'rxjs';
import { UserDTO } from '../DTOs/user.dto';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private apiUrl = 'http://localhost:5032/api/customer'; // URL to backend API

  constructor(
    private http: HttpClient
  ) { }

  getCustomer(id: string) : Observable<User> {
    // Send a GET request to API to get the customer by ID
    return this.http.get<User>(`${this.apiUrl}/${id}`);
  }

  postCustomer(customer: UserDTO) : Observable<void> {
    // Send a POST request to API to create a new customer
    return this.http.post<void>(this.apiUrl, customer);
  }

  deleteCustomer(id: string) : Observable<void> {
    // Send a DELETE request to API to delete the customer by ID
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
