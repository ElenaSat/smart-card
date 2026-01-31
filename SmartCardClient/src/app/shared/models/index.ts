// Usuario
export interface Usuario {
    idUsuario: number;
    titulo?: string;
    nombre?: string;
    apellido?: string;
    infoExtra?: string;
}

export interface CreateUsuario {
    titulo?: string;
    nombre?: string;
    apellido?: string;
    infoExtra?: string;
}

// Cuenta
export interface Cuenta {
    idCuenta: number;
    numero?: string;
    idUsuario?: number;
}

export interface CreateCuenta {
    numero?: string;
    idUsuario?: number;
}

// Tarjeta
export interface Tarjeta {
    idTarjeta: number;
    idCuenta?: number;
    idFormato?: number;
    idTipo?: number;
    pan?: string;
    pin?: string;
    fechaEmision?: string;
    fechaExpiracion?: string;
    idPaisEmision?: number;
    ddaHabilitado?: boolean;
    arqcHabilitado?: boolean;
    track1?: string;
    track2?: string;
    track3?: string;
}

export interface CreateTarjeta {
    idCuenta?: number;
    idFormato?: number;
    idTipo?: number;
    pan?: string;
    pin?: string;
    fechaEmision?: string;
    fechaExpiracion?: string;
    idPaisEmision?: number;
    ddaHabilitado?: boolean;
    arqcHabilitado?: boolean;
    track1?: string;
    track2?: string;
    track3?: string;
}

// Pais
export interface Pais {
    idPais: number;
    nombre?: string;
    codigoIso2?: string;
}

export interface CreatePais {
    nombre?: string;
    codigoIso2?: string;
}

// FormatoTarjeta
export interface FormatoTarjeta {
    idFormato: number;
    nombre?: string;
}

export interface CreateFormatoTarjeta {
    nombre?: string;
}

// TipoTarjeta
export interface TipoTarjeta {
    idTipo: number;
    nombre?: string;
}

export interface CreateTipoTarjeta {
    nombre?: string;
}

// UsuarioSistema
export interface UsuarioSistema {
    idUsuarioSistema: number;
    nombreUsuario?: string;
}

export interface CreateUsuarioSistema {
    nombreUsuario?: string;
}
