import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { IconDirective, IconService } from '@ant-design/icons-angular';
import { PlusOutline } from '@ant-design/icons-angular/icons';
import { TarjetasService } from './tarjetas.service';

@Component({
  selector: 'app-tarjetas-list',
  standalone: true,
  imports: [CommonModule, RouterLink, IconDirective],
  template: `
    <div class="row">
      <div class="col-sm-12">
        <div class="card">
          <div class="card-header d-flex justify-content-between align-items-center">
            <h5>Tarjetas</h5>
            <a routerLink="new" class="btn btn-primary d-flex align-items-center">
              <i antIcon type="plus" theme="outline" class="me-2"></i> Nueva Tarjeta
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
        <div class="table-responsive">
          <table class="table table-hover mb-0">
            <thead>
              <tr>
                <th>ID</th>
                <th>PAN</th>
                <th>Tipo</th>
                <th>Formato</th>
                <th>Expiración</th>
                <th>DDA</th>
                <th class="text-end">Acciones</th>
              </tr>
            </thead>
            <tbody>
              @for (item of items$ | async; track item.idTarjeta) {
                <tr>
                  <td>{{ item.idTarjeta }}</td>
                  <td class="font-monospace">{{ maskPan(item.pan) }}</td>
                  <td>{{ item.idTipo || '-' }}</td>
                  <td>{{ item.idFormato || '-' }}</td>
                  <td>{{ item.fechaExpiracion ? (item.fechaExpiracion | date:'MM/yy') : '-' }}</td>
                  <td>
                    @if (item.ddaHabilitado) {
                      <span class="badge bg-light-success text-success">Sí</span>
                    } @else {
                      <span class="badge bg-light-secondary text-secondary">No</span>
                    }
                  </td>
                  <td class="text-end">
                    <a [routerLink]="['edit', item.idTarjeta]" class="text-primary me-3">Editar</a>
                    <button (click)="onDelete(item.idTarjeta)" class="btn btn-link text-danger p-0">Eliminar</button>
                  </td>
                </tr>
              } @empty {
                <tr>
                  <td colspan="7" class="text-center py-5 text-muted">No hay tarjetas registradas</td>
                </tr>
              }
            </tbody>
          </table>
        </div>
      }
          </div>
        </div>
      </div>
    </div>
  `
})
export class TarjetasListComponent implements OnInit {
  private service = inject(TarjetasService);
  private iconService = inject(IconService);

  constructor() {
    this.iconService.addIcon(PlusOutline);
  }

  items$ = this.service.items;
  loading$ = this.service.loading;
  error$ = this.service.error;

  ngOnInit(): void {
    this.service.refresh();
  }

  maskPan(pan?: string): string {
    if (!pan || pan.length < 8) return pan || '-';
    return pan.substring(0, 4) + ' **** **** ' + pan.substring(pan.length - 4);
  }

  onDelete(id: number): void {
    if (confirm('¿Está seguro de eliminar esta tarjeta?')) {
      this.service.delete(id).subscribe();
    }
  }
}
