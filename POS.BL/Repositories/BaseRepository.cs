using LogicHelper.Consts;
using Microsoft.EntityFrameworkCore;
using POS.BL.Interfaces;
using POS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace POS.BL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected POSContext _context;
        public BaseRepository(POSContext context)
        {
            _context = context;
        }


        public T GetById(int id)
            => _context.Set<T>().Find(id);


        public async Task<T> GetByIdAsync(int id)
            => await _context.Set<T>().FindAsync(id);


        public IEnumerable<T> GetAll()
            => _context.Set<T>().ToList();


        public async Task<IEnumerable<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();

        public T Find(Expression<Func<T, bool>> match)
            => _context.Set<T>().FirstOrDefault(match);

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
            => await _context.Set<T>().FirstOrDefaultAsync(match);


        public T Find(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);


            return query.FirstOrDefault(match);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);


            return await query.FirstOrDefaultAsync(match);
        }


        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);


            return query.Where(match).ToList();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);


            return await query.Where(match).ToListAsync();
        }



        IEnumerable<T> IBaseRepository<T>.FindAll(Expression<Func<T, bool>> match, int? take, int? skip, Expression<Func<T, object>> orderBy, string OrderByDirection, string[] includes)
        {
            IQueryable<T> query = _context.Set<T>().Where(match);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (orderBy != null)
            {
                if (OrderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }


            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);


            return query.ToList();
        }

        async Task<IEnumerable<T>> IBaseRepository<T>.FindAllAsync(Expression<Func<T, bool>> match, int? take, int? skip, Expression<Func<T, object>> orderBy, string OrderByDirection, string[] includes)
        {
            IQueryable<T> query = _context.Set<T>().Where(match);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (orderBy != null)
            {
                if (OrderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }


            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);


            return await query.ToListAsync();
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().AddAsync(entity);
            _context.SaveChangesAsync();
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRangeAsync(entities);
            _context.SaveChangesAsync();
            return entities;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        //public async Task<T> UpdateAsync(T entity)
        //{
        //    //await _context.Update(entity);
        //    return entity;
        //}

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }


        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public int Count()
            => _context.Set<T>().Count();
    }
}
