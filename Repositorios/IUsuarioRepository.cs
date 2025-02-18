using models;

public interface IUsuarioRepository
{
    public Usuario GetUser(string username, string password);
    public void AltaUsuario(Usuario usuario);
}
