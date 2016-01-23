namespace AUSKF.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Domain.Interfaces;
    using Entities.Identity;

    public class UserRepository : EntityRepository<User, int>
    {
        public UserRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        //public ICollection<User> GetWithPromotion(int page)
        //{

        //    //var results = from p in persons
        //    //  group p.car by p.PersonId into g
        //    //  select new { PersonID = g.Key, Cars = g.ToList() };

        //    var context = (DataContext) this.Context;
                        

        //}
    }
}