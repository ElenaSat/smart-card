import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { TarjetasService } from './tarjetas.service';

@Component({
    selector: 'app-tarjetas-form',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, RouterLink],
    template: `
    <div class="p-6 max-w-4xl mx-auto">
      <div class="mb-6">
        <a routerLink="/tarjetas" class="text-indigo-600 hover:text-indigo-800 flex items-center gap-2">
          <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 19l-7-7 7-7"/>
          </svg>
          Volver a la lista
        </a>
      </div>

      <div class="bg-white rounded-xl shadow-sm border border-gray-200 p-6">
        <h1 class="text-2xl font-bold text-gray-800 mb-6">
          {{ isEdit ? 'Editar Tarjeta' : 'Nueva Tarjeta' }}
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
          <form [formGroup]="form" (ngSubmit)="onSubmit()" class="space-y-6">
            <!-- Card Info Section -->
            <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <label for="pan" class="block text-sm font-medium text-gray-700 mb-1">PAN *</label>
                <input id="pan" formControlName="pan" type="text" maxlength="19"
                       class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 font-mono"
                       [class.border-red-500]="form.get('pan')?.invalid && form.get('pan')?.touched"
                       placeholder="1234567890123456">
                @if (form.get('pan')?.invalid && form.get('pan')?.touched) {
                  <p class="mt-1 text-sm text-red-600">El PAN es requerido</p>
                }
              </div>

              <div>
                <label for="pin" class="block text-sm font-medium text-gray-700 mb-1">PIN</label>
                <input id="pin" formControlName="pin" type="password" maxlength="6"
                       class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
                       placeholder="****">
              </div>
            </div>

            <!-- Relations Section -->
            <div class="grid grid-cols-1 md:grid-cols-3 gap-4">
              <div>
                <label for="idCuenta" class="block text-sm font-medium text-gray-700 mb-1">ID Cuenta</label>
                <input id="idCuenta" formControlName="idCuenta" type="number"
                       class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500">
              </div>

              <div>
                <label for="idTipo" class="block text-sm font-medium text-gray-700 mb-1">ID Tipo Tarjeta</label>
                <input id="idTipo" formControlName="idTipo" type="number"
                       class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500">
              </div>

              <div>
                <label for="idFormato" class="block text-sm font-medium text-gray-700 mb-1">ID Formato</label>
                <input id="idFormato" formControlName="idFormato" type="number"
                       class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500">
              </div>
            </div>

            <!-- Dates Section -->
            <div class="grid grid-cols-1 md:grid-cols-3 gap-4">
              <div>
                <label for="fechaEmision" class="block text-sm font-medium text-gray-700 mb-1">Fecha Emisión</label>
                <input id="fechaEmision" formControlName="fechaEmision" type="date"
                       class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500">
              </div>

              <div>
                <label for="fechaExpiracion" class="block text-sm font-medium text-gray-700 mb-1">Fecha Expiración</label>
                <input id="fechaExpiracion" formControlName="fechaExpiracion" type="date"
                       class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500">
              </div>

              <div>
                <label for="idPaisEmision" class="block text-sm font-medium text-gray-700 mb-1">ID País Emisión</label>
                <input id="idPaisEmision" formControlName="idPaisEmision" type="number"
                       class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500">
              </div>
            </div>

            <!-- Flags Section -->
            <div class="flex gap-6">
              <label class="flex items-center gap-2 cursor-pointer">
                <input type="checkbox" formControlName="ddaHabilitado"
                       class="w-5 h-5 text-indigo-600 border-gray-300 rounded focus:ring-indigo-500">
                <span class="text-sm text-gray-700">DDA Habilitado</span>
              </label>

              <label class="flex items-center gap-2 cursor-pointer">
                <input type="checkbox" formControlName="arqcHabilitado"
                       class="w-5 h-5 text-indigo-600 border-gray-300 rounded focus:ring-indigo-500">
                <span class="text-sm text-gray-700">ARQC Habilitado</span>
              </label>
            </div>

            <!-- Tracks Section -->
            <div class="space-y-4">
              <h3 class="text-lg font-medium text-gray-800">Tracks</h3>
              <div>
                <label for="track1" class="block text-sm font-medium text-gray-700 mb-1">Track 1</label>
                <input id="track1" formControlName="track1" type="text"
                       class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 font-mono text-sm">
              </div>
              <div>
                <label for="track2" class="block text-sm font-medium text-gray-700 mb-1">Track 2</label>
                <input id="track2" formControlName="track2" type="text"
                       class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 font-mono text-sm">
              </div>
              <div>
                <label for="track3" class="block text-sm font-medium text-gray-700 mb-1">Track 3</label>
                <input id="track3" formControlName="track3" type="text"
                       class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 font-mono text-sm">
              </div>
            </div>

            <div class="flex gap-4 pt-4">
              <button type="submit" [disabled]="form.invalid || (loading$ | async)"
                      class="flex-1 bg-indigo-600 hover:bg-indigo-700 disabled:bg-gray-400 text-white px-6 py-3 rounded-lg font-medium transition-colors">
                {{ isEdit ? 'Actualizar' : 'Crear' }}
              </button>
              <a routerLink="/tarjetas" 
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
export class TarjetasFormComponent implements OnInit {
    private fb = inject(FormBuilder);
    private service = inject(TarjetasService);
    private route = inject(ActivatedRoute);
    private router = inject(Router);

    loading$ = this.service.loading;
    error$ = this.service.error;
    isEdit = false;
    private editId?: number;

    form = this.fb.group({
        idCuenta: [null as number | null],
        idFormato: [null as number | null],
        idTipo: [null as number | null],
        pan: ['', Validators.required],
        pin: [''],
        fechaEmision: [''],
        fechaExpiracion: [''],
        idPaisEmision: [null as number | null],
        ddaHabilitado: [false],
        arqcHabilitado: [false],
        track1: [''],
        track2: [''],
        track3: ['']
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
                    ...item,
                    fechaEmision: item.fechaEmision ? item.fechaEmision.split('T')[0] : '',
                    fechaExpiracion: item.fechaExpiracion ? item.fechaExpiracion.split('T')[0] : ''
                });
            }
        });
    }

    onSubmit(): void {
        if (this.form.invalid) return;

        const data = this.form.value;

        if (this.isEdit && this.editId) {
            this.service.update(this.editId, { idTarjeta: this.editId, ...data } as any).subscribe(() => {
                this.router.navigate(['/tarjetas']);
            });
        } else {
            this.service.create(data as any).subscribe(() => {
                this.router.navigate(['/tarjetas']);
            });
        }
    }
}
