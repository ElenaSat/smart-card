import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseCrudService } from '../../shared/services/base-crud.service';
import { FormatoTarjeta, CreateFormatoTarjeta } from '../../shared/models';

@Injectable({ providedIn: 'root' })
export class FormatoTarjetasService extends BaseCrudService<FormatoTarjeta, CreateFormatoTarjeta> {
    protected endpoint = 'FormatoTarjetas';
    protected idField: keyof FormatoTarjeta = 'idFormato';
}
