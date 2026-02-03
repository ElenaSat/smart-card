import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { IconDirective, IconService } from '@ant-design/icons-angular';
import { ArrowLeftOutline } from '@ant-design/icons-angular/icons';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { TarjetasService } from './tarjetas.service';
import { PaisesService } from '../paises/paises.service';
import { TipoTarjetasService } from '../tipo-tarjetas/tipo-tarjetas.service';
import { FormatoTarjetasService } from '../formato-tarjetas/formato-tarjetas.service';

@Component({
  selector: 'app-tarjetas-form',
  standalone: true,

  imports: [CommonModule, ReactiveFormsModule, RouterLink, IconDirective],
  template: `
    <div class="row">
      <div class="col-sm-12">
        <div class="card">
          <div class="card-header d-flex justify-content-between align-items-center">
            <h5>{{ isEdit ? 'Editar Tarjeta' : 'Nueva Tarjeta' }}</h5>
            <a routerLink="/tarjetas" class="btn btn-link d-flex align-items-center">
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
                <!-- Card Info Section -->
                <div class="row">
                  <div class="col-md-6 mb-3">
                    <label for="pan" class="form-label">PAN *</label>
                    <input id="pan" formControlName="pan" type="text" maxlength="19" class="form-control font-monospace"
                           [class.is-invalid]="form.get('pan')?.invalid && form.get('pan')?.touched"
                           placeholder="1234567890123456">
                    @if (form.get('pan')?.invalid && form.get('pan')?.touched) {
                      <div class="invalid-feedback">El PAN es requerido</div>
                    }
                  </div>
                  <div class="col-md-6 mb-3">
                    <label for="pin" class="form-label">PIN</label>
                    <input id="pin" formControlName="pin" type="password" maxlength="6" class="form-control" placeholder="****">
                  </div>
                </div>

                <!-- Relations Section -->
                <div class="row">
                  <div class="col-md-4 mb-3">
                    <label for="idCuenta" class="form-label">ID Cuenta</label>
                    <input id="idCuenta" formControlName="idCuenta" type="number" class="form-control">
                  </div>
                  <div class="col-md-4 mb-3">                    
                    <label for="idTipo" class="form-label">Tipo Tarjeta</label>
                    <select id="idTipo" formControlName="idTipo" class="form-select">
                      <option [ngValue]="null">Seleccione un tipo</option>
                      @for (tipo of tipos$ | async; track tipo.idTipo) {
                        <option [ngValue]="tipo.idTipo">{{ tipo.nombre }}</option>
                      }
                    </select>                  
                  </div>
                  <div class="col-md-4 mb-3">
                    <label for="idFormato" class="form-label">Formato</label>
                    <select id="idFormato" formControlName="idFormato" class="form-select">
                      <option [ngValue]="null">Seleccione un formato</option>
                      @for (formato of formatos$ | async; track formato.idFormato) {
                        <option [ngValue]="formato.idFormato">{{ formato.nombre }}</option>
                      }
                    </select>  
                  </div>
                </div>

                <!-- Dates Section -->
                <div class="row">
                  <div class="col-md-4 mb-3">
                    <label for="fechaEmision" class="form-label">Fecha Emisión</label>
                    <input id="fechaEmision" formControlName="fechaEmision" type="date" class="form-control">
                  </div>
                  <div class="col-md-4 mb-3">
                    <label for="fechaExpiracion" class="form-label">Fecha Expiración</label>
                    <input id="fechaExpiracion" formControlName="fechaExpiracion" type="date" class="form-control">
                  </div>
                  <div class="col-md-4 mb-3">
                    <label for="idPaisEmision" class="form-label">País Emisión</label>
                    <select id="idPaisEmision" formControlName="idPaisEmision" class="form-select">
                      <option [ngValue]="null">Seleccione un país</option>
                      @for (pais of paises$ | async; track pais.idPais) {
                        <option [ngValue]="pais.idPais">{{ pais.nombre }} ({{ pais.codigoIso2 }})</option>
                      }
                    </select>
                  </div>
                </div>

                <!-- Flags Section -->
                <div class="mb-3">
                  <div class="form-check form-check-inline">
                    <input class="form-check-input" type="checkbox" id="ddaHabilitado" formControlName="ddaHabilitado">
                    <label class="form-check-label" for="ddaHabilitado">DDA Habilitado</label>
                  </div>
                  <div class="form-check form-check-inline">
                    <input class="form-check-input" type="checkbox" id="arqcHabilitado" formControlName="arqcHabilitado">
                    <label class="form-check-label" for="arqcHabilitado">ARQC Habilitado</label>
                  </div>
                </div>

                <!-- Tracks Section -->
                <h5 class="mt-4 mb-3">Tracks</h5>
                <div class="mb-3">
                  <label for="track1" class="form-label">Track 1</label>
                  <input id="track1" formControlName="track1" type="text" class="form-control font-monospace form-control-sm">
                </div>
                <div class="mb-3">
                  <label for="track2" class="form-label">Track 2</label>
                  <input id="track2" formControlName="track2" type="text" class="form-control font-monospace form-control-sm">
                </div>
                <div class="mb-3">
                  <label for="track3" class="form-label">Track 3</label>
                  <input id="track3" formControlName="track3" type="text" class="form-control font-monospace form-control-sm">
                </div>

                <div class="d-flex gap-2 mt-4">
                  <button type="submit" [disabled]="form.invalid || (loading$ | async)" class="btn btn-primary">
                    {{ isEdit ? 'Actualizar' : 'Crear' }}
                  </button>
                  <a routerLink="/tarjetas" class="btn btn-secondary">Cancelar</a>
                </div>
              </form>
            }
          </div>
        </div>
      </div>
    </div>
  `
})
export class TarjetasFormComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly service = inject(TarjetasService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly iconService = inject(IconService);
  private readonly paisesService = inject(PaisesService);
  private readonly tiposService = inject(TipoTarjetasService);
  private readonly formatosService = inject(FormatoTarjetasService);

  loading$ = this.service.loading;
  error$ = this.service.error;
  paises$ = this.paisesService.items;
  tipos$ = this.tiposService.items;
  formatos$ = this.formatosService.items;
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

  constructor() {
    this.iconService.addIcon(ArrowLeftOutline);
  }

  ngOnInit(): void {
    this.paisesService.refresh();
    this.tiposService.refresh();
    this.formatosService.refresh();
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
