import { LoginComponent } from './pages/login/login.component';
import { RegistroComponent } from './pages/registro/registro.component';
import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';

export const routes: Routes = [
  { path: '', component: RegistroComponent},
  { path: 'login', component: LoginComponent},
  { path: 'Home', component: HomeComponent},
];
