using AutoMapper;
using BookTravel.APi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookTravel.APi.Sevrices
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public BaseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> CreateAsync(T Model)
        {
            await _context.AddAsync(Model);
            _context.SaveChanges();

            return Model;
        }
        public virtual async Task<T?> GetAsync(object Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }
        public virtual async Task<IEnumerable<T>> GetAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public virtual T Update(T Model)
        {
            _context.Update(Model);
            _context.SaveChanges();

            return Model;
        }
        public virtual T Delete(T Model)
        {
            _context.Remove(Model);
            _context.SaveChanges();

            return Model;
        }
        public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> Criteria, Expression<Func<T, object>> Include)
        {
            return await _context.Set<T>().Include(Include).SingleOrDefaultAsync(Criteria);
        }
        public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> Criteria)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(Criteria);
        }
        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, object>> Include)
        {
            return await _context.Set<T>().Include(Include).ToListAsync();
        }
    }
}
