using Microsoft.AspNetCore.Mvc;
using Northwind.DAL.Interfaces;

namespace AspNetCore.Homework.Controllers
{
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
    }
}