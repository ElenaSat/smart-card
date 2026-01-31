import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CuentasService } from './cuentas.service';

@Component({
    selector: 'app-cuentas-form',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, RouterLink],
    template: `
    <div class="p-6 max-w-2xl mx-auto">
      <div class="mb-6">
        <a routerLink="/cuentas" class="text-indigo-600 hover:text-indigo-800 flex items-center gap-2">
          <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 19l-7-7 7-7"/>
          </svg>
          Volver a la lista
        </a>
      </div>

      <div class="bg-white rounded-xl shadow-sm border border-gray-200 p-6">
        <h1 class="text-2xl font-bold text-gray-800 mb-6">
          {{ isEdit ? 'Editar Cuenta' : 'Nueva Cuenta' }}
        </h1>

        @if (loading$ | async) {
          <div class="flex justify-center items-center py-12">
            <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-indigo-600"></div>
          </div>
        }

        @if (error$ | async; as error) {
          <div class="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded-lg mb-4">
            {{ error }}
          </div>
        }

        @if (!(loading$ | async)) {
          <form [formGroup]="form" (ngSubmit)="onSubmit()" class="space-y-4">
            <div>
              <label for="numero" class="block text-sm font-medium text-gray-700 mb-1">Número de Cuenta *</label>
              <input id="numero" formControlName="numero" type="text"
                     class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-colors"
                     [class.border-red-500]="form.get('numero')?.invalid && form.get('numero')?.touched">
              @if (form.get('numero')?.invalid && form.get('numero')?.touched) {
                <p class="mt-1 text-sm text-red-600">El número de cuenta es requerido</p>
              }
            </div>

            <div>
              <label for="idUsuario" class="block text-sm font-medium text-gray-700 mb-1">ID Usuario</label>
              <input id="idUsuario" formControlName="idUsuario" type="number"
                     class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-colors">
            </div>

            <div class="flex gap-4 pt-4">
              <button type="submit" [disabled]="form.invalid || (loading$ | async)"
                      class="flex-1 bg-indigo-600 hover:bg-indigo-700 disabled:bg-gray-400 text-white px-6 py-3 rounded-lg font-medium transition-colors">
                {{ isEdit ? 'Actualizar' : 'Crear' }}
              </button>
              <a routerLink="/cuentas" 
                 class="flex-1 text-center bg-gray-200 hover:bg-gray-300 text-gray-700 px-6 py-3 rounded-lg font-medium transition-colors">
                Cancelar
              </a>
            </div>
          </form>
        }
      </div>
    </div>
  `
})
export class CuentasFormComponent implements OnInit {
    private fb = inject(FormBuilder);
    private service = inject(CuentasService);
    private route = inject(ActivatedRoute);
    private router = inject(Router);

    loading$ = this.service.loading;
    error$ = this.service.error;
    isEdit = false;
    private editId?: number;

    form = this.fb.group({
        numero: ['', Validators.required],
        idUsuario: [null as number | null]
    });

    ngOnInit(): void {
        const id = this.route.snapshot.paramMap.get('id');
        if (id) {
            this.isEdit = true;
            this.editId = +id;
            this.loadItem(this.editId);
        }
    }

    private loadItem(id: number): void {
        this.service.getById(id).subscribe(item => {
            if (item) {
                this.form.patchValue({
                    numero: item.numero || '',
                    idUsuario: item.idUsuario || null
                });
            }
        });
    }

    onSubmit(): void {
        if (this.form.invalid) return;

        const data = this.form.value;

        if (this.isEdit && this.editId) {
            this.service.update(this.editId, { idCuenta: this.editId, ...data } as any).subscribe(() => {
                this.router.navigate(['/cuentas']);
            });
        } else {
            this.service.create(data as any).subscribe(() => {
                this.router.navigate(['/cuentas']);
            });
        }
    }
}
