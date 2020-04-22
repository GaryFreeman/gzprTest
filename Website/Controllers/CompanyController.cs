using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRequestHandler _companyRequestHandler;
        
        public CompanyController(ICompanyRequestHandler companyRequestHandler)
        {
            _companyRequestHandler = companyRequestHandler;
        }

        // GET: api/Company
        [HttpGet]
        public Task<IEnumerable<CompanyModel>> Get()
        {
            return _companyRequestHandler.Get();
        }

        // GET: api/Company/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _companyRequestHandler.Get(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        // POST: api/Company
        [HttpPost]
        public Task Post([FromBody] CompanyModel model)
        {
            return _companyRequestHandler.Create(model);
        }

        // PUT: api/Company/5
        [HttpPut("{id}")]
        public Task Put(int id, [FromBody] CompanyModel model)
        {
            model.Id = id;
            return _companyRequestHandler.Update(model);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Task Delete(int id)
        {
            return _companyRequestHandler.Delete(id);
        }
    }
}
