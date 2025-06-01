using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainData.Repository
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        public TModel GetById(int id, params Expression<Func<TModel, object>>[] includes);
        public List<TModel> GetAll(params Expression<Func<TModel, object>>[] includes);
        public void Attach(TModel model);

        public void Create(TModel model);

        public void Update(TModel model);

        public void Delete(int id);
        public TModel GetTrackedOrAttach(int id, params Expression<Func<TModel, object>>[] includes);
    }
}
