using System.Collections.Generic;

namespace Application
{
    public interface IApplicationContext
    {
        //DbSet<Product> Products { get; set; }
        Task<int> SaveChanges();
    }
}
