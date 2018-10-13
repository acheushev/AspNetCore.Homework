using System;
using System.Collections.Generic;
using System.Text;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Models;

namespace AspNetCore.Homework.UnitTests
{
    class CategoriesRepositoryStub: IRepository<Categories>
    {
        public static List<Categories> Categories = new List<Categories>
        {
            new Categories() {CategoryId = 1, CategoryName = "First Category"},
            new Categories() {CategoryId = 2, CategoryName = "Second Category"},
            new Categories() {CategoryId = 3, CategoryName = "Third Category"}
        };

        public IEnumerable<Categories> GetAll()
        {
            return Categories;
        }

        public Categories Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Categories item)
        {
            throw new NotImplementedException();
        }

        public void Update(Categories item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
