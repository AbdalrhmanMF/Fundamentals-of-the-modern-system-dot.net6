using LogicHelper.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace POS.BL.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);


        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();


        T Find(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);


        T Find(Expression<Func<T, bool>> match, string[] includes = null);
        Task<T> FindAsync(Expression<Func<T, bool>> match, string[] includes = null);


        IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, string[] includes = null);


        IEnumerable<T> FindAll(Expression<Func<T, bool>> match , int? take , int? skip,
            Expression<Func<T, object>> orderBy = null , string OrderByDirection = OrderBy.Ascending
            , string[] includes = null);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, int? take , int? skip,
            Expression<Func<T, object>> orderBy = null , string OrderByDirection = OrderBy.Ascending
            , string[] includes = null);


        T Add(T entity);
        Task<T> AddAsync(T entity);


        IEnumerable<T> AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);


        T Update(T entity);

        //Task<T> UpdateAsync(T entity);

        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        void Attach(T entity);

        int Count();

    }
}
