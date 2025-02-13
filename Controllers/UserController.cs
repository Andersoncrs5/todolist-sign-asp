using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListApi.DTOs;
using ToDoListApi.Entities;
using ToDoListApi.SetUnitOfWork;

namespace ToDoListApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _uof;

    public UserController(IUnitOfWork uof)
    {
        _uof = uof;
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<User>> Get(ulong id)
    {
        try
        {
            if (id == 0)
                return BadRequest("Id is required");

            User user = await _uof.UserRepository.GetAsync(id);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e.Message}");
        }
    }

    [HttpPost("create")]
    public async Task<ActionResult<User>> Create([FromBody] User user)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User userCreated = await _uof.UserRepository.CreateAsync(user);

            if (userCreated == null)
                return StatusCode(StatusCodes.Status304NotModified, "User creation failed");

            await _uof.Commit();
            return StatusCode(StatusCodes.Status201Created, userCreated);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e.Message}");
        }
    }

    [HttpPut("update")]
    public async Task<ActionResult<User>> Update([FromBody] User user)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User userUpdated = await _uof.UserRepository.UpdateAsync(user);

            if (userUpdated == null)
                return NotFound("User not found for update");

            await _uof.Commit();
            return Ok(userUpdated);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e.Message}");
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<User>> Delete(ulong id)
    {
        try
        {
            if (id == 0)
                return BadRequest("Id is required");

            User user = await _uof.UserRepository.DeleteAsync(id);

            if (user == null)
                return NotFound("User could not be deleted");

            bool check = await _uof.TasksRepository.DeleteAllAsync(id);

            await _uof.Commit();
            return Ok(user);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e.Message}");
        }
    }

    [HttpGet("getAll/{id}")]
    public async Task<ActionResult<List<Tasks>>> GetAll(ulong id)
    {
        try
        {
            if (id == 0)
                return BadRequest("Id is required");

            User user = await _uof.UserRepository.GetAsync(id);

            if (user == null)
                return NotFound("User not found");

            List<Tasks> taskList = await this._uof.TasksRepository.GetAllAsync(id);

            return Ok(taskList);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e.Message}");
        }
    }

    [HttpPost("Login")]
    public async Task<ActionResult<User>> Login([FromBody] UserLoginDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool check = await _uof.UserRepository.LoginAsync(dto);

            if (check == false)
                return BadRequest(false);

            return Ok(true);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e.Message}");
        }
    }

}