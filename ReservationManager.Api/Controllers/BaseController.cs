using Microsoft.AspNetCore.Mvc;
using ReservationManager.Service.IServices;

namespace ReservationManager.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        public IBaseService BaseService { get; }

        public BaseController(IBaseService baseService)
        {
            BaseService = baseService;
        }
    }
}
