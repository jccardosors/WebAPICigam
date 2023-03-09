using Microsoft.Extensions.Caching.Memory;
using System.Runtime.ConstrainedExecution;
using WebAPICigam.Model;
using WebAPICigam.Model.Shared.Request;
using WebAPICigam.Model.Shared.Response;
using WebAPICigam.Repositorio;
using WebAPICigam.Services.Interface;

namespace WebAPICigam.Services.Implementation
{
    public class TarefaService : ITarefa
    {      
        static readonly ITarefaRepositorio tarefasRepositorio = new TarefaRepositorio();

        public TarefaService()
        {
            
        }

        /// <summary>
        /// CreateTarefa
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<TarefaResponse>> CreateTarefa(TarefaRequest data)
        {
            var response = new ServiceResponse<TarefaResponse>();
            try
            {
                var mensagemErro = string.Empty;

                if (string.IsNullOrEmpty(data.Data.Codigo.ToString()))
                {
                    mensagemErro = "Código é um campo obrigatório.";
                }
                else if (string.IsNullOrEmpty(data.Data.Descricao))
                {
                    mensagemErro = "Descrição é um campo obrigatório.";
                }
                else if (EhCodigoJaCadastrado(data.Data.Codigo))
                {
                    mensagemErro = "Código já cadastrado!";
                }

                if (!string.IsNullOrEmpty(mensagemErro))
                {
                    response.StatusOk = false;
                    response.Result = null;
                    response.MessageError = mensagemErro;
                }
                else
                {
                    response.StatusOk = true;
                    response.Result = new TarefaResponse
                    {
                        Data = new Tarefa
                        {
                            Codigo = data.Data.Codigo,
                            Descricao = data.Data.Descricao,
                            Status = 'P',
                        }
                    };

                    tarefasRepositorio.Add(new Tarefa
                    {
                        Codigo = data.Data.Codigo,
                        Descricao = data.Data.Descricao,
                        Status = 'P',
                    });
                }
            }
            catch (Exception ex)
            {
                response.StatusOk = false;
                response.Result = null;
                response.MessageError = ex.GetBaseException().Message;
            }

            return response;
        }

        /// <summary>
        /// GetTarefa
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<TarefaResponse>> GetTarefa(int codigo)
        {
            var response = new ServiceResponse<TarefaResponse>();
            try
            {
                var tarefa = tarefasRepositorio.GetAllTarefas().FirstOrDefault(p => p.Codigo == codigo);
                if (tarefa == null || tarefa.Codigo <= 0)
                {
                    response.StatusOk = false;
                    response.Result = null;
                    response.MessageError = "Tarefa não encontrada.";
                }
                else
                {
                    response.StatusOk = true;
                    response.Result = new TarefaResponse
                    {
                        Data = new Tarefa
                        {
                            Codigo = tarefa.Codigo,
                            Descricao = tarefa.Descricao,
                            Status = tarefa.Status,
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                response.StatusOk = false;
                response.Result = null;
                response.MessageError = ex.GetBaseException().Message;
            }

            return response;
        }

        /// <summary>
        /// GetTarefas
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<List<TarefaResponse>>> GetTarefas()
        {
            var response = new ServiceResponse<List<TarefaResponse>>();
            try
            {
                response.StatusOk = true;
                response.Result = new List<TarefaResponse>();

                var todasTarefasList = tarefasRepositorio.GetAllTarefas().ToList();
                foreach (var item in todasTarefasList)
                {
                    response.Result.Add(new TarefaResponse
                    {
                        Data = new Tarefa
                        {
                            Codigo = item.Codigo,
                            Descricao = item.Descricao,
                            Status = item.Status,
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                response.StatusOk = false;
                response.Result = null;
                response.MessageError = ex.GetBaseException().Message;
            }

            return response;
        }

        /// <summary>
        /// UpdateTarefa
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<TarefaResponse>> UpdateTarefa(TarefaRequest data)
        {
            var response = new ServiceResponse<TarefaResponse>();
            try
            {
                var mensagemErro = string.Empty;

                if (string.IsNullOrEmpty(data.Data.Codigo.ToString()))
                {
                    mensagemErro = "Código é um campo obrigatório.";
                }
                else if (string.IsNullOrEmpty(data.Data.Descricao))
                {
                    mensagemErro = "Descrição é um campo obrigatório.";
                }

                var tarefa = await GetTarefa(data.Data.Codigo);
                if (!tarefa.StatusOk)
                {
                    response.StatusOk = false;
                    response.Result = tarefa.Result;
                    response.MessageError = tarefa.MessageError;
                }
                else
                {
                    response.StatusOk = true;
                    response.Result = new TarefaResponse
                    {
                        Data = new Tarefa
                        {
                            Codigo = tarefa.Result.Data.Codigo,
                            Descricao = data.Data.Descricao,
                            Status = 'C',
                        }
                    };

                    tarefasRepositorio.GetAllTarefas().First(p => p.Codigo == tarefa.Result.Data.Codigo).Status = 'C';
                    tarefasRepositorio.GetAllTarefas().First(p => p.Codigo == tarefa.Result.Data.Codigo).Descricao = data.Data.Descricao;
                }
            }
            catch (Exception ex)
            {
                response.StatusOk = false;
                response.Result = null;
                response.MessageError = ex.GetBaseException().Message;
            }

            return response;
        }

        private bool EhCodigoJaCadastrado(int codigo)
        {
            return tarefasRepositorio.GetAllTarefas().Any(p => p.Codigo == codigo);
        }
    }
}
