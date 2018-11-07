using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Homework.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.DAL.Interfaces;

namespace AspNetCore.Homework.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CategoriesApiController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        

        [HttpGet]
        public IEnumerable<CategoryViewModel> Get()
        {
            return mapper.Map<IEnumerable<CategoryViewModel>>(uow.CategoriesRepository.GetAll());
        }

      
        [HttpGet("{id}/image", Name = "Get")]
        public IActionResult Get(int id)
        {
            var category = uow.CategoriesRepository.Get(id);
            if (category != null)
            {
                var image = category.Picture.ToArray();

                return File(image, "image/bmp");
            }

            return NotFound();
        }


        [HttpPut("{id}/image")]
        public IActionResult Post(int id, byte[] image)
        {
            var category = uow.CategoriesRepository.Get(id);
            if (category != null)
            {

                category.Picture = image;
                uow.CategoriesRepository.Update(category);

                uow.Commit();

                return Ok();
            }


            return NotFound();
        }



    }
}
