using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellController : ControllerBase
    {
        private readonly IWellRequestHandler _wellRequestHandler;

        public WellController(IWellRequestHandler wellRequestHandler)
        {
            _wellRequestHandler = wellRequestHandler;
        }

        // GET: api/Well
        [HttpGet]
        public Task<IEnumerable<WellModel>> Get()
        {
            return _wellRequestHandler.Get();
        }

        // GET: api/Well/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _wellRequestHandler.Get(id);
            if (result != null)
            {
                return Ok(result);
            }
            
            return NotFound();
        }

        // POST: api/Well
        [HttpPost]
        public Task Post(WellModel well)
        {
            return _wellRequestHandler.Create(well);
        }

        // PUT: api/Well/5
        [HttpPut("{id}")]
        public Task Put(int id, WellModel model)
        {
            model.Id = id;
            return _wellRequestHandler.Update(model);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Task Delete(int id)
        {
            return _wellRequestHandler.Delete(id);
        }
    }
}
