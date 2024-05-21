import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Credentials } from '../../Models/credentials.model';
import { AuthService } from '../../Services/authservices/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  providers: [AuthService]
})

export class LoginComponent {
  credentials: Credentials = {email: '', senha: ''};

  constructor(private authService: AuthService, private router: Router) { }

  login(){
    console.log('Dados do login', this.credentials);
    this.authService.login(this.credentials).subscribe(response => {
      console.log('Logado com sucesso', response);
      this.router.navigate(['/Home']);
    }, error => {
      console.log('Não foi possivel realizar o login', error);
    });
  }
}
