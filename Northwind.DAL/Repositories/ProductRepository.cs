using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Models;

namespace Northwind.DAL.Repositories
{
    public class ProductRepository : IRepository<Products>
    {
        private readonly NorthwindContext northwindContext;

        public ProductRepository(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;
        }

        public IEnumerable<Products> GetAll()
        {
            return northwindContext.Products.Include(prod => prod.Category).Include(prod => prod.Supplier);
        }

        public Products Get(int id)
        {
            return northwindContext.Products.Find(id);
        }

        public void Create(Products item)
        {
            northwindContext.Products.Add(item);
        }

        public void Update(Products item)
        {
            northwindContext.Products.Update(item);
        }

        public void Delete(int id)
        {
            var product = northwindContext.Products.Find(id);
            if (product != null)
                northwindContext.Products.Remove(product);
        }
    }
}