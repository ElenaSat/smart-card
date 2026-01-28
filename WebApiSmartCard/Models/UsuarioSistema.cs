using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

public class UsuarioSistema : INotifyPropertyChanged
{
    private int _idUsuarioSistema;
    private string _nombreUsuario = string.Empty;
    private DateTime _fechaCreacion;

    [Key]
    public int IdUsuarioSistema
    {
        get => _idUsuarioSistema;
        init
        {
            _idUsuarioSistema = value;
            OnPropertyChanged();
        }
    }

    [Required, MaxLength(100)]
    public string NombreUsuario
    {
        get => _nombreUsuario;
        set
        {
            _nombreUsuario = value;
            OnPropertyChanged();
        }
    }

    public DateTime FechaCreacion
    {
        get => _fechaCreacion;
        init
        {
            _fechaCreacion = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}