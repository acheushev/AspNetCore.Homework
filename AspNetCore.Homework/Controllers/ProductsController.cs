using System.Diagnostics;
using System.Linq;
using AspNetCore.Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Models;

namespace AspNetCore.Homework.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IConfiguration config;
        private readonly IUnitOfWork uow;

        public ProductsController(IUnitOfWork uow, IConfiguration config)
        {
            this.uow = uow;
            this.config = config;
        }

        public IActionResult Index()
        {
            return View(int.TryParse(config.GetSection("M")?.Value, out int max)
                ? uow.ProductsRepository.GetAll().Take(max)
                : uow.ProductsRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = uow.ProductsRepository.Get(id);
            if (product != null)
                return View(new ProductViewModel
                {
                    ProductId = product.ProductId,
                    CategoryId = product.CategoryId,
                    ProductName = product.ProductName,
                    QuantityPerUnit = product.QuantityPerUnit,
                    UnitsOnOrder = product.UnitsOnOrder,
                    Discontinued = product.Discontinued,
                    ReorderLevel = product.ReorderLevel,
                    UnitPrice = product.UnitPrice,
                    SupplierId = product.SupplierId,
                    UnitsInStock = product.UnitsInStock,
                    /* Supplier = product.Supplier,
                     Category = product.Category,
                     OrderDetails = product.OrderDetails,*/
                    AllCategories = uow.CategoriesRepository.GetAll().Select(cat => new CategoryViewModel
                        {Id = cat.CategoryId, Name = cat.CategoryName}).ToList(),
                    AllSuppliers = uow.SuppliersRepository.GetAll().Select(sup => new SupplierViewModel
                        {SupplierId = sup.SupplierId, SupplierName = sup.CompanyName}).ToList()
                });

            return View("Create");
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = new Products
                {
                    ProductName = viewModel.ProductName,
                    ProductId = viewModel.ProductId.Value,
                    CategoryId = viewModel.CategoryId,
                    Discontinued = viewModel.Discontinued,
                    QuantityPerUnit = viewModel.QuantityPerUnit,
                    SupplierId = viewModel.SupplierId,
                    ReorderLevel = viewModel.ReorderLevel,
                    UnitPrice = viewModel.UnitPrice,
                    UnitsInStock = viewModel.UnitsInStock,
                    UnitsOnOrder = viewModel.UnitsOnOrder
                    /* Supplier = viewModel.Supplier,
                     Category = viewModel.Category,
                     OrderDetails = viewModel.OrderDetails*/
                };

                uow.ProductsRepository.Update(product);
                uow.Commit();

                return RedirectToAction("Index");
            }

            viewModel.AllCategories = uow.CategoriesRepository.GetAll()
                .Select(cat => new CategoryViewModel {Id = cat.CategoryId, Name = cat.CategoryName}).ToList();
            viewModel.AllSuppliers = uow.SuppliersRepository.GetAll().Select(sup => new SupplierViewModel
                {SupplierId = sup.SupplierId, SupplierName = sup.CompanyName}).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ProductViewModel
            {
                AllCategories =
                    uow.CategoriesRepository.GetAll().Select(cat =>
                        new CategoryViewModel {Id = cat.CategoryId, Name = cat.CategoryName}).ToList(),
                AllSuppliers = uow.SuppliersRepository.GetAll().Select(sup => new SupplierViewModel
                {
                    SupplierId = sup.SupplierId, SupplierName = sup.CompanyName
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = new Products
                {
                    ProductName = viewModel.ProductName,
                    CategoryId = viewModel.CategoryId,
                    Discontinued = viewModel.Discontinued,
                    QuantityPerUnit = viewModel.QuantityPerUnit,
                    SupplierId = viewModel.SupplierId,
                    ReorderLevel = viewModel.ReorderLevel,
                    UnitPrice = viewModel.UnitPrice,
                    UnitsInStock = viewModel.UnitsInStock,
                    UnitsOnOrder = viewModel.UnitsOnOrder
                    /* Supplier = viewModel.Supplier,
                     Category = viewModel.Category,
                     OrderDetails = viewModel.OrderDetails*/
                };

                uow.ProductsRepository.Update(product);
                uow.Commit();

                return RedirectToAction("Index");
            }

            viewModel.AllCategories = uow.CategoriesRepository.GetAll()
                .Select(cat => new CategoryViewModel {Id = cat.CategoryId, Name = cat.CategoryName}).ToList();
            viewModel.AllSuppliers = uow.SuppliersRepository.GetAll().Select(sup => new SupplierViewModel
                {SupplierId = sup.SupplierId, SupplierName = sup.CompanyName}).ToList();

            return View(viewModel);
        }

    }


}