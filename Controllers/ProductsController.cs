using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi1.Services;

namespace WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IHangHoaResposity _hangHoaResposity;

        public ProductsController(IHangHoaResposity hangHoaResposity)
        {
            _hangHoaResposity = hangHoaResposity;

        }

        [HttpGet]
        public IActionResult GetAllProducts(string? search , double? from , double? to , string? sortBy, int page = 1)
        {
            try
            {
                var result = _hangHoaResposity.GetAll(search, from , to, sortBy,page);
                return Ok(result);
            }
            catch
            {
                return BadRequest("We can't get the products");

            }
        }
    }
}
