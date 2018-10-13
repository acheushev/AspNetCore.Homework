using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCore.Homework.Controllers;
using AspNetCore.Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Northwind.DAL.Models;
using Xunit;
using Assert = Xunit.Assert;


namespace AspNetCore.Homework.UnitTests
{
    public class CategoriesControllerTests
    {
        [Fact]
        public void CategoriesController_IndexAction_ShouldReturnAllCategories()
        {
            var controller = new CategoriesController(new UnitOfWorkStub());
            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Categories>>(
                viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());

        }
        
    }
}
