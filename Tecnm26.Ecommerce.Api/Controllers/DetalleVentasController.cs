using Microsoft.AspNetCore.Mvc;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;
using Tecnm26.Ecommerce.Core.Http;

namespace Tecnm26.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetalleVentasController : ControllerBase
{
    private readonly IDetalleVentasRepository _repository;

    public DetalleVentasController(IDetalleVentasRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<Detalle_Ventas>>>> GetAll()
    {
        var data = await _repository.GetAllAsync();
        return Ok(new Response<List<Detalle_Ventas>> { Data = data });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<Detalle_Ventas>>> Get(int id)
    {
        var data = await _repository.GetById(id);

        if (data == null)
            return NotFound(new Response<Detalle_Ventas> { Errors = { "Detalle no encontrado" } });

        return Ok(new Response<Detalle_Ventas> { Data = data });
    }

    [HttpPost]
    public async Task<ActionResult<Response<Detalle_Ventas>>> Post([FromBody] Detalle_Ventas detalle)
    {
        var result = await _repository.SaveAsync(detalle);
        return Ok(new Response<Detalle_Ventas> { Data = result });
    }

    [HttpPut]
    public async Task<ActionResult<Response<Detalle_Ventas>>> Put([FromBody] Detalle_Ventas detalle)
    {
        var result = await _repository.UpdateAsync(detalle);
        return Ok(new Response<Detalle_Ventas> { Data = result });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var result = await _repository.DeleteAsync(id);
        return Ok(new Response<bool> { Data = result });
    }
}