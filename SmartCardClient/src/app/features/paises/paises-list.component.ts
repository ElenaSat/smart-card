import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { PaisesService } from './paises.service';

@Component({
    selector: 'app-paises-list',
    standalone: true,
    imports: [CommonModule, RouterLink],
    template: `
    <div class="p-6">
      <div class="flex justify-between items-center mb-6">
        <h1 class="text-2xl font-bold text-gray-800">Países</h1>
        <a routerLink="new" 
           class="bg-indigo-600 hover:bg-indigo-700 text-white px-4 py-2 rounded-lg transition-colors flex items-center gap-2">
          <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 4v16m8-8H4"/>
          </svg>
          Nuevo País
        </a>
      </div>

      @if (loading$ | async) {
        <div class="flex justify-center items-center py-12">
          <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-indigo-600"></div>
        </div>
      }

      @if (error$ | async; as error) {
        <div class="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded-lg mb-4">{{ error }}</div>
      }

      @if (!(loading$ | async)) {
        <div class="bg-white rounded-xl shadow-sm border border-gray-200 overflow-hidden">
          <table class="min-w-full divide-y divide-gray-200">
            <thead class="bg-gray-50">
              <tr>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">ID</th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">Nombre</th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">ISO 2</th>
                <th class="px-6 py-3 text-right text-xs font-medium text-gray-500 uppercase">Acciones</th>
              </tr>
            </thead>
            <tbody class="bg-white divide-y divide-gray-200">
              @for (item of items$ | async; track item.idPais) {
                <tr class="hover:bg-gray-50 transition-colors">
                  <td class="px-6 py-4 text-sm text-gray-900">{{ item.idPais }}</td>
                  <td class="px-6 py-4 text-sm text-gray-600">{{ item.nombre || '-' }}</td>
                  <td class="px-6 py-4 text-sm font-mono text-gray-600">{{ item.codigoIso2 || '-' }}</td>
                  <td class="px-6 py-4 text-right text-sm font-medium">
                    <a [routerLink]="['edit', item.idPais]" class="text-indigo-600 hover:text-indigo-900 mr-4">Editar</a>
                    <button (click)="onDelete(item.idPais)" class="text-red-600 hover:text-red-900">Eliminar</button>
                  </td>
                </tr>
              } @empty {
                <tr><td colspan="4" class="px-6 py-12 text-center text-gray-500">No hay países registrados</td></tr>
              }
            </tbody>
          </table>
        </div>
      }
    </div>
  `
})
export class PaisesListComponent implements OnInit {
    private service = inject(PaisesService);
    items$ = this.service.items;
    loading$ = this.service.loading;
    error$ = this.service.error;

    ngOnInit(): void { this.service.refresh(); }

    onDelete(id: number): void {
        if (confirm('¿Está seguro de eliminar este país?')) {
            this.service.delete(id).subscribe();
        }
    }
}
