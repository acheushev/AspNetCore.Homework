using System.Diagnostics;
using System.IO;
using System.Linq;
using AspNetCore.Homework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.DAL.Interfaces;

namespace AspNetCore.Homework.Controllers
{
    [SampleActionFilter(true)]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork uow;

        public CategoriesController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public IActionResult Index()
        {
            return View(uow.CategoriesRepository.GetAll());
        }

        public IActionResult GetCategoryImageById(int id)
        {
            var category = uow.CategoriesRepository.Get(id);
            if (category != null)
            {
                var image=category.Picture.ToArray();
              
                return File(image, "image/bmp");
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Upload(int categoryId, IFormFile file)
        {
            var category = uow.CategoriesRepository.Get(categoryId);
            if (category != null)
            {
                if (file!=null&&file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                         file.CopyTo(stream);

                        byte[] image = stream.ToArray();
                        category.Picture = image;
                        uow.CategoriesRepository.Update(category);

                        uow.Commit();
                    }
                }
            }

            return RedirectToAction("Index");
        }
    }
}