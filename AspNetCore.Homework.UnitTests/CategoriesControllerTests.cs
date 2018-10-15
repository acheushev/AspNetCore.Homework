using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AspNetCore.Homework.Controllers;
using AspNetCore.Homework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Northwind.DAL.Interfaces;
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

        [Fact]
        public void CategoriesController_GetCategoryImageByIdAction_ShouldReturnNotFoundIfCategoryDoesNotExist()
        {
            var controller = new CategoriesController(new UnitOfWorkStub());
            var result = controller.GetCategoryImageById(20);

            Assert.IsType<NotFoundResult>(result);
          
        }

        [Fact]
        public void CategoriesController_GetCategoryImageByIdAction_ShouldReturnFileResultIfCategoryExists()
        {
            var controller = new CategoriesController(new UnitOfWorkStub());
            var result = controller.GetCategoryImageById(2);

            Assert.IsType<FileContentResult>(result);

        }

        [Fact]
        public void CategoriesController_UploadAction_ShouldCommitNewPictureIfCategoryExistsAndFormFileIsNotNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Loose);

            unitOfWorkMock.Setup(uow => uow.CategoriesRepository.Get(It.IsAny<int>())).Returns(new Categories());

            var formFile=new Mock<IFormFile>();

            formFile.Setup(f => f.Length).Returns(1);
            formFile.Setup(f => f.CopyTo(It.IsAny<Stream>())).Callback<Stream>(stream => stream.WriteByte(1));


            var controller = new CategoriesController(unitOfWorkMock.Object);
             controller.Upload(1,formFile.Object);


            unitOfWorkMock.Verify(uow => uow.CategoriesRepository.Update(It.IsAny<Categories>()));
            unitOfWorkMock.Verify(uow => uow.Commit());


        }

        [Fact]
        public void CategoriesController_UploadAction_ShouldNotCommitNewPictureIfCategoryDoesNotExist()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Loose);

            unitOfWorkMock.Setup(uow => uow.CategoriesRepository.Get(It.IsAny<int>())).Returns((Categories)null);

            var formFile = new Mock<IFormFile>();

            formFile.Setup(f => f.Length).Returns(1);
            formFile.Setup(f => f.CopyTo(It.IsAny<Stream>())).Callback<Stream>(stream => stream.WriteByte(1));


            var controller = new CategoriesController(unitOfWorkMock.Object);
            controller.Upload(1, formFile.Object);


            unitOfWorkMock.Verify(uow => uow.CategoriesRepository.Update(It.IsAny<Categories>()),Times.Never);
            unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);


        }

        [Fact]
        public void CategoriesController_UploadAction_ShouldNotCommitNewPictureIfFileIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Loose);

            unitOfWorkMock.Setup(uow => uow.CategoriesRepository.Get(It.IsAny<int>())).Returns((Categories)null);
           
            var controller = new CategoriesController(unitOfWorkMock.Object);
            controller.Upload(1, null);

            unitOfWorkMock.Verify(uow => uow.CategoriesRepository.Update(It.IsAny<Categories>()), Times.Never);
            unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);


        }

    }
}
