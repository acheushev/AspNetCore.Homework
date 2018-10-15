using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspNetCore.Homework.Controllers;
using AspNetCore.Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Northwind.DAL.Models;
using Xunit;

namespace AspNetCore.Homework.UnitTests
{
    public class ProductsControllerTests
    {

        [Fact]
        public void ProductsController_IndexAction_ShouldReturnAllProductsIfMIsNull()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(config => config.GetSection("M")).Returns((IConfigurationSection) null);
            var controller = new ProductsController(new UnitOfWorkStub(), configMock.Object);
            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Products>>(
                viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());

        }

        [Fact]
        public void ProductsController_IndexAction_ShouldReturnMProducts()
        {
            int M = 2;
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(config => config.GetSection("M").Value).Returns(M.ToString);
            var controller = new ProductsController(new UnitOfWorkStub(), configMock.Object);
            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Products>>(
                viewResult.ViewData.Model);
            Assert.Equal(M, model.Count());

        }

        [Fact]
        public void ProductsController_EditActionGet_ShouldReturnProductForEdit()
        { 
            var configMock = new Mock<IConfiguration>();          
            var controller = new ProductsController(new UnitOfWorkStub(), configMock.Object);
            var result = controller.Edit(2);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.ProductId);

        }

        [Fact]
        public void ProductsController_EditActionGet_ShouldRedirectToCreateActionIfProductIdIsInvalid()
        {
            var configMock = new Mock<IConfiguration>();
            var controller = new ProductsController(new UnitOfWorkStub(), configMock.Object);
            var result = controller.Edit(20);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            
            Assert.Equal("Create",redirectResult.ActionName);

        }

        [Fact]
        public void ProductsController_EditActionPost_ShouldRedirectToIndexActionIfViewModelIsValid()
        {
            var configMock = new Mock<IConfiguration>();
            var controller = new ProductsController(new UnitOfWorkStub(), configMock.Object);
            var result = controller.Edit(new ProductViewModel
            {
                SupplierId = 3,
                ProductName = "New Product",
                CategoryId = 3,
                ProductId = 2,
                Discontinued = true,
                QuantityPerUnit = "Yes",
                ReorderLevel = 3,
                UnitPrice = 1000,
                UnitsInStock = 10,
                UnitsOnOrder = 2
            });

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectResult.ActionName);

        }

        [Fact]
        public void ProductsController_EditActionPost_ShouldReturnTheSameViewModelIfItIsNotValid()
        {
            var configMock = new Mock<IConfiguration>();
            var controller = new ProductsController(new UnitOfWorkStub(), configMock.Object);

            controller.ModelState.AddModelError("error", "some error");

            var result = controller.Edit(new ProductViewModel
            {
                SupplierId = 3,
                ProductName = "New",
                CategoryId = 3,
                ProductId = 2,
                Discontinued = true,
                QuantityPerUnit = "Yes",
                ReorderLevel = 3,
                UnitPrice = 1000,
                UnitsInStock = 10,
                UnitsOnOrder = 2
            });
            

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.ProductId);

        }

        [Fact]
        public void ProductsController_CreateActionPost_ShouldRedirectToIndexActionIfViewModelIsValid()
        {
            var configMock = new Mock<IConfiguration>();
            var controller = new ProductsController(new UnitOfWorkStub(), configMock.Object);
            var result = controller.Create(new ProductViewModel
            {
                SupplierId = 3,
                ProductName = "New Product",
                CategoryId = 3,
                ProductId = 4,
                Discontinued = true,
                QuantityPerUnit = "Yes",
                ReorderLevel = 3,
                UnitPrice = 1000,
                UnitsInStock = 10,
                UnitsOnOrder = 2
            });

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectResult.ActionName);

        }

        [Fact]
        public void ProductsController_CreateActionPost_ShouldReturnTheSameViewModelIfItIsNotValid()
        {
            var configMock = new Mock<IConfiguration>();
            var controller = new ProductsController(new UnitOfWorkStub(), configMock.Object);

            controller.ModelState.AddModelError("error", "some error");

            var result = controller.Create(new ProductViewModel
            {
                SupplierId = 3,
                ProductName = "New",
                CategoryId = 3,
                ProductId = 4,
                Discontinued = true,
                QuantityPerUnit = "Yes",
                ReorderLevel = 3,
                UnitPrice = 1000,
                UnitsInStock = 10,
                UnitsOnOrder = 2
            });


            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal(4, model.ProductId);

        }

        [Fact]
        public void ProductsController_CreateActionGet_ShouldReturnEmptyViewModel()
        {
            var configMock = new Mock<IConfiguration>();
            var controller = new ProductsController(new UnitOfWorkStub(), configMock.Object);          

            var result = controller.Create();


            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal(null, model.ProductId);

        }
    }
}
