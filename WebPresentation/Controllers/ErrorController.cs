///<summary>
/// Creator: Dat Tran
/// Created: 2025-04-28
/// Summary: To handle pages that don't exist. 
/// 
/// Last Updated By:
/// Last Updated:
/// What was changed:
/// </summary>



using Microsoft.AspNetCore.Mvc;

namespace GameCollectionWeb.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("errors/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            if (statusCode == 404)
                return View("404");

            return View("GenericError"); // optional fallback
        }
    }
}
