import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private http = inject(HttpClient)
  baseUrl = 'https://localhost:5001/api/'

  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model)
  }

}

//servicios de angular son singletons. Es creado cuando la app enciende o se refresca el navegador y es bueno para almacenar states y hacer https requests
