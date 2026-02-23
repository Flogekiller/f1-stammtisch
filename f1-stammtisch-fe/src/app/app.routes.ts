import { Routes } from '@angular/router';
import {Login} from './sites/login/login';

export const routes: Routes = [
  {path:'login', component: Login},
  {path:'', component: Login},
];
