using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

public class Pais : INotifyPropertyChanged
{
    private int _idPais;
    private string _nombre = string.Empty;
    private string? _codigoIso2;
    private DateTime _fechaCreacion = DateTime.UtcNow;
    private DateTime? _fechaModificacion;
    private int _usuarioCreacion = 3;
    private int? _usuarioModificacion;

    [Key]
    public int IdPais
    {
        get => _idPais;
        set
        {
            _idPais = value;
            OnPropertyChanged();
        }
    }

    [Required, MaxLength(100)]
    public string Nombre
    {
        get => _nombre;
        set
        {
            _nombre = value;
            OnPropertyChanged();
        }
    }

    [MaxLength(2)]
    public string? CodigoIso2
    {
        get => _codigoIso2;
        set
        {
            _codigoIso2 = value;
            OnPropertyChanged();
        }
    }

    // AUDITORÍA
    public DateTime FechaCreacion { get => _fechaCreacion; set { _fechaCreacion = value; OnPropertyChanged(); } }
    public DateTime? FechaModificacion { get => _fechaModificacion; set { _fechaModificacion = value; OnPropertyChanged(); } }
    public int UsuarioCreacion { get => _usuarioCreacion; set { _usuarioCreacion = value; OnPropertyChanged(); } }
    public int? UsuarioModificacion { get => _usuarioModificacion; set { _usuarioModificacion = value; OnPropertyChanged(); } }

    // Navegación EF Core
    public virtual ICollection<Tarjeta>? Tarjetas { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}