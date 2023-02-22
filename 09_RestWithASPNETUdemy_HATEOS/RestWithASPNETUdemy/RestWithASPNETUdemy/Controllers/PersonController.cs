using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Filters;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {


        private readonly ILogger<PersonController> _logger;
        private IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;

        }

        [HttpGet]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult Get()
        {

            return Ok(_personBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HypermediaFilter))]

        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }
        [HttpPost]
        [TypeFilter(typeof(HypermediaFilter))]

        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            return Ok(_personBusiness.Create(person));
        }
        [HttpPut]
        [TypeFilter(typeof(HypermediaFilter))]

        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            return Ok(_personBusiness.Update(person));
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }

    }
}