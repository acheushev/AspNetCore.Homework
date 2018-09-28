using System;
using Northwind.DAL.Models;

namespace Northwind.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Products> ProductsRepository { get; }
        IRepository<Categories> CategoriesRepository { get; }
        IRepository<Suppliers> SuppliersRepository { get; }
        void Commit();
    }
}