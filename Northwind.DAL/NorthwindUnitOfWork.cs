using System;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Models;
using Northwind.DAL.Repositories;

namespace Northwind.DAL
{
    public class NorthwindUnitOfWork : IUnitOfWork
    {
        private readonly NorthwindContext context;
        private IRepository<Categories> categoriesRepository;
        private bool disposed;
        private IRepository<Products> productsRepository;
        private SuppliersRepository suppliersRepository;

        public NorthwindUnitOfWork(NorthwindContext context)
        {
            this.context = context;
        }

        public IRepository<Products> ProductsRepository =>
            productsRepository ?? (productsRepository = new ProductRepository(context));

        public IRepository<Categories> CategoriesRepository =>
            categoriesRepository ?? (categoriesRepository = new CategoryRepository(context));

        public IRepository<Suppliers> SuppliersRepository =>
            suppliersRepository ?? (suppliersRepository = new SuppliersRepository(context));

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                context.Dispose();
                GC.SuppressFinalize(this);

                disposed = true;
            }
        }
    }
}