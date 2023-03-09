using WebAPICigam.Model;

namespace WebAPICigam.Repositorio
{
    public interface ITarefaRepositorio
    {
        IEnumerable<Tarefa> GetAllTarefas();

        bool Add(Tarefa data);
    }
}
