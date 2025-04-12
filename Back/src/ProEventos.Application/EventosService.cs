using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventosService : IEventosService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly IEventosPersistence _eventosPersistence;
        private readonly IMapper _mapper;

        public EventosService(IEventosPersistence eventosPersistence, IGeralPersistence geralPersistence, IMapper mapper)
        {
            _eventosPersistence = eventosPersistence;
            _geralPersistence = geralPersistence;
            _mapper = mapper;
        }

        public async Task<EventoDto> AddEventos(EventoDto model)
        {
            try 
            {
                var evento = _mapper.Map<Evento>(model);

                _geralPersistence.Add<Evento>(evento);
                
                if (await _geralPersistence.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventosPersistence.GetEventoByIdAsync(model.Id, false);

                    return _mapper.Map<EventoDto>(eventoRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> UpdateEventos(int eventoId, EventoDto model)
        {
            try 
            {
                var evento = await _eventosPersistence.GetEventoByIdAsync(model.Id, false);

                if (evento == null) return null;

                model.Id = evento.Id;

                _mapper.Map(model, evento);

                _geralPersistence.Update<Evento>(evento);
                
                if (await _geralPersistence.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventosPersistence.GetEventoByIdAsync(model.Id, false);

                    return _mapper.Map<EventoDto>(eventoRetorno);
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

                _geralPersistence.Delete<Evento>(evento);
                
                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try 
            {
                var eventos = await _eventosPersistence.GetAllEventosAsync(includePalestrantes);

                if (eventos == null) return null;

                var resultado = _mapper.Map<EventoDto[]>(eventos);
                
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try 
            {
                var eventos = await _eventosPersistence.GetAllEventosByTemaAsync(tema, includePalestrantes);

                if (eventos == null) return null;

                var resultado = _mapper.Map<EventoDto[]>(eventos);
                
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try 
            {
                var evento = await _eventosPersistence.GetEventoByIdAsync(eventoId, includePalestrantes);

                if (evento == null) return null;

                var resultado = _mapper.Map<EventoDto>(evento);
                
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}