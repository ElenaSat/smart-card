import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseCrudService } from '../../shared/services/base-crud.service';
import { Pais, CreatePais } from '../../shared/models';

@Injectable({ providedIn: 'root' })
export class PaisesService extends BaseCrudService<Pais, CreatePais> {
    protected endpoint = 'Pais';
    protected idField: keyof Pais = 'idPais';
}
