import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user.model';
import { Observable } from 'rxjs';
import { UserDTO } from '../DTOs/user.dto';

const API_URL = 'http://localhost:5032/api/customer';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  constructor(private http: HttpClient) {}

  getCustomer(id: string): Observable<User> {
    // Send a GET request to API to get the customer by ID
    return this.http.get<User>(`${API_URL}/${id}`);
  }

  postCustomer(customer: UserDTO): Observable<void> {
    // Send a POST request to API to create a new customer
    return this.http.post<void>(API_URL, customer);
  }

  deleteCustomer(id: string): Observable<void> {
    // Send a DELETE request to API to delete the customer by ID
    return this.http.delete<void>(`${API_URL}/${id}`);
  }
}
