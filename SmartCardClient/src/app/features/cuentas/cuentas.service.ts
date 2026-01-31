import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseCrudService } from '../../shared/services/base-crud.service';
import { Cuenta, CreateCuenta } from '../../shared/models';

@Injectable({ providedIn: 'root' })
export class CuentasService extends BaseCrudService<Cuenta, CreateCuenta> {
    protected endpoint = 'Cuentas';
    protected idField: keyof Cuenta = 'idCuenta';
}
