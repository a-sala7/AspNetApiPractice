using AspNetApiPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetApiPractice.Data.Repository
{
    public class BaseModelRepository : Repository<BaseModel>
    {
        public BaseModelRepository(Entities dbContext) : base(dbContext)
        {
        }

        public override BaseModel Add(BaseModel entity)
        {
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }
        public override void Delete(BaseModel entity)
        {
            entity.IsDeleted = true;
        }
    }
}
