using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Models;

namespace AspNetCore.Homework.UnitTests
{
    class CategoriesRepositoryStub: IRepository<Categories>
    {
        public  List<Categories> Categories = new List<Categories>
        {
            new Categories() {CategoryId = 1, CategoryName = "First Category", Picture=new byte[3]{1,2,3}},
            new Categories() {CategoryId = 2, CategoryName = "Second Category", Picture=new byte[3]{1,2,3}},
            new Categories() {CategoryId = 3, CategoryName = "Third Category", Picture=new byte[3]{1,2,3}}
        };

        public IEnumerable<Categories> GetAll()
        {
            return Categories;
        }

        public Categories Get(int id)
        {
            return Categories.FirstOrDefault(p => p.CategoryId == id);
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
