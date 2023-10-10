import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  private baseUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAll(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/clients`);
  }

  getById(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/clients/${id}`);
  }

  add(request: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/clients`, request);
  }

  update(request: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/clients`, request);
  }

  delete(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/clients/${id}`);
  }
}
