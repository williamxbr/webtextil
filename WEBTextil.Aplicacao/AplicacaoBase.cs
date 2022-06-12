using WEBTextil.Dominio.Interfaces.Aplicacao;
using WEBTextil.Dominio.Interfaces.Respositorios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEBTextil.Aplicacao
{
    public class AplicacaoBase<TEntity> : IDisposable, IAplicacaoBase<TEntity> where TEntity : class
    {

        private readonly IRepositorioBase<TEntity> _repositorioBase;

        public AplicacaoBase(IRepositorioBase<TEntity> repositorioBase)
        {
            _repositorioBase = repositorioBase;
        }

        public void Adicionar(TEntity entidade)
        {
            _repositorioBase.Adicionar(entidade);
        }

        public void AdicionarEmLote(List<TEntity> listaEntidade)
        {
            _repositorioBase.AdicionarEmLote(listaEntidade);
        }

        public void Atualizar(TEntity entidade)
        {
            _repositorioBase.Atualizar(entidade);
        }

        public TEntity BuscarId(int id)
        {
            return _repositorioBase.BuscarId(id);
        }

        public TEntity BuscarId(Guid id)
        {
            return _repositorioBase.BuscarId(id);
        }

        public TEntity BuscarId(long id)
        {
            return _repositorioBase.BuscarId(id);
        }

        public IEnumerable<TEntity> BuscarTodos()
        {
            return _repositorioBase.BuscarTodos();
        }

        public IEnumerable<TEntity> Consultar(Func<TEntity, bool> lambda)
        {
            return _repositorioBase.Consultar(lambda);
        }

        public void Dispose()
        {
            _repositorioBase.Dispose();
        }

        public IEnumerable<TEntity> Pesquisar(object where)
        {
            return _repositorioBase.Pesquisar(where);
        }

        public void Remover(TEntity entidade)
        {
            _repositorioBase.Remover(entidade);
        }

        public void Remover(int id)
        {
            _repositorioBase.Remover(id);
        }

        public async Task AdicionarAsync(TEntity entidade)
        {
            await _repositorioBase.AdicionarAsync(entidade);
        }

        public async Task AdicionarEmLoteAsync(List<TEntity> listaEntidade)
        {
            await _repositorioBase.AdicionarEmLoteAsync(listaEntidade);
        }

        public async Task<TEntity> BuscarIdAsync(int id)
        {
            return await _repositorioBase.BuscarIdAsync(id);
        }

        public async Task<TEntity> BuscarIdAsync(Guid id)
        {
            return await _repositorioBase.BuscarIdAsync(id);
        }

        public async Task<TEntity> BuscarIdAsync(long id)
        {
            return await _repositorioBase.BuscarIdAsync(id);
        }

        public async Task<IEnumerable<TEntity>> BuscarTodosAsync()
        {
            return await _repositorioBase.BuscarTodosAsync();
        }

        public async Task<IEnumerable<TEntity>> PesquisarAsync(object where)
        {
            return await _repositorioBase.PesquisarAsync(where);
        }

        public async Task<IEnumerable<TEntity>> ConsultarAsync(Func<TEntity, bool> lambda)
        {
            return await _repositorioBase.ConsultarAsync(lambda);
        }

        public async Task AtualizarAsync(TEntity entidade)
        {
            await _repositorioBase.AtualizarAsync(entidade);
        }

        public async Task RemoverAsync(TEntity entidade)
        {
            await _repositorioBase.RemoverAsync(entidade);
        }

        public async Task RemoverAsync(int id)
        {
            await _repositorioBase.RemoverAsync(id);
        }
    }
}
