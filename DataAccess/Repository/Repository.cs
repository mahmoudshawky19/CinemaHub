 
using DataAccess;
using DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CinemaHub.Repository
{
    public class Repository <T>: IRepository <T> where T : class
    {
        ApplicationDbContext dbContext;// = new ApplicationDbContext();
        DbSet<T> dbSet;
        private ApplicationDbContext dbContext1;

        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

 

        // CRUD
        public IQueryable<T> GetAll(Expression<Func<T, object>>[]? includeProp = null, Expression<Func<T, bool>>? expression = null, bool tracked = true )
        {
            IQueryable<T> query = dbSet;
            if (includeProp != null)
            {
                foreach (var item in includeProp)
                {
                    query = query.Include(item);

                }
            }
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
         
            return query ;
        }

        public IQueryable<T> GetOne(Expression<Func<T, object>>[]? includeProp = null, Expression<Func<T, bool>>? expression = null, bool tracked = true)
        {
            return GetAll(includeProp, expression, tracked);
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Edit(T entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }
        public IQueryable<TResult> GetAll<TLink, TResult>(
    Expression<Func<TLink, bool>>? linkCondition,
    Expression<Func<TLink, TResult>> selector,
    Expression<Func<TResult, object>>[]? includeProps = null,
    bool tracked = true
)
    where TLink : class
    where TResult : class
        {
            // استعلام جدول الربط وتطبيق شرط الربط
            IQueryable<TLink> linkQuery = dbContext.Set<TLink>().Where(linkCondition);

            // استخدام selector لجلب الكائنات المرتبطة
            IQueryable<TResult> resultQuery = linkQuery.Select(selector);

            // تضمين الخصائص المطلوبة إذا كانت موجودة
            if (includeProps != null)
            {
                foreach (var includeProp in includeProps)
                {
                    resultQuery = resultQuery.Include(includeProp);
                }
            }

            // تحديد الاستعلام كغير متعقب إذا كان tracked = false
            if (!tracked)
            {
                resultQuery = resultQuery.AsNoTracking();
            }

            return resultQuery ;
        }
 

    }
}
