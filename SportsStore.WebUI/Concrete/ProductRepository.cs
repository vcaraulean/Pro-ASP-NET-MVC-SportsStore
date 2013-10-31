using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }
    }
}