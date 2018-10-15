using System;
using System.Collections.Generic;
using System.Text;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Models;

namespace AspNetCore.Homework.UnitTests
{
    public class SuppliersRepositoryStub:IRepository<Suppliers>
    {
        public List<Suppliers> Suppliers=new List<Suppliers>
        {
            new Suppliers(){SupplierId = 1,CompanyName = "First Supplier"},
            new Suppliers(){SupplierId = 2,CompanyName = "Second Supplier"},
            new Suppliers(){SupplierId = 3,CompanyName = "Third Supplier"}
        };
        public IEnumerable<Suppliers> GetAll()
        {
            return Suppliers;
        }

        public Suppliers Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Suppliers item)
        {
            throw new NotImplementedException();
        }

        public void Update(Suppliers item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
