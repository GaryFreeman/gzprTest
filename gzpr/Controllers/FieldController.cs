using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gzpr.Models;
using gzpr.Models.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gzpr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldController : ControllerBase
    {

        private readonly FieldRepository _FieldRepository;

        public FieldController() {
            _FieldRepository = new FieldRepository();
        }


        // GET: api/Field
        [HttpGet]
        public IEnumerable<FieldModel> Get()
        {
            return _FieldRepository.Get();
        }

        // GET: api/Field/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            object result = _FieldRepository.GetById(id);
            if (result != null)
                return result;
            return NotFound();
        }

        // POST: api/Field
        [HttpPost]
        public void Post([FromBody] FieldModel model)
        {
            _FieldRepository.Create(model);
        }

        // PUT: api/Field/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] FieldModel model)
        {
            model.Id = id;
            _FieldRepository.Update(model);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _FieldRepository.Delete(id);
        }
    }
}
