using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface ILotePersist
    {
        /// <summary>
        /// Método get que retornará uma lista de lotes por eventoId
        /// </summary>
        /// <param name="eventoId">Código chave da tabela Evento</param>
        /// <returns>Array de lotes</returns>
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);

        /// <summary>
        /// Método get que retornará apenas 1 lote
        /// </summary>
        /// <param name="eventoId">Código chave da tabela Evento</param>
        /// <param name="id">Código chave da tabela lote</param>
        /// <returns>Apenas 1 lote</returns>
        Task<Lote> GetLoteByIdsAsync(int eventoId, int id);
    }
}