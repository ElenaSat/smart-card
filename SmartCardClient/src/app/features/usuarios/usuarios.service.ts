import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseCrudService } from '../../shared/services/base-crud.service';
import { Usuario, CreateUsuario } from '../../shared/models';

@Injectable({ providedIn: 'root' })
export class UsuariosService extends BaseCrudService<Usuario, CreateUsuario> {
    protected endpoint = 'Usuarios';
    protected idField: keyof Usuario = 'idUsuario';
}
