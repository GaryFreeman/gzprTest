using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gzpr.Models;
using gzpr.Models.Repos;
using Newtonsoft.Json.Linq;
using gzpr.Helpers;

namespace gzpr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellController : ControllerBase
    {
        private readonly WellRepository _WellRepository;

        public WellController() {
            _WellRepository = new WellRepository();
        }

        // GET: api/Well
        [HttpGet]
        public IEnumerable<WellModel> Get()
        {
            return _WellRepository.Get();
        }

        // GET: api/Well/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return _WellRepository.GetById(id);
        }

        // POST: api/Well
        [HttpPost]
        public void Post(WellRequest well)
        {
            _WellRepository.Create(well);
        }

        // PUT: api/Well/5
        [HttpPut("{id}")]
        public void Put(int id, WellRequest model)
        {
            model.id = id;
            _WellRepository.Update(model);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _WellRepository.Delete(id);
        }
    }
}
