import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {TokenModel} from '../models/TokenModel';
import {Observable} from 'rxjs/Observable';

import 'rxjs/add/operator/map';

@Injectable()
export class AuthService {

  constructor(private httpClient: HttpClient) {
  }

  login(userName: string, password: string) {
    // return new Observable(observer => {
    //   this.httpClient.post(
    //     'localhost:5000/api/auth/token',
    //     {
    //       userName: userName,
    //       password: password
    //     })
    //     .subscribe((tokenModel: TokenModel) => {
    //       if (tokenModel && tokenModel.accessToken && tokenModel.refreshToken) {
    //         localStorage.setItem('authTokens', JSON.stringify(tokenModel));
    //         observer.complete();
    //       }
    //     });
    // });

    const loginModel = {
      userName: userName,
      password: password
    };

    return this.httpClient.post('http://localhost:57970/api/auth/token', loginModel)
      .map((tokenModel: TokenModel) => {
        if (tokenModel && tokenModel.accessToken && tokenModel.refreshToken) {
          localStorage.setItem('authTokens', JSON.stringify(tokenModel));
        }
      });
  }

  logout() {
    localStorage.removeItem('authTokens');
  }
}
