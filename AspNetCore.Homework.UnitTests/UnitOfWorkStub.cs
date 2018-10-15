using System;
using System.Collections.Generic;
using System.Text;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Models;

namespace AspNetCore.Homework.UnitTests
{
    class UnitOfWorkStub: IUnitOfWork
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IRepository<Products> ProductsRepository { get; } = new ProductsRepositoryStub();

        public IRepository<Categories> CategoriesRepository { get; } = new CategoriesRepositoryStub();

        public IRepository<Suppliers> SuppliersRepository { get; } = new SuppliersRepositoryStub();
        public void Commit()
        {
         
        }
    }
}
