using AspNetApiPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetApiPractice.Data.Repository
{
    public class BaseModelRepository<T> : Repository<T> where T : BaseModel
    {
        public BaseModelRepository(Entities dbContext) : base(dbContext)
        {
        }

        public override T Add(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }
        public override void Delete(T entity)
        {
            entity.IsDeleted = true;
        }
    }
}
