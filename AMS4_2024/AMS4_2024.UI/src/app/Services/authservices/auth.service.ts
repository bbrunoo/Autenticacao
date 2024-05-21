import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable, map} from 'rxjs';
import { Credentials } from '../../Models/credentials.model';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl = 'https://localhost:7123/api/Auth'

  constructor(private http:HttpClient){}

  register(user: any): Observable<any>
  {
    return this.http.post(`${this.apiUrl}/register`, user);
  }

  login(credentials: Credentials): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, credentials)
    .pipe(map((response: any) =>
      {
        localStorage.setItem('token', response.token);
        return response;
      }));
  }

  logout() {
    localStorage.removeItem('token');
  }

  getToken(){
    return localStorage.getItem('token');
  }


}
