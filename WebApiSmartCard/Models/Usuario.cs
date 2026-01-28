
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

public class Usuario : INotifyPropertyChanged
{
    private int _idUsuario;
    private string? _titulo;
    private string _nombre = string.Empty;
    private string _apellido = string.Empty;
    private string? _infoExtra;
    private DateTime _fechaCreacion = DateTime.UtcNow;
    private DateTime? _fechaModificacion;
    private int _usuarioCreacion = 3;
    private int? _usuarioModificacion;

    [Key]
    public int IdUsuario
    {
        get => _idUsuario;
        set
        {
            _idUsuario = value;
            OnPropertyChanged();
        }
    }

    [MaxLength(20)]
    public string? Titulo
    {
        get => _titulo;
        set
        {
            _titulo = value;
            OnPropertyChanged();
        }
    }

    [Required, MaxLength(40)]
    public string Nombre
    {
        get => _nombre;
        set
        {
            _nombre = value;
            OnPropertyChanged();
        }
    }

    [Required, MaxLength(40)]
    public string Apellido
    {
        get => _apellido;
        set
        {
            _apellido = value;
            OnPropertyChanged();
        }
    }

    public string? InfoExtra
    {
        get => _infoExtra;
        set
        {
            _infoExtra = value;
            OnPropertyChanged();
        }
    }

    // AUDITORÍA
    public DateTime FechaCreacion { get => _fechaCreacion; set { _fechaCreacion = value; OnPropertyChanged(); } }
    public DateTime? FechaModificacion { get => _fechaModificacion; set { _fechaModificacion = value; OnPropertyChanged(); } }
    public int UsuarioCreacion { get => _usuarioCreacion; set { _usuarioCreacion = value; OnPropertyChanged(); } }
    public int? UsuarioModificacion { get => _usuarioModificacion; set { _usuarioModificacion = value; OnPropertyChanged(); } }

    // Navegación EF Core
    public virtual ICollection<Cuenta>? Cuentas { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
