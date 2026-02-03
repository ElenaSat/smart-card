import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { IconDirective, IconService } from '@ant-design/icons-angular';
import { ArrowLeftOutline } from '@ant-design/icons-angular/icons';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CuentasService } from './cuentas.service';

@Component({
  selector: 'app-cuentas-form',
  standalone: true,

  imports: [CommonModule, ReactiveFormsModule, RouterLink, IconDirective],
  template: `
    <div class="row">
      <div class="col-sm-12">
        <div class="card">
          <div class="card-header d-flex justify-content-between align-items-center">
            <h5>{{ isEdit ? 'Editar Cuenta' : 'Nueva Cuenta' }}</h5>
            <a routerLink="/cuentas" class="btn btn-link d-flex align-items-center">
              <i antIcon type="arrow-left" theme="outline" class="me-2"></i> Volver
            </a>
          </div>
          <div class="card-body">

            @if (loading$ | async) {
              <div class="d-flex justify-content-center py-5">
                <div class="spinner-border text-primary" role="status">
                  <span class="visually-hidden">Loading...</span>
                </div>
              </div>
            }

            @if (error$ | async; as error) {
              <div class="alert alert-danger mb-4">{{ error }}</div>
            }

            @if (!(loading$ | async)) {
              <form [formGroup]="form" (ngSubmit)="onSubmit()">
                <div class="form-group mb-3">
                  <label for="numero" class="form-label">Número de Cuenta *</label>
                  <input id="numero" formControlName="numero" type="text" class="form-control"
                         [class.is-invalid]="form.get('numero')?.invalid && form.get('numero')?.touched">
                  @if (form.get('numero')?.invalid && form.get('numero')?.touched) {
                    <div class="invalid-feedback">El número de cuenta es requerido</div>
                  }
                </div>

                <div class="form-group mb-3">
                  <label for="idUsuario" class="form-label">ID Usuario</label>
                  <input id="idUsuario" formControlName="idUsuario" type="number" class="form-control">
                </div>

                <div class="d-flex gap-2 mt-4">
                  <button type="submit" [disabled]="form.invalid || (loading$ | async)" class="btn btn-primary">
                    {{ isEdit ? 'Actualizar' : 'Crear' }}
                  </button>
                  <a routerLink="/cuentas" class="btn btn-secondary">Cancelar</a>
                </div>
              </form>
            }
          </div>
        </div>
      </div>
    </div>
  `
})
export class CuentasFormComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly service = inject(CuentasService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly iconService = inject(IconService);

  loading$ = this.service.loading;
  error$ = this.service.error;
  isEdit = false;
  private editId?: number;

  form = this.fb.group({
    numero: ['', Validators.required],
    idUsuario: [null as number | null]
  });

  constructor() {
    this.iconService.addIcon(ArrowLeftOutline);
  }

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
