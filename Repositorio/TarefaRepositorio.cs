using WebAPICigam.Model;

namespace WebAPICigam.Repositorio
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private List<Tarefa> todasTarefas = new List<Tarefa>();      

        public TarefaRepositorio()
        {
          
        }

        public bool Add(Tarefa data)
        {
            todasTarefas.Add(data); 
            return true;
        }

        public IEnumerable<Tarefa> GetAllTarefas()
        {
            return todasTarefas;
        }
    }
}
