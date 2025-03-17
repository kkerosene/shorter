import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Shortlink } from './shortlinks/shortlinks.component'; // Import the interface

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl: string = 'https://localhost:7161/api/Url';

  constructor(private http: HttpClient) { }

  getUrls(): Observable<any> {
    return this.http.get(this.apiUrl, { withCredentials: true });
  }

  deleteUrl(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`, { withCredentials: true });
  }
}
