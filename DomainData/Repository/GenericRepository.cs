using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MenuManager.DB;
using MenuManager.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DomainData.Repository
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        protected readonly DbSet<TModel> _dbSet;
        protected readonly MenuContext _context;

        public GenericRepository(MenuContext context)
        {
            _dbSet = context.Set<TModel>();
            _context = context;
        }

        public TModel GetById(int id, params Expression<Func<TModel, object>>[] includes)
        {
            IQueryable<TModel> query = _dbSet;

            foreach (var include in includes)
                query = query.Include(include);

            var keyPropertyName = GetKeyPropertyName(null);
            return query.FirstOrDefault(e => EF.Property<int>(e, keyPropertyName) == id);
        }
        public List<TModel> GetAll(params Expression<Func<TModel, object>>[] includes)
        {
            IQueryable<TModel> query = _dbSet;

            foreach (var include in includes)
                query = query.Include(include);

            return query.ToList();
        }


        public void Create(TModel model)
        {
            _dbSet.Add(model);
        }

        public void Update(TModel model)
        {
            var entry = _context.Entry(model);

            if (entry.State == EntityState.Detached)
            {
                var existing = _dbSet.Find(GetIdValue(model));
                if (existing != null)
                {
                    _context.Entry(existing).CurrentValues.SetValues(model);
                    return;
                }
            }

            _dbSet.Update(model);
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }
        public void Attach(TModel model)
        {
            _dbSet.Attach(model);
        }
        public TModel GetTrackedOrAttach(int id, params Expression<Func<TModel, object>>[] includes)
        {

            var keyPropertyName = GetKeyPropertyName(null); 
            var keyProperty = typeof(TModel).GetProperty(keyPropertyName);

            var trackedEntity = _context.ChangeTracker
                .Entries<TModel>()
                .FirstOrDefault(e =>
                    keyProperty != null &&
                    keyProperty.PropertyType == typeof(int) &&
                    (int)keyProperty.GetValue(e.Entity) == id);

            if (trackedEntity != null)
            {
                return trackedEntity.Entity;
            }

            var entity = GetById(id, includes);

            if (entity != null)
            {
                _dbSet.Attach(entity);
            }

            return entity;
        }

        private int GetIdValue(TModel model)
        {
            var propertyName = GetKeyPropertyName(model);
            var property = typeof(TModel).GetProperty(propertyName);
            if (property != null && property.PropertyType == typeof(int))
            {
                return (int)property.GetValue(model);
            }
            throw new InvalidOperationException();
        }
        private string GetKeyPropertyName(TModel model)
        {
            var eType = _context.Model.FindEntityType(typeof(TModel));
            var primaryKey = eType.FindPrimaryKey();
            var propertyName = primaryKey.Properties[0].Name;
            return propertyName;
        }

    }
}
