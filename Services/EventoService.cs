using DSW_3P_PR02.Models;
using DSW_3P_PR02.Repositories;

namespace DSW_3P_PR02.Services;

public class EventoService : IEventoService
{
    private readonly IEventoRepository _repository;

    public EventoService(IEventoRepository repository)
    {
        _repository = repository;
    }

    public List<Evento> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public Evento? ObtenerPorId(int id)
    {
        List<Evento> Eventos = _repository.ObtenerTodos();
        if(!Eventos.Any(e => e.Id == id))
        {
            return null;
        }

        return _repository.ObtenerPorId(id);
    }

    public Evento Agregar(Evento nuevo)
    {
        if(string.IsNullOrWhiteSpace(nuevo.Nombre))
            throw new ArgumentException("El nombre es obligatorio");

        if(string.IsNullOrWhiteSpace(nuevo.Lugar))
            throw new ArgumentException("El lugar es obligatorio");

        if(nuevo.CupoMaximo < 0)
            throw new ArgumentException("El cupo no puede ser menor a 0");
        
        return _repository.Agregar(nuevo);
    }

    public bool Actualizar(int id, Evento actualizado)
    {
        
        if(_repository.ObtenerPorId(id) == null)
            throw new ArgumentException("El evento no existe");

        if(string.IsNullOrWhiteSpace(actualizado.Nombre))
            throw new ArgumentException("El nombre es obligatorio");

        if(string.IsNullOrWhiteSpace(actualizado.Lugar))
            throw new ArgumentException("El lugar es obligatorio");

        if(actualizado.CupoMaximo < 0)
            throw new ArgumentException("El cupo no puede ser menor a 0");

        return _repository.Actualizar(id, actualizado);
    }

    public bool Eliminar(int id)
    {
        if(_repository.ObtenerPorId(id) == null)
            throw new ArgumentException("El evento no existe");
            
        return _repository.Eliminar(id);
    }
}