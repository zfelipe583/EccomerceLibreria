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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<Rol>>> Get(int id)
    {
        var response = new Response<Rol>();

        if (id <= 0)
        {
            response.Errors.Add("Id inválido");
            return BadRequest(response);
        }

        var data = await _repository.GetById(id);

        if (data == null)
        {
            response.Errors.Add("Rol no encontrado");
            return NotFound(response);
        }

        response.Data = data;
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Rol>>> Post([FromBody] Rol rol)
    {
        var response = new Response<Rol>();

        if (rol == null)
        {
            response.Errors.Add("Datos requeridos");
            return BadRequest(response);
        }

        if (string.IsNullOrWhiteSpace(rol.Nombre))
            response.Errors.Add("El nombre del rol es obligatorio");

        if (rol.Nombre != null && rol.Nombre.Length > 50)
            response.Errors.Add("El nombre del rol no puede exceder 50 caracteres");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.SaveAsync(rol);
        response.Data = result;

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Rol>>> Put([FromBody] Rol rol)
    {
        var response = new Response<Rol>();

        if (rol.Id <= 0)
            response.Errors.Add("Id es requerido");

        if (string.IsNullOrWhiteSpace(rol.Nombre))
            response.Errors.Add("El nombre del rol es obligatorio");

        if (rol.Nombre != null && rol.Nombre.Length > 50)
            response.Errors.Add("El nombre del rol no puede exceder 50 caracteres");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.UpdateAsync(rol);
        response.Data = result;

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();

        if (id <= 0)
        {
            response.Errors.Add("Id inválido");
            return BadRequest(response);
        }

        var result = await _repository.DeleteAsync(id);
        response.Data = result;

        return Ok(response);
    }
}