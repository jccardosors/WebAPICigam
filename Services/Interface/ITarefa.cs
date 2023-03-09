using WebAPICigam.Model;
using WebAPICigam.Model.Shared.Request;
using WebAPICigam.Model.Shared.Response;
using WebAPICigam.Repositorio;

namespace WebAPICigam.Services.Interface
{
    public interface ITarefa
    {
        Task<ServiceResponse<TarefaResponse>> CreateTarefa(TarefaRequest data);

        Task<ServiceResponse<TarefaResponse>> UpdateTarefa(TarefaRequest data);

        Task<ServiceResponse<TarefaResponse>> GetTarefa(int codigo);

        Task<ServiceResponse<List<TarefaResponse>>> GetTarefas();
    }
}
