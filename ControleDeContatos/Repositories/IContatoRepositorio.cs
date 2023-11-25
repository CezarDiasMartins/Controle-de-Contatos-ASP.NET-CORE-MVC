using ControleDeContatos.Models;

namespace ControleDeContatos.Repositories
{
    public interface IContatoRepositorio
    {
        // Buscar todos contatos que pertencem a determinado usuário
        // Pela chave estrangeira de Usuários que está em Contatos
        List<ContatoModel> BuscarTodos(int usuarioId);
        ContatoModel BuscarPorId(int id);
        ContatoModel Adicionar(ContatoModel contato);
        ContatoModel Atualizar(ContatoModel contato);
        bool Apagar(int id);
    }
}
