import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../Services/authservices/auth.service';
import { User } from '../../Models/user.models';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-registro',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css'],
  providers: [AuthService]
})
export class RegistroComponent {
  user = new User('', '', '');
  errorMessage: string | null = null;

  constructor(private authService: AuthService, private router: Router) { }

  register() {
    this.authService.register(this.user).subscribe({
      next: (response) => {
        console.log('Registrado com sucesso', response);
        this.router.navigate(['/login']);
      },
      error: (error: HttpErrorResponse) => {
        if (error.status === 400 && error.error.errors) {
          const validationErrors = error.error.errors;
          if (validationErrors.Senha) {
            this.errorMessage = 'Erro na senha: ' + validationErrors.Senha.join(', ');
          } else {
            this.errorMessage = 'Falha no registro: ' + error.message;
          }
        } else {
          this.errorMessage = 'Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.';
        }
        console.log('Não foi possível realizar o cadastro', error);
      }
    });
  }
}
