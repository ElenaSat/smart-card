import { Routes } from '@angular/router';
import { CuentasListComponent } from './cuentas-list.component';
import { CuentasFormComponent } from './cuentas-form.component';

export const CUENTAS_ROUTES: Routes = [
    { path: '', component: CuentasListComponent },
    { path: 'new', component: CuentasFormComponent },
    { path: 'edit/:id', component: CuentasFormComponent }
];
