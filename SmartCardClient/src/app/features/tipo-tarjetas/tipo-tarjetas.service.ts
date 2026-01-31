import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseCrudService } from '../../shared/services/base-crud.service';
import { TipoTarjeta, CreateTipoTarjeta } from '../../shared/models';

@Injectable({ providedIn: 'root' })
export class TipoTarjetasService extends BaseCrudService<TipoTarjeta, CreateTipoTarjeta> {
    protected endpoint = 'TipoTarjetas';
    protected idField: keyof TipoTarjeta = 'idTipo';
}
