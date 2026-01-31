import { Routes } from '@angular/router';
import { UsuariosListComponent } from './usuarios-list.component';
import { UsuariosFormComponent } from './usuarios-form.component';

export const USUARIOS_ROUTES: Routes = [
    { path: '', component: UsuariosListComponent },
    { path: 'new', component: UsuariosFormComponent },
    { path: 'edit/:id', component: UsuariosFormComponent }
];
