using Financial.Domain.Interfaces;
using Financial.Models.Req;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : BaseController
    {

        //var teste = Configuration["ConnectionStrings:DatabaseConn"];
        //Console.WriteLine(Configuration["ConnectionStrings:DatabaseConn"]);
         
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetCategories() => BuildResponse(categoryService.Get(Uid));

        [Route("category")]
        [HttpPost]
        public IActionResult CreateCategory(CategoryReq reqCategory) => BuildResponse(categoryService.Create(reqCategory, Uid));
    }
}
