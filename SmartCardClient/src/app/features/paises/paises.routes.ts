import { Routes } from '@angular/router';
import { PaisesListComponent } from './paises-list.component';
import { PaisesFormComponent } from './paises-form.component';

export const PAISES_ROUTES: Routes = [
    { path: '', component: PaisesListComponent },
    { path: 'new', component: PaisesFormComponent },
    { path: 'edit/:id', component: PaisesFormComponent }
];
