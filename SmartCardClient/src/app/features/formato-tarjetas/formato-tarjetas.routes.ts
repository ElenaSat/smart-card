import { Routes } from '@angular/router';
import { FormatoTarjetasListComponent } from './formato-tarjetas-list.component';
import { FormatoTarjetasFormComponent } from './formato-tarjetas-form.component';

export const FORMATO_TARJETAS_ROUTES: Routes = [
    { path: '', component: FormatoTarjetasListComponent },
    { path: 'new', component: FormatoTarjetasFormComponent },
    { path: 'edit/:id', component: FormatoTarjetasFormComponent }
];
