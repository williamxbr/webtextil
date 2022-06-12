using WEBTextil.Data.Contexto;
using WEBTextil.Dominio.Interfaces.Respositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBTextil.Data.Repositorios
{
    public class RepositorioBase<TEntity> : ConnectDB, IRepositorioBase<TEntity> where TEntity : class
    {
        public void Adicionar(TEntity entidade)
        {
            Insert(entidade);
        }

        public void AdicionarEmLote(List<TEntity> listaEntidade)
        {
            foreach(var entidade in listaEntidade)
            {
                Insert(entidade);
            }
        }

        public async Task AdicionarAsync(TEntity entidade)
        {
            await InsertAsync(entidade);
        }

        public async Task AdicionarEmLoteAsync(List<TEntity> listaEntidade)
        {
            await AdicionarEmLoteAsync(listaEntidade);
        }

        public void Atualizar(TEntity entidade)
        {
            Update(entidade);
        }

        public async Task AtualizarAsync(TEntity entidade)
        {
            await UpdateAsync(entidade);
        }

        public TEntity BuscarId(int id)
        {
            return Find<TEntity>(id);
        }

        public TEntity BuscarId(Guid id)
        {
            return Find<TEntity>(id);
        }

        public TEntity BuscarId(long id)
        {
            return Find<TEntity>(id);
        }

        public async Task<TEntity> BuscarIdAsync(int id)
        {
            return await FindAsync<TEntity>(id);
        }

        public async Task<TEntity> BuscarIdAsync(Guid id)
        {
            return await FindAsync<TEntity>(id);
        }

        public async Task<TEntity> BuscarIdAsync(long id)
        {
            return await FindAsync<TEntity>(id);
        }

        public async Task<IEnumerable<TEntity>> BuscarTodosAsync()
        {
            return await GetAllAsync<TEntity>();
        }

        public IEnumerable<TEntity> BuscarTodos()
        {
            return GetAll<TEntity>();
        }

        public IEnumerable<TEntity> Consultar(Func<TEntity, bool> lambda)
        {
            var enumerable = GetAll<TEntity>();
            return enumerable.Where(lambda);
        }

        public IEnumerable<TEntity> Pesquisar(object where)
        {
            return Select<TEntity>(where);
        }

        public async Task<IEnumerable<TEntity>> ConsultarAsync(Func<TEntity, bool> lambda)
        {
            var enumerable = GetAllAsync<TEntity>();
            return await Task.FromResult(enumerable.Result.Where(lambda));
        }

        public void Remover(TEntity entidade)
        {
            Delete(entidade);
        }

        public void Remover(int id)
        {
            var entidade = Find<TEntity>(id);
            Delete(entidade);
        }

        public async Task RemoverAsync(TEntity entidade)
        {
            await DeleteAsync(entidade);
        }

        public async Task RemoverAsync(int id)
        {
            var entidade = await FindAsync<TEntity>(id);
            await DeleteAsync(entidade);
        }

        public async Task<IEnumerable<TEntity>> PesquisarAsync(object where)
        {
            return await SelectAsync<TEntity>(where);
        }

        //private static string GetPropertyName<T>(System.Linq.Expressions.Expression<Func<T, bool>> property)
        //{
        //    System.Linq.Expressions.LambdaExpression lambda = (System.Linq.Expressions.LambdaExpression)property;
        //    System.Linq.Expressions.MemberExpression memberExpression;

        //    if (lambda.Body is System.Linq.Expressions.UnaryExpression)
        //    {
        //        System.Linq.Expressions.UnaryExpression unaryExpression = (System.Linq.Expressions.UnaryExpression)(lambda.Body);
        //        memberExpression = (System.Linq.Expressions.MemberExpression)(unaryExpression.Operand);
        //    }
        //    else
        //    {
        //        memberExpression = (System.Linq.Expressions.MemberExpression)(lambda.Body);
        //    }

        //    return ((PropertyInfo)memberExpression.Member).Name;
        //}

        //public static string GetMemberName<T>(this Expression<T> expression) =>
        //          expression.Body switch
        //          {
        //              MemberExpression m => m.Member.Name,
        //              UnaryExpression { Operand: MemberExpression m } => m.Member.Name,
        //              _ => throw new NotImplementedException(expression.GetType().ToString())
        //          };

    }
}
