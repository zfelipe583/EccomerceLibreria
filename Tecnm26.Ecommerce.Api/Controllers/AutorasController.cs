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
        var data = await _repository.GetById(id);

        if (data == null)
            return NotFound(new Response<Autora> { Errors = { "Autora no encontrada" } });

        return Ok(new Response<Autora> { Data = data });
    }

    [HttpPost]
    public async Task<ActionResult<Response<Autora>>> Post([FromBody] Autora autora)
    {
        var result = await _repository.SaveAsync(autora);
        return Ok(new Response<Autora> { Data = result });
    }

    [HttpPut]
    public async Task<ActionResult<Response<Autora>>> Put([FromBody] Autora autora)
    {
        var result = await _repository.UpdateAsync(autora);
        return Ok(new Response<Autora> { Data = result });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var result = await _repository.DeleteAsync(id);
        return Ok(new Response<bool> { Data = result });
    }
}