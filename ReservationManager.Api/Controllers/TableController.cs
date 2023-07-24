using Microsoft.AspNetCore.Mvc;
using ReservationManager.Domain.Models.RequestDtos.Table;
using ReservationManager.Service.IServices;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace ReservationManager.Api.Controllers
{
    [Route("Table")]
    public class TableController : BaseController
    {
        private readonly ITableService _tableService;

        public TableController(IBaseService baseService,ITableService tableService) : base(baseService)
        {
            _tableService = tableService;
        }

        [HttpPost]
        [Route("SaveTable")]
        public IActionResult SaveTable([FromBody] SaveTableRequestDto requestSaveTable)
        {
            var responseService = _tableService.SaveTable(requestSaveTable);
            return Ok(responseService);
        }

        [HttpGet]
        [Route("GetTableList")]
        public IActionResult GetTableList()
        {
            var responseService = _tableService.GetTableList();
            return Ok(responseService);
        }
        [HttpGet]
        [Route("GetTablesWithoutReservation")]
        public IActionResult GetTablesWithoutReservation([FromBody] GetTablesWithoutReservationRequestDto requestGetTablesWithoutReservation)
        {
            var responseService = _tableService.GetTablesWithoutReservation(requestGetTablesWithoutReservation);
            return Ok(responseService);
        }
    }
}
