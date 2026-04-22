using Microsoft.AspNetCore.Mvc;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;
using Tecnm26.Ecommerce.Core.Http;

namespace Tecnm26.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleRepository _repository;

    public RolesController(IRoleRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<Rol>>>> GetAll()
    {
        var data = await _repository.GetAllAsync();
        return Ok(new Response<List<Rol>> { Data = data });
    }

    [HttpPost]
    public async Task<ActionResult<Response<Rol>>> Post(Rol rol)
    {
        var result = await _repository.SaveAsync(rol);
        return Ok(new Response<Rol> { Data = result });
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<Rol>>> Get(int id)
    {
        var data = await _repository.GetById(id);

        if (data == null)
            return NotFound(new Response<Rol> { Errors = { "Rol no encontrado" } });

        return Ok(new Response<Rol> { Data = data });
    }

    [HttpPut]
    public async Task<ActionResult<Response<Rol>>> Put([FromBody] Rol rol)
    {
        var result = await _repository.UpdateAsync(rol);
        return Ok(new Response<Rol> { Data = result });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var result = await _repository.DeleteAsync(id);
        return Ok(new Response<bool> { Data = result });
    }
}