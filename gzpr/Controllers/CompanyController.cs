using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gzpr.Models;
using gzpr.Models.Repos;

namespace gzpr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyRepository _CompanyRepository;

        //public CompanyController()
        public CompanyController() {
            _CompanyRepository = new CompanyRepository(); 
        }

        // GET: api/Company
        [HttpGet]
        public IEnumerable<CompanyModel> Get()
        {
            return _CompanyRepository.Get();
        }

        // GET: api/Company/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            object result = _CompanyRepository.GetById(id);
            if (result != null)
                return result;
            return NotFound();
        }

        // POST: api/Company
        [HttpPost]
        public void Post([FromBody] CompanyModel model)
        {
            _CompanyRepository.Create(model);

        }

        // PUT: api/Company/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CompanyModel model)
        {
            model.Id = id;
            _CompanyRepository.Update(model);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _CompanyRepository.Delete(id);
        }
    }
}
