import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/user';
import { map } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private http = inject(HttpClient)
  baseUrl = environment.apiUrl
  currentUser = signal<User | null>(null)

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map(user => {
        //ejecutamos el callball acá
        if (user) {
          localStorage.setItem('user', JSON.stringify(user))
          this.currentUser.set(user)
        }
      })
    )
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(user => {
        //ejecutamos el callball acá
        if (user) {
          localStorage.setItem('user', JSON.stringify(user))
          this.currentUser.set(user)
        }
        return user
      })
    )
  }


  logout() {
    localStorage.removeItem('user')
    this.currentUser.set(null)
  }

}

//servicios de angular son singletons. Es creado cuando la app enciende o se refresca el navegador y es bueno para almacenar states y hacer https requests
