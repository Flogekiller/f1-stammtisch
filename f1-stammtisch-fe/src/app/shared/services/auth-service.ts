import { Injectable, inject } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BASE_URL} from '../globals';
import {map} from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  httpClient = inject(HttpClient);

  login(email: string, password: string) {
    return this.httpClient.post(
      'http://localhost:5000/api/account/login',
      { email, password },
      { responseType: 'text' }
    ).pipe(
      map(token => ({ token }))
    );
  }
}
