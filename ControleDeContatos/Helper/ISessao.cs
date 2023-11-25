using ControleDeContatos.Models;

namespace ControleDeContatos.Helper
{
    // Cookies
    public interface ISessao
    {
        void CriarSessaoDoUsuario(UsuarioModel usuario);
        void RemoverSessaoUsuario();
        UsuarioModel BuscarSessaoDoUsuario();
    }
}
