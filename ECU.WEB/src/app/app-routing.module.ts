import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { AuthenticatedGuard } from './core/guards/authenticated.guard';
import { LoginComponent } from './business/authentication/login/login.component';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./shared/components/layout/layout.component'),
    children: [
      {
        path: 'dashboard',
        loadComponent: () => import('./business/dashboard/dashboard.component'),
        //canActivate: [AuthGuard]
      },
      {
        path: 'persona',
        loadComponent: () => import('./business/persona/persona.component'),
        //canActivate: [AuthGuard]
      },
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      }

    ]
  },
  {
    path: 'login',
    loadComponent: () => LoginComponent,//import('./business/authentication/login/login.component'),
    canActivate: [AuthenticatedGuard]
  },
  {
    path: '**',
    redirectTo: 'dashboard'
  }
];
