using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Persistence;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;
       
        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;         
        }

        [HttpGet]
        public async Task <IActionResult> Get()
        {
            try
            {
                 var eventos = await _eventoService.GetAllEventosAsync(true);
                 if (eventos == null) return NotFound("Nenhum Evento Encontrado");

                 return Ok(eventos);
            }
            catch (Exception ex)
            {
               return this.StatusCode(StatusCodes.Status500InternalServerError, 
               $"Erro ao tentar recuperar todos eventos. Erro: {ex.Message}");
            }
   
          
        }

       [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            try
            {
                 var evento = await _eventoService.GetEventoByIdAsync(id, true);
                 if (evento == null) return NotFound("Nenhum Evento por Id Encontrado");

                 return Ok(evento);
            }
            catch (Exception ex)
            {
               return this.StatusCode(StatusCodes.Status500InternalServerError, 
               $"Erro ao tentar recuperar evento por id. Erro: {ex.Message}");
            }
          
        }
       [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetbyTema(string tema)
        {
            try
            {
                 var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                 if (evento == null) return NotFound("Nenhum Evento por tema Encontrado");

                 return Ok(evento);
            }
            catch (Exception ex)
            {
               return this.StatusCode(StatusCodes.Status500InternalServerError, 
               $"Erro ao tentar recuperar eventos por tema. Erro: {ex.Message}");
            }
          
        }

        [HttpPost]
        public async Task<ActionResult> Post(Evento model)
        {
            try
            {
                 var evento = await _eventoService.AddEventos(model);
                 if (evento == null) return BadRequest("Erro ao tentar adicionar evento.");

                 return Ok(evento);
            }
            catch (Exception ex)
            {
               return this.StatusCode(StatusCodes.Status500InternalServerError, 
               $"Erro ao tentar adicionar evento. Erro: {ex.Message}");
            }
          
        }

         [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Evento model)
        {
             try
            {
                 var evento = await _eventoService.UpdateEvento(id, model);
                 if (evento == null) return BadRequest("Erro ao tentar adicionar evento.");

                 return Ok(evento);
            }
            catch (Exception ex)
            {
               return this.StatusCode(StatusCodes.Status500InternalServerError, 
               $"Erro ao tentar Atualizar evento. Erro: {ex.Message}");
            }
        }

         [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, Evento model)
        {
            try
            {
               return await _eventoService.DeleteEvento(id) ? Ok("Deletado") : BadRequest("Evento não deletado");        
            }
            catch (Exception ex)
            {
               return this.StatusCode(StatusCodes.Status500InternalServerError, 
               $"Erro ao tentar deletar evento. Erro: {ex.Message}");
            }
        }


    }
}
