using Microsoft.AspNetCore.Mvc;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;
using Tecnm26.Ecommerce.Core.Http;

namespace Tecnm26.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetalleLibrosController : ControllerBase
{
    private readonly IDetalleLibrosRepository _repository;

    public DetalleLibrosController(IDetalleLibrosRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<Detalle_Libros>>>> GetAll()
    {
        var data = await _repository.GetAllAsync();
        return Ok(new Response<List<Detalle_Libros>> { Data = data });
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<Detalle_Libros>>> Get(int id)
    {
        var data = await _repository.GetById(id);

        if (data == null)
            return NotFound(new Response<Detalle_Libros> { Errors = { "Detalle no encontrado" } });

        return Ok(new Response<Detalle_Libros> { Data = data });
    }

    [HttpPost]
    public async Task<ActionResult<Response<Detalle_Libros>>> Post([FromBody] Detalle_Libros detalle)
    {
        var result = await _repository.SaveAsync(detalle);
        return Ok(new Response<Detalle_Libros> { Data = result });
    }

    [HttpPut]
    public async Task<ActionResult<Response<Detalle_Libros>>> Put([FromBody] Detalle_Libros detalle)
    {
        var result = await _repository.UpdateAsync(detalle);
        return Ok(new Response<Detalle_Libros> { Data = result });
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var result = await _repository.DeleteAsync(id);
        return Ok(new Response<bool> { Data = result });
    }
}