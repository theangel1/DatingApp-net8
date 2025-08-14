import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/user';
import { map } from 'rxjs';
import { environment } from '../../environments/environment';
import { LikesService } from './likes.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private http = inject(HttpClient)
  private likeService = inject(LikesService)
  baseUrl = environment.apiUrl
  currentUser = signal<User | null>(null)

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map(user => {
        //ejecutamos el callball acá
        if (user) {
          this.setCurrentUser(user)
        }
      })
    )
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(user => {
        //ejecutamos el callball acá
        if (user) {
          this.setCurrentUser(user)
        }
        return user
      })
    )
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser.set(user);
    this.likeService.getLikeIds();
  }


  logout() {
    localStorage.removeItem('user')
    this.currentUser.set(null)
  }

}

//servicios de angular son singletons. Es creado cuando la app enciende o se refresca el navegador y es bueno para almacenar states y hacer https requests
