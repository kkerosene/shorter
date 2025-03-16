import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  // Updated API URL with 'api' segment:
  private apiUrl: string = 'https://localhost:7161/api/Url';

  constructor(private http: HttpClient) { }

  getUrls(): Observable<any> {
    return this.http.get(this.apiUrl);
  }

  createUrl(url: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/CreateUrl`, url);
  }
}
