using Microsoft.AspNetCore.Mvc;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;
using Tecnm26.Ecommerce.Core.Http;

namespace Tecnm26.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutorasController : ControllerBase
{
    private readonly IAutoraRepository _repository;

    public AutorasController(IAutoraRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<Autora>>>> GetAll()
    {
        var data = await _repository.GetAllAsync();
        return Ok(new Response<List<Autora>> { Data = data });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<Autora>>> Get(int id)
    {
        var response = new Response<Autora>();

        if (id <= 0)
        {
            response.Errors.Add("Id inválido");
            return BadRequest(response);
        }

        var data = await _repository.GetById(id);

        if (data == null)
        {
            response.Errors.Add("Autora no encontrada");
            return NotFound(response);
        }

        response.Data = data;
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Autora>>> Post([FromBody] Autora autora)
    {
        var response = new Response<Autora>();

        if (autora == null)
        {
            response.Errors.Add("Datos requeridos");
            return BadRequest(response);
        }

        if (string.IsNullOrWhiteSpace(autora.Nombre))
            response.Errors.Add("Nombre es requerido");

        if (autora.TotalLibrosEscritos < 0)
            response.Errors.Add("TotalLibrosEscritos no puede ser negativo");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.SaveAsync(autora);
        response.Data = result;

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Autora>>> Put([FromBody] Autora autora)
    {
        var response = new Response<Autora>();

        if (autora.Id <= 0)
            response.Errors.Add("Id es requerido");

        if (string.IsNullOrWhiteSpace(autora.Nombre))
            response.Errors.Add("Nombre es requerido");

        if (autora.TotalLibrosEscritos < 0)
            response.Errors.Add("TotalLibrosEscritos no puede ser negativo");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.UpdateAsync(autora);
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