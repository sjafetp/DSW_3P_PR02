using DSW_3P_PR02.Data;
using DSW_3P_PR02.Models;
namespace DSW_3P_PR02.Repositories;

public class EventoSQLRepository : IEventoRepository
{
    private readonly AppDbContext _context;

    public EventoSQLRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Evento> ObtenerTodos()
    {
        return _context.Eventos.ToList();   
    }

    public Evento? ObtenerPorId(int id)
    {
        return _context.Eventos.Find(id);
    }

    public Evento Agregar(Evento evento)
    {
        evento.Id=0;

        _context.Eventos.Add(evento);
        _context.SaveChanges();

        return evento;
    }

    public bool Actualizar(int id, Evento evento)
    {
        var actual = _context.Eventos.Find(id);

        if(actual==null)
            return false;
        
        actual.Nombre = evento.Nombre;
        actual.Categoria = evento.Categoria;
        actual.Fecha = evento.Fecha;
        actual.Lugar = evento.Lugar;
        actual.CupoMaximo = evento.CupoMaximo;
        actual.CupoMaximo = evento.CupoMaximo;

        _context.SaveChanges();

        return true;
    }

    public bool Eliminar(int id)
    {
        var evento = _context.Eventos.Find(id);

        if(evento == null)
            return false;
        
        _context.Eventos.Remove(evento);
        _context.SaveChanges();
        
        return true;
    }
}