using Financial.Domain;
using FinancialAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Financial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfraController(IInitializeDbService initializeDbService) : BaseController
    {
        [Route("infra/initializeDb")]
        [HttpGet]
        public IActionResult GetCategories() => BuildResponse(initializeDbService.Execute());

    }
}
