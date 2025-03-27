import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  standalone: true,
  imports: [],
  templateUrl: './server-error.component.html',
  styleUrl: './server-error.component.css'
})
export class ServerErrorComponent {
//buen momento para crear un constructor en esta clase porq es la unica forma de acceder a navigationExtras...
error:any

constructor(private router: Router){
  const navigation = this.router.getCurrentNavigation()
  //todo deber opcional ya que puede retornar undefined
  this.error = navigation?.extras?.state?.['error'];
}
}
