using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

public class Cuenta : INotifyPropertyChanged
{
    private int _idCuenta;
    private string _numero = string.Empty;
    private int _idUsuario;
    private DateTime _fechaCreacion = DateTime.UtcNow;
    private DateTime? _fechaModificacion;
    private int _usuarioCreacion = 3;
    private int? _usuarioModificacion;

    [Key]
    public int IdCuenta
    {
        get => _idCuenta;
        set
        {
            _idCuenta = value;
            OnPropertyChanged();
        }
    }

    [Required, MaxLength(50)]
    public string Numero
    {
        get => _numero;
        set
        {
            _numero = value;
            OnPropertyChanged();
        }
    }

    [ForeignKey("Usuario")]
    public int IdUsuario
    {
        get => _idUsuario;
        set
        {
            _idUsuario = value;
            OnPropertyChanged();
        }
    }

    // AUDITOR�A
    public DateTime FechaCreacion { get => _fechaCreacion; set { _fechaCreacion = value; OnPropertyChanged(); } }
    public DateTime? FechaModificacion { get => _fechaModificacion; set { _fechaModificacion = value; OnPropertyChanged(); } }
    public int UsuarioCreacion { get => _usuarioCreacion; set { _usuarioCreacion = value; OnPropertyChanged(); } }
    public int? UsuarioModificacion { get => _usuarioModificacion; set { _usuarioModificacion = value; OnPropertyChanged(); } }

    // Navegaci�n EF Core
    public virtual Usuario? Usuario { get; set; }
    public virtual ICollection<Tarjeta>? Tarjetas { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
