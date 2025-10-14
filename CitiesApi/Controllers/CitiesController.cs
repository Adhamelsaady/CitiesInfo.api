using Microsoft.AspNetCore.Mvc;
namespace CitiesApi.Controllers
{
    [ApiController]
    public class CitiesController : ControllerBase
    {
        [HttpGet("api/Cities")]
        public JsonResult GetCities()
        {
            return new JsonResult(new List<object> {

                new {id = 1 , Name = "Cairo"} , 
                new {id = 2 , Name = "Alexandria "}
            });
        }
    }
}
