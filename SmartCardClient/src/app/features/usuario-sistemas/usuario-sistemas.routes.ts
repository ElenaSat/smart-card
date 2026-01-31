import { Routes } from '@angular/router';
import { UsuarioSistemasListComponent } from './usuario-sistemas-list.component';
import { UsuarioSistemasFormComponent } from './usuario-sistemas-form.component';

export const USUARIO_SISTEMAS_ROUTES: Routes = [
    { path: '', component: UsuarioSistemasListComponent },
    { path: 'new', component: UsuarioSistemasFormComponent },
    { path: 'edit/:id', component: UsuarioSistemasFormComponent }
];
