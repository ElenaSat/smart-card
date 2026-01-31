import { Routes } from '@angular/router';
import { TipoTarjetasListComponent } from './tipo-tarjetas-list.component';
import { TipoTarjetasFormComponent } from './tipo-tarjetas-form.component';

export const TIPO_TARJETAS_ROUTES: Routes = [
    { path: '', component: TipoTarjetasListComponent },
    { path: 'new', component: TipoTarjetasFormComponent },
    { path: 'edit/:id', component: TipoTarjetasFormComponent }
];
