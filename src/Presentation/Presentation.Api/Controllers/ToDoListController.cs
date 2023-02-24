using Domain.DataTransferObjects;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ToDoListController : Controller
    {
        private IToDoListProvider _todoListProvider;

        public ToDoListController(IToDoListProvider todoListProvider)
        {
            _todoListProvider = todoListProvider;
        }

        [ProducesResponseType(typeof(List<ToDoListDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ToDoListDto>>> GetAll() => Ok(await _todoListProvider.GetAll());

        [ProducesResponseType(typeof(List<ToDoListDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("GetByUserId")]
        public async Task<ActionResult<List<ToDoListDto>>> GetByUserId(int userId) => Ok(await _todoListProvider.GetByUserId(userId));

        [ProducesResponseType(typeof(ToDoListDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("AddItemToList")]
        public async Task<ActionResult<ToDoListDto>> AddItemToList(ToDoListDto item) => Ok(await _todoListProvider.AddItemToList(item));

        [ProducesResponseType(typeof(ToDoListDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("CompleteTask/{id}")]
        public async Task<ActionResult<ToDoListDto>> CompleteTask(int id) => Ok(await _todoListProvider.CompleteTask(id));

        [ProducesResponseType(typeof(ToDoListDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("ResetCompleteTask/{id}")]
        public async Task<ActionResult<ToDoListDto>> ResetCompleteTask(int id) => Ok(await _todoListProvider.ResetCompleteTask(id));

        [ProducesResponseType(typeof(ToDoListDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("UpdateDescription/{id}")]
        public async Task<ActionResult<ToDoListDto>> UpdateDescription(int id, string description) => Ok(await _todoListProvider.UpdateDescription(id, description));

        [ProducesResponseType(typeof(ToDoListDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("DeleteTask/{id}")]
        public async Task<ActionResult<bool>> DeleteTask(int id) => Ok(await _todoListProvider.DeleteTask(id));
    }
}
