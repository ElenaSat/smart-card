import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { UsuariosService } from './usuarios.service';

@Component({
    selector: 'app-usuarios-form',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, RouterLink],
    template: `
    <div class="p-6 max-w-2xl mx-auto">
      <div class="mb-6">
        <a routerLink="/usuarios" class="text-indigo-600 hover:text-indigo-800 flex items-center gap-2">
          <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 19l-7-7 7-7"/>
          </svg>
          Volver a la lista
        </a>
      </div>

      <div class="bg-white rounded-xl shadow-sm border border-gray-200 p-6">
        <h1 class="text-2xl font-bold text-gray-800 mb-6">
          {{ isEdit ? 'Editar Usuario' : 'Nuevo Usuario' }}
        </h1>

        <!-- Loading State -->
        @if (loading$ | async) {
          <div class="flex justify-center items-center py-12">
            <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-indigo-600"></div>
          </div>
        }

        <!-- Error State -->
        @if (error$ | async; as error) {
          <div class="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded-lg mb-4">
            {{ error }}
          </div>
        }

        @if (!(loading$ | async)) {
          <form [formGroup]="form" (ngSubmit)="onSubmit()" class="space-y-4">
            <div>
              <label for="titulo" class="block text-sm font-medium text-gray-700 mb-1">Título</label>
              <input id="titulo" formControlName="titulo" type="text"
                     class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-colors"
                     placeholder="Ej: Sr., Sra., Dr.">
            </div>

            <div>
              <label for="nombre" class="block text-sm font-medium text-gray-700 mb-1">Nombre *</label>
              <input id="nombre" formControlName="nombre" type="text"
                     class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-colors"
                     [class.border-red-500]="form.get('nombre')?.invalid && form.get('nombre')?.touched">
              @if (form.get('nombre')?.invalid && form.get('nombre')?.touched) {
                <p class="mt-1 text-sm text-red-600">El nombre es requerido</p>
              }
            </div>

            <div>
              <label for="apellido" class="block text-sm font-medium text-gray-700 mb-1">Apellido *</label>
              <input id="apellido" formControlName="apellido" type="text"
                     class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-colors"
                     [class.border-red-500]="form.get('apellido')?.invalid && form.get('apellido')?.touched">
              @if (form.get('apellido')?.invalid && form.get('apellido')?.touched) {
                <p class="mt-1 text-sm text-red-600">El apellido es requerido</p>
              }
            </div>

            <div>
              <label for="infoExtra" class="block text-sm font-medium text-gray-700 mb-1">Información Extra</label>
              <textarea id="infoExtra" formControlName="infoExtra" rows="3"
                        class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-colors"
                        placeholder="Notas adicionales..."></textarea>
            </div>

            <div class="flex gap-4 pt-4">
              <button type="submit" [disabled]="form.invalid || (loading$ | async)"
                      class="flex-1 bg-indigo-600 hover:bg-indigo-700 disabled:bg-gray-400 text-white px-6 py-3 rounded-lg font-medium transition-colors">
                {{ isEdit ? 'Actualizar' : 'Crear' }}
              </button>
              <a routerLink="/usuarios" 
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
export class UsuariosFormComponent implements OnInit {
    private fb = inject(FormBuilder);
    private service = inject(UsuariosService);
    private route = inject(ActivatedRoute);
    private router = inject(Router);

    loading$ = this.service.loading;
    error$ = this.service.error;
    isEdit = false;
    private editId?: number;

    form = this.fb.group({
        titulo: [''],
        nombre: ['', Validators.required],
        apellido: ['', Validators.required],
        infoExtra: ['']
    });

    ngOnInit(): void {
        const id = this.route.snapshot.paramMap.get('id');
        if (id) {
            this.isEdit = true;
            this.editId = +id;
            this.loadUser(this.editId);
        }
    }

    private loadUser(id: number): void {
        this.service.getById(id).subscribe(user => {
            if (user) {
                this.form.patchValue({
                    titulo: user.titulo || '',
                    nombre: user.nombre || '',
                    apellido: user.apellido || '',
                    infoExtra: user.infoExtra || ''
                });
            }
        });
    }

    onSubmit(): void {
        if (this.form.invalid) return;

        const data = this.form.value;

        if (this.isEdit && this.editId) {
            this.service.update(this.editId, { idUsuario: this.editId, ...data } as any).subscribe(() => {
                this.router.navigate(['/usuarios']);
            });
        } else {
            this.service.create(data as any).subscribe(() => {
                this.router.navigate(['/usuarios']);
            });
        }
    }
}
