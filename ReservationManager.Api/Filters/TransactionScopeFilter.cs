using Microsoft.AspNetCore.Mvc.Filters;
using ReservationManager.Api.Controllers;

namespace ReservationManager.Api.Filters
{
    public class TransactionScopeFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller is BaseController baseController)
            {
                if (context.Exception == null)
                    baseController.BaseService.SaveChanges();
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
