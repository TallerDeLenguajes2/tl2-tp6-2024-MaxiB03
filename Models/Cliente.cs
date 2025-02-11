namespace models;
using System.ComponentModel.DataAnnotations;

public class Cliente
{
    int idCliente;
    string nombre;
    string email;
    string telefono;

    public int IdCliente { get => idCliente; set => idCliente = value; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get => nombre; set => nombre = value; }

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string Email { get => email; set => email = value; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [Phone(ErrorMessage = "Formato de teléfono inválido")]
    public string Telefono { get => telefono; set => telefono = value; }

    public Cliente(){}

    public Cliente(string nombre, string email, string telefono)
    {
        Nombre=nombre;
        Email=email;
        Telefono=telefono;
    }

    public Cliente(int id, string nombre, string email, string telefono)
    {
        IdCliente=id;
        Nombre=nombre;
        Email=email;
        Telefono=telefono;
    }
}