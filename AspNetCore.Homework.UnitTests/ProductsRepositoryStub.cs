using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Models;

namespace AspNetCore.Homework.UnitTests
{
    class ProductsRepositoryStub: IRepository<Products>
    {
        public List<Products> Products=new List<Products>
        {
            new Products(){ProductId = 1, ProductName = "First Product"},
            new Products(){ProductId = 2, ProductName = "Second Product"},
            new Products(){ProductId = 3, ProductName = "Third Product"}
        };

        public IEnumerable<Products> GetAll()
        {
            return Products;
        }

        public Products Get(int id)
        {
            return Products.FirstOrDefault(p => p.ProductId == id);
        }

        public void Create(Products item)
        {
            Products.Add(item);
        }

        public void Update(Products item)
        {
            if(Products.All(p => p.ProductId != item.ProductId))
                throw new Exception("Product to update not found");
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
