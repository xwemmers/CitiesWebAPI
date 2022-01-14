using CitiesWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitiesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        static List<City> cities = new ()
        {
            new City { CityID = 1, CityName = "Parijs", Country = "Frankrijk"},
            new City { CityID = 2, CityName = "Utrecht", Country = "Nederland"},
            new City { CityID = 3, CityName = "Amsterdam", Country = "Nederland"}
        };

        [HttpGet]
        public ActionResult<IEnumerable<City>> Get()
        {
            return Ok(cities);
        }


        [HttpGet("{id:int}")]
        public ActionResult<City> Get(int id)
        {
            var c = cities.FirstOrDefault(c => c.CityID == id);

            if (c == null)
                return NotFound();
            else
                return Ok(c);
        }


        [HttpGet("search/{searchtext}")]
        public ActionResult<IEnumerable<City>> Get(string searchtext)
        {
            var result = cities.Where(c => c.CityName!.Contains(searchtext)).ToList();

            return Ok(result);
        }

        // GET PUT POST DELETE

        [HttpGet("searchByCountry/{searchtext}")]
        public ActionResult<IEnumerable<City>> GetByCountry(string searchtext)
        {
            var result = cities.Where(c => c.Country!.Contains(searchtext)).ToList();

            return Ok(result);
        }

        // HTTP Verbs: GET PUT POST DELETE

        [HttpPost]
        public IActionResult Post(City c)
        {
            // Normaal gesproken bepaalt de DB de nieuwe id maar nu moeten we dat zelf doen...
            int id = cities.Max(cc => cc.CityID) + 1;

            c.CityID = id;
            cities.Add(c);

            return Ok(c);
        }


        //[HttpPost]
        //public IActionResult Post(string naam)
        //{
        //    var c = new City { CityName = naam, Country = "NL" };

        //    // Normaal gesproken bepaalt de DB de nieuwe id maar nu moeten we dat zelf doen...
        //    int id = cities.Max(cc => cc.CityID) + 1;

        //    c.CityID = id;
        //    cities.Add(c);

        //    return Ok(c);
        //}

        [HttpPut]
        public IActionResult Put(int id, City c)
        {
            // Validatie kan ook middels ModelState.IsValid
            if (c.CityID != id)
                return BadRequest("IDs komen niet overeen");

            var oldCity = cities.FirstOrDefault(cc => cc.CityID == id);

            if (oldCity == null)
                return NotFound();

            oldCity.CityName = c.CityName;
            oldCity.Country = c.Country;

            //return Ok(c);
            return Accepted();
        }

        /// <summary>
        /// Deze methode verwijdert een stad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var c = cities.FirstOrDefault(c => c.CityID == id);

            if (c == null)
                return NotFound();

            cities.Remove(c);

            return NoContent();
        }
    }
}
