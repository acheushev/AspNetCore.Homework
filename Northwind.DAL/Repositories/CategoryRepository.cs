using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Models;

namespace Northwind.DAL.Repositories
{
    public class CategoryRepository : IRepository<Categories>
    {
        private readonly NorthwindContext northwindContext;

        public CategoryRepository(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;
        }

        public IEnumerable<Categories> GetAll()
        {
            return northwindContext.Categories;
        }

        public Categories Get(int id)
        {
            return northwindContext.Categories.Find(id);
        }

        public void Create(Categories item)
        {
            northwindContext.Categories.Add(item);
        }

        public void Update(Categories item)
        {
            northwindContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var cat = northwindContext.Categories.Find(id);
            if (cat != null)
                northwindContext.Categories.Remove(cat);
        }
    }
}