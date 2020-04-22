using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkshopController : ControllerBase
    {

        private readonly WorkshopRepository _WorkshopRepository;

        public WorkshopController() {
            _WorkshopRepository = new WorkshopRepository();
        }

        // GET: api/Workshop
        [HttpGet]
        public IEnumerable<WorkshopModel> Get()
        {
            return _WorkshopRepository.Get();
        }

        // GET: api/Workshop/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            object result = _WorkshopRepository.GetById(id);
            if (result != null)
                return result;
            return NotFound();
        }

        // POST: api/Workshop
        [HttpPost]
        public void Post([FromBody] WorkshopModel model)
        {
            _WorkshopRepository.Create(model);
        }

        // PUT: api/Workshop/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] WorkshopModel model)
        {
            model.Id = id;
            _WorkshopRepository.Update(model);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _WorkshopRepository.Delete(id);
        }
    }
}
