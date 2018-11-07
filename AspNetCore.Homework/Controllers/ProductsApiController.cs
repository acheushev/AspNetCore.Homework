using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Homework.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCore.Homework.Controllers
{
    [Route("api/products")]
    public class ProductsApiController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork uow;

        public ProductsApiController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<ProductViewModel> Get()
        {
            return mapper.Map<IEnumerable<ProductViewModel>>(uow.ProductsRepository.GetAll());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ProductViewModel Get(int id)
        {
            return mapper.Map<ProductViewModel>(uow.ProductsRepository.Get(id));
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]ProductViewModel value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            uow.ProductsRepository.Create(mapper.Map<Products>(value));

            uow.Commit();

            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ProductViewModel value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var oldProduct = uow.ProductsRepository.Get(id);

            if (oldProduct is null)
                return NotFound();

            value.ProductId = id;

            mapper.Map(value, oldProduct);

            uow.ProductsRepository.Update(oldProduct);

            uow.Commit();

            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            uow.ProductsRepository.Delete(id);
            
            uow.Commit();

            return Ok();
        }
    }
}
