using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using WebAPICigam.Model;
using WebAPICigam.Model.Shared.Request;
using WebAPICigam.Repositorio;
using WebAPICigam.Services.Interface;

namespace WebAPICigam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {       
        private readonly ITarefa _tarefaService;        

        public TarefaController(ITarefa tarefaService)
        {
            _tarefaService = tarefaService;
        }

        /// <summary>
        /// Obtem uma tarefa
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpGet("GetTarefa")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetTarefa([Required] int codigo)
        {
            var response = await _tarefaService.GetTarefa(codigo);

            return Ok(response);
        }

        /// <summary>
        /// Obtém uma lista de tarefas
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTarefas")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetTarefas()
        {
            var response = await _tarefaService.GetTarefas();

            return Ok(response);
        }

        /// <summary>
        /// Cria uma nova tarefa
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CreateTarefa")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateTarefa([Required] TarefaRequest request)
        {           
            var response = await _tarefaService.CreateTarefa(request);

            return Ok(response);
        }

        /// <summary>
        /// Atualiza uma tarefa existente
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("UpdateTarefa")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateTarefa([Required] TarefaRequest request)
        {
            var response = await _tarefaService.UpdateTarefa(request);

            return Ok(response);
        }
    }
}
