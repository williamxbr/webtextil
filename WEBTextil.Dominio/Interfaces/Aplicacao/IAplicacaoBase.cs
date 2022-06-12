using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBTextil.Dominio.Interfaces.Aplicacao
{
    public interface IAplicacaoBase<TEntity> where TEntity : class
    {
        void Adicionar(TEntity entidade);
        void AdicionarEmLote(List<TEntity> listaEntidade);
        TEntity BuscarId(int id);
        TEntity BuscarId(Guid id);
        TEntity BuscarId(long id);
        IEnumerable<TEntity> BuscarTodos();
        IEnumerable<TEntity> Pesquisar(object where);
        IEnumerable<TEntity> Consultar(Func<TEntity, bool> lambda);
        void Atualizar(TEntity entidade);
        void Remover(TEntity entidade);
        void Remover(int id);

        Task AdicionarAsync(TEntity entidade);
        Task AdicionarEmLoteAsync(List<TEntity> listaEntidade);
        Task<TEntity> BuscarIdAsync(int id);
        Task<TEntity> BuscarIdAsync(Guid id);
        Task<TEntity> BuscarIdAsync(long id);
        Task<IEnumerable<TEntity>> BuscarTodosAsync();
        Task<IEnumerable<TEntity>> PesquisarAsync(object where);
        Task<IEnumerable<TEntity>> ConsultarAsync(Func<TEntity, bool> lambda);
        Task AtualizarAsync(TEntity entidade);
        Task RemoverAsync(TEntity entidade);
        Task RemoverAsync(int id);
        void Dispose();
    }
}
