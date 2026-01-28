using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

public class Tarjeta : INotifyPropertyChanged
{
    private int _idTarjeta;
    private int _idCuenta;
    private int _idFormato;
    private int _idTipo;
    private string _pan = string.Empty;
    private string? _pin;
    private DateTime? _fechaEmision;
    private DateTime? _fechaExpiracion;
    private int _idPaisEmision;
    private bool _ddaHabilitado;
    private bool _arqcHabilitado;
    private string? _track1;
    private string? _track2;
    private string? _track3;
    private DateTime _fechaCreacion = DateTime.UtcNow;
    private DateTime? _fechaModificacion;
    private int _usuarioCreacion = 3;
    private int? _usuarioModificacion;

    [Key]
    public int IdTarjeta
    {
        get => _idTarjeta;
        set
        {
            _idTarjeta = value;
            OnPropertyChanged();
        }
    }

    [ForeignKey("Cuenta")]
    public int IdCuenta
    {
        get => _idCuenta;
        set
        {
            _idCuenta = value;
            OnPropertyChanged();
        }
    }

    [ForeignKey("FormatoTarjeta")]
    public int IdFormato
    {
        get => _idFormato;
        set
        {
            _idFormato = value;
            OnPropertyChanged();
        }
    }

    [ForeignKey("TipoTarjeta")]
    public int IdTipo
    {
        get => _idTipo;
        set
        {
            _idTipo = value;
            OnPropertyChanged();
        }
    }

    [Required, MaxLength(32)]
    public string Pan
    {
        get => _pan;
        set
        {
            _pan = value;
            OnPropertyChanged();
        }
    }

    [MaxLength(20)]
    public string? Pin
    {
        get => _pin;
        set
        {
            _pin = value;
            OnPropertyChanged();
        }
    }

    public DateTime? FechaEmision
    {
        get => _fechaEmision;
        set
        {
            _fechaEmision = value;
            OnPropertyChanged();
        }
    }

    public DateTime? FechaExpiracion
    {
        get => _fechaExpiracion;
        set
        {
            _fechaExpiracion = value;
            OnPropertyChanged();
        }
    }

    [ForeignKey("Pais")]
    public int IdPaisEmision
    {
        get => _idPaisEmision;
        set
        {
            _idPaisEmision = value;
            OnPropertyChanged();
        }
    }

    public bool DdaHabilitado
    {
        get => _ddaHabilitado;
        set
        {
            _ddaHabilitado = value;
            OnPropertyChanged();
        }
    }

    public bool ArqcHabilitado
    {
        get => _arqcHabilitado;
        set
        {
            _arqcHabilitado = value;
            OnPropertyChanged();
        }
    }

    public string? Track1
    {
        get => _track1;
        set
        {
            _track1 = value;
            OnPropertyChanged();
        }
    }

    public string? Track2
    {
        get => _track2;
        set
        {
            _track2 = value;
            OnPropertyChanged();
        }
    }

    public string? Track3
    {
        get => _track3;
        set
        {
            _track3 = value;
            OnPropertyChanged();
        }
    }

    // AUDITORÍA
    public DateTime FechaCreacion { get => _fechaCreacion; set { _fechaCreacion = value; OnPropertyChanged(); } }
    public DateTime? FechaModificacion { get => _fechaModificacion; set { _fechaModificacion = value; OnPropertyChanged(); } }
    public int UsuarioCreacion { get => _usuarioCreacion; set { _usuarioCreacion = value; OnPropertyChanged(); } }
    public int? UsuarioModificacion { get => _usuarioModificacion; set { _usuarioModificacion = value; OnPropertyChanged(); } }

    // Navegación EF Core
    public virtual Cuenta Cuenta { get; set; } = null!;
    public virtual FormatoTarjeta FormatoTarjeta { get; set; } = null!;
    public virtual TipoTarjeta TipoTarjeta { get; set; } = null!;
    public virtual Pais Pais { get; set; } = null!;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}