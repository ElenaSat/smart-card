import { Routes } from '@angular/router';
import { AdminLayout } from './theme/layouts/admin-layout/admin-layout.component';

export const routes: Routes = [
    {
        path: '',
        component: AdminLayout,
        children: [
            { path: '', redirectTo: 'usuarios', pathMatch: 'full' },
            {
                path: 'usuarios',
                loadChildren: () => import('./features/usuarios/usuarios.routes').then(m => m.USUARIOS_ROUTES)
            },
            {
                path: 'cuentas',
                loadChildren: () => import('./features/cuentas/cuentas.routes').then(m => m.CUENTAS_ROUTES)
            },
            {
                path: 'tarjetas',
                loadChildren: () => import('./features/tarjetas/tarjetas.routes').then(m => m.TARJETAS_ROUTES)
            },
            {
                path: 'tipo-tarjetas',
                loadChildren: () => import('./features/tipo-tarjetas/tipo-tarjetas.routes').then(m => m.TIPO_TARJETAS_ROUTES)
            },
            {
                path: 'formato-tarjetas',
                loadChildren: () => import('./features/formato-tarjetas/formato-tarjetas.routes').then(m => m.FORMATO_TARJETAS_ROUTES)
            },
            {
                path: 'paises',
                loadChildren: () => import('./features/paises/paises.routes').then(m => m.PAISES_ROUTES)
            },
            {
                path: 'usuario-sistemas',
                loadChildren: () => import('./features/usuario-sistemas/usuario-sistemas.routes').then(m => m.USUARIO_SISTEMAS_ROUTES)
            }
        ]
    }
];
