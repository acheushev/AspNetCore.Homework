using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Models;

namespace Northwind.DAL.Repositories
{
    public class SuppliersRepository : IRepository<Suppliers>
    {
        private readonly NorthwindContext northwindContext;

        public SuppliersRepository(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;
        }

        public IEnumerable<Suppliers> GetAll()
        {
            return northwindContext.Suppliers;
        }

        public Suppliers Get(int id)
        {
            return northwindContext.Suppliers.Find(id);
        }

        public void Create(Suppliers item)
        {
            northwindContext.Suppliers.Add(item);
        }

        public void Update(Suppliers item)
        {
            northwindContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var supplier = northwindContext.Suppliers.Find(id);
            if (supplier != null)
                northwindContext.Suppliers.Remove(supplier);
        }
    }
}