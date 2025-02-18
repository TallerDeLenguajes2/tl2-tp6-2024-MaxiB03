namespace models;

public class Usuario
{
    int id;
    string nombre;

    string username; 
    string password; 

    AccessLevel accessLevel;


    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Username { get => username; set => username = value; }
    public string Password { get => password; set => password = value; }
    public AccessLevel AccessLevel1 { get => accessLevel; set => accessLevel = value; }

    public Usuario(){}

}

public enum AccessLevel
{
    Admin, 
    Cliente
}
