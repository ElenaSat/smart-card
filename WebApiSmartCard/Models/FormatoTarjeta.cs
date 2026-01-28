using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

public class FormatoTarjeta : INotifyPropertyChanged
{
    private int _idFormato;
    private string _nombre = string.Empty;
    private DateTime _fechaCreacion = DateTime.UtcNow;
    private DateTime? _fechaModificacion;
    private int _usuarioCreacion = 3;
    private int? _usuarioModificacion;

    [Key]
    public int IdFormato
    {
        get => _idFormato;
        set
        {
            _idFormato = value;
            OnPropertyChanged();
        }
    }

    [Required, MaxLength(50)]
    public string Nombre
    {
        get => _nombre;
        set
        {
            _nombre = value;
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