import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { IconDirective, IconService } from '@ant-design/icons-angular';
import { PlusOutline } from '@ant-design/icons-angular/icons';
import { CuentasService } from './cuentas.service';

@Component({
  selector: 'app-cuentas-list',
  standalone: true,
  imports: [CommonModule, RouterLink, IconDirective],
  template: `
    <div class="row">
      <div class="col-sm-12">
        <div class="card">
          <div class="card-header d-flex justify-content-between align-items-center">
            <h5>Cuentas</h5>
            <a routerLink="new" class="btn btn-primary d-flex align-items-center">
              <i antIcon type="plus" theme="outline" class="me-2"></i> Nueva Cuenta
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
                <th>Número</th>
                <th>ID Usuario</th>
                <th class="text-end">Acciones</th>
              </tr>
            </thead>
            <tbody>
              @for (item of items$ | async; track item.idCuenta) {
                <tr>
                  <td>{{ item.idCuenta }}</td>
                  <td>{{ item.numero || '-' }}</td>
                  <td>{{ item.idUsuario || '-' }}</td>
                  <td class="text-end">
                    <a [routerLink]="['edit', item.idCuenta]" class="text-primary me-3">Editar</a>
                    <button (click)="onDelete(item.idCuenta)" class="btn btn-link text-danger p-0">Eliminar</button>
                  </td>
                </tr>
              } @empty {
                <tr>
                  <td colspan="4" class="text-center py-5 text-muted">No hay cuentas registradas</td>
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
export class CuentasListComponent implements OnInit {
  private service = inject(CuentasService);
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

  onDelete(id: number): void {
    if (confirm('¿Está seguro de eliminar esta cuenta?')) {
      this.service.delete(id).subscribe();
    }
  }
}
