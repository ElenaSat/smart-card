import { Routes } from '@angular/router';
import { TarjetasListComponent } from './tarjetas-list.component';
import { TarjetasFormComponent } from './tarjetas-form.component';

export const TARJETAS_ROUTES: Routes = [
    { path: '', component: TarjetasListComponent },
    { path: 'new', component: TarjetasFormComponent },
    { path: 'edit/:id', component: TarjetasFormComponent }
];
