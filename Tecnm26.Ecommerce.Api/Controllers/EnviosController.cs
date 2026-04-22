using Microsoft.AspNetCore.Mvc;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;
using Tecnm26.Ecommerce.Core.Http;

namespace Tecnm26.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnviosController : ControllerBase
{
    private readonly IEnviosRepository _repository;

    public EnviosController(IEnviosRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<Envios>>>> GetAll()
    {
        var data = await _repository.GetAllAsync();
        return Ok(new Response<List<Envios>> { Data = data });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<Envios>>> Get(int id)
    {
        var data = await _repository.GetById(id);

        if (data == null)
            return NotFound(new Response<Envios> { Errors = { "Envio no encontrado" } });

        return Ok(new Response<Envios> { Data = data });
    }

    [HttpPost]
    public async Task<ActionResult<Response<Envios>>> Post([FromBody] Envios envio)
    {
        var result = await _repository.SaveAsync(envio);
        return Ok(new Response<Envios> { Data = result });
    }

    [HttpPut]
    public async Task<ActionResult<Response<Envios>>> Put([FromBody] Envios envio)
    {
        var result = await _repository.UpdateAsync(envio);
        return Ok(new Response<Envios> { Data = result });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var result = await _repository.DeleteAsync(id);
        return Ok(new Response<bool> { Data = result });
    }
}