using System.Linq.Expressions;

namespace DataAccess.IRepository
{
    public interface IRepository <T> where T : class
    {
        public IQueryable<T> GetAll(Expression<Func<T, object>>[]? includeProp = null, Expression<Func<T, bool>>? expression = null, bool tracked = true);
        public IQueryable<TResult> GetAll<TLink, TResult>(
   Expression<Func<TLink, bool>>? linkCondition,
   Expression<Func<TLink, TResult>> selector,
   Expression<Func<TResult, object>>[]? includeProps = null,
   bool tracked = true
)
   where TLink : class
   where TResult : class
; 

        public IQueryable<T> GetOne(Expression<Func<T, object>>[]? includeProp = null, Expression<Func<T, bool>>? expression = null, bool tracked = true);
        void Add(T category);
        void Edit(T category);
        void Delete(T category);
        void Commit();

    }
}
