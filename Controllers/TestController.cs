using Microsoft.AspNetCore.Mvc;

namespace Citd 
{
    public class TestController : Controller
    {
        [HttpGet("/api/demo/")]
        public dynamic TestMethod() 
        {
            return new { Name = "Marjatan puhelin" };
        }
    }
}