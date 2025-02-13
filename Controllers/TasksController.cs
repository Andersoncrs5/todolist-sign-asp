using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Entities;
using ToDoListApi.SetUnitOfWork;

namespace ToDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public TasksController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Tasks>> Create([FromBody] Tasks tasks )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Tasks task = await this._uof.TasksRepository.CreateAsync(tasks);

                User user = await this._uof.UserRepository.GetAsync(tasks.FkUserId);

                if (user is null)
                    return BadRequest($"Not exists user with id : {tasks.FkUserId}");
                await this._uof.Commit();

                return tasks;
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error:{e}");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Tasks>> Update([FromBody] Tasks tasks)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                User user = await this._uof.UserRepository.GetAsync(tasks.FkUserId);

                if (user is null)
                    return BadRequest($"Not exists user with id : {tasks.FkUserId}");

                Tasks task = await this._uof.TasksRepository.UpdateAsync(tasks);
                
                await this._uof.Commit();

                return tasks;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error:{e}");
            }
        }

        [HttpGet("Get/{Id}")]
        public async Task<ActionResult<Tasks>> Get(ulong Id)
        {
            try
            {
                if (Id == 0)
                    return BadRequest("Id is required");

                Tasks task = await this._uof.TasksRepository.GetAsync(Id);

                if (task is null)
                    return StatusCode(StatusCodes.Status404NotFound, "Task not found");

                return StatusCode(StatusCodes.Status302Found, task);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error:{e}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Tasks>> Delete(ulong id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Id is required");

                Tasks task = await this._uof.TasksRepository.DeleteAsync(id);

                if (task is null)
                    return BadRequest("Error the to delete task");

                await this._uof.Commit();
                return Ok(task);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error:{e}");
            }
        }

        [HttpDelete("DeleteAll/{id}")]
        public async Task<ActionResult<Tasks>> DeleteAll(ulong id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Id is required");

                bool check = await this._uof.TasksRepository.DeleteAllAsync(id);

                if (check == false)
                    return BadRequest("Error the to delete task");

                await this._uof.Commit();
                return Ok($"Tasks deleted ? {check}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error:{e}");
            }
        }

        [HttpGet("AlterStatus/{id}")]
        public async Task<ActionResult<Tasks>> AlterStatusDone(ulong id)
        {
            try
            {

                if (id == 0)
                    return BadRequest("Id is required");

                Tasks task = await this._uof.TasksRepository.GetAsync(id);

                if (task is null)
                    return StatusCode(StatusCodes.Status404NotFound, "Task not found");

                Tasks taskChange = await this._uof.TasksRepository.AlterStatusDoneAsync(id);

                await this._uof.Commit();
                return Ok(taskChange);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error:{e}");
            }
        }

    }
}
