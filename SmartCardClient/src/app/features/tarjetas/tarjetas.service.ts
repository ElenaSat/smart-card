import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseCrudService } from '../../shared/services/base-crud.service';
import { Tarjeta, CreateTarjeta } from '../../shared/models';

@Injectable({ providedIn: 'root' })
export class TarjetasService extends BaseCrudService<Tarjeta, CreateTarjeta> {
    protected endpoint = 'Tarjetas';
    protected idField: keyof Tarjeta = 'idTarjeta';
}
