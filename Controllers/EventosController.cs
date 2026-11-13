using Microsoft.AspNetCore.Mvc;
using DSW_3P_PR02.Models;
using DSW_3P_PR02.Services;

namespace DSW_3P_PR02.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly IEventoService _service;

    public EventosController(IEventoService service)
    {
        _service = service;
    }

    //GET - Obtener todos los eventos
    [HttpGet]
    [ProducesResponseType(typeof(List<Evento>),200)]
    public IActionResult ObtenerTodos()
    {
        return Ok(_service.ObtenerTodos());
    }

    //GET - Obtener evento por Id
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Evento),200)]
    [ProducesResponseType(404)]
    public IActionResult ObtenerPorId(int id)
    {
        var evento = _service.ObtenerPorId(id);
        if(evento == null)
            return NotFound();

        return Ok(evento);
    }

    //POST - Agregar nuevo evento
    [HttpPost]
    [ProducesResponseType(typeof(Evento),200)]
    [ProducesResponseType(404)]
    public IActionResult Agregar(Evento nuevo)
    {
        try
        {
            var creado = _service.Agregar(nuevo);

            return CreatedAtAction(nameof(ObtenerPorId), new{id = creado.Id}, creado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new{mensaje=ex.Message});
        }

    }

    //PUT - Actualizar un evento
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)] //No content
    [ProducesResponseType(404)] //Not found
    [ProducesResponseType(400)] //Bad request
    public IActionResult Actualizar(int id, Evento actualizado)
    {
        try
        {
            bool resultado = _service.Actualizar(id, actualizado);
            if(!resultado)
                return NotFound();

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new{mensaje=ex.Message});
        }
    }

    //DELETE - Eliminar un evento
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult Eliminar(int id)
    {
        bool resultado = _service.Eliminar(id);
        if(!resultado)
            return NotFound();

        return NoContent();
    }
}