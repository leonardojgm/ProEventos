using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventosService : IEventosService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly IEventosPersistence _eventosPersistence;

        public EventosService(IEventosPersistence eventosPersistence, IGeralPersistence geralPersistence)
        {
            _eventosPersistence = eventosPersistence;
            _geralPersistence = geralPersistence;
        }

        public async Task<Evento> AddEventos(Evento model)
        {
            try 
            {
                _geralPersistence.Add(model);
                
                if (await _geralPersistence.SaveChangesAsync())
                {
                    return await _eventosPersistence.GetEventoByIdAsync(model.Id, false);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEventos(int eventoId, Evento model)
        {
            try 
            {
                var evento = await _eventosPersistence.GetEventoByIdAsync(model.Id, false);

                if (evento == null) return null;

                model.Id = evento.Id;

                _geralPersistence.Update(model);
                
                if (await _geralPersistence.SaveChangesAsync())
                {
                    return await _eventosPersistence.GetEventoByIdAsync(model.Id, false);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEventos(int eventoId)
        {
            try 
            {
                var evento = await _eventosPersistence.GetEventoByIdAsync(eventoId, false);

                if (evento == null) throw new Exception("Evento para delete não encontrado.");

                _geralPersistence.Delete(evento);
                
                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try 
            {
                var eventos = await _eventosPersistence.GetAllEventosAsync(includePalestrantes);

                if (eventos == null) return null;
                
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try 
            {
                var eventos = await _eventosPersistence.GetAllEventosByTemaAsync(tema, includePalestrantes);

                if (eventos == null) return null;
                
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try 
            {
                var evento = await _eventosPersistence.GetEventoByIdAsync(eventoId, includePalestrantes);

                if (evento == null) return null;
                
                return evento;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}