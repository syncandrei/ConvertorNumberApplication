using Convertor.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ConvertorAppRestApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ConvertorController : ControllerBase
    {
        private readonly IConvertorService convertorService;

        public ConvertorController(IConvertorService convertorService)
        {
            this.convertorService = convertorService;
        }

        // GET: api/convertor?number={number}
        [HttpGet("convertor", Name = "ConvertorNumberToWords")]
        public IActionResult Convert([FromQuery] string number)
        {
            try
            {
                return Ok(convertorService.GetNumberToWords(number));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
