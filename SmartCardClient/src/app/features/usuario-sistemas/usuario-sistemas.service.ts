import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseCrudService } from '../../shared/services/base-crud.service';
import { UsuarioSistema, CreateUsuarioSistema } from '../../shared/models';

@Injectable({ providedIn: 'root' })
export class UsuarioSistemasService extends BaseCrudService<UsuarioSistema, CreateUsuarioSistema> {
    protected endpoint = 'UsuarioSistemas';
    protected idField: keyof UsuarioSistema = 'idUsuarioSistema';
}
