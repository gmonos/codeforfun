using Dapper.Contrib.Extensions;
using Zags.Domain.Entity;
using Zags.Domain.Repositories;
using Zags.Utility.Functional;

namespace Zags.DataAccess.Dapper
{
    public class GenericDapperRepository<T>: DapperBase, IRepository<T>
          where T : class, IEntity, new()
    {
      
        public GenericDapperRepository(string connectionString):base(connectionString)
        { 
        }

        public GenericDapperRepository(): base(string.Empty) { }

        public virtual T Add(T newEntity) 
        {
            Connect(ConnectionString, conn => conn.Insert(newEntity));

            return newEntity;
        }

        public virtual void Update(T entity) 
        {
            Connect(ConnectionString, conn => conn.Update(entity));
        }

        public virtual void Delete(T entity)
        {

            Connect(ConnectionString, conn => conn.Delete(entity));
        }

        public virtual void Delete(int pId) 
        {
            var e = new T { Id = pId };

            Connect(ConnectionString, conn => conn.Delete(e));
        }


        public Option<T> GetById(int id)
        {
            return Connect(ConnectionString, conn => conn.Get<T>(id));
        }

        

        
    }
}
