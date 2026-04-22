using Microsoft.AspNetCore.Mvc;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;
using Tecnm26.Ecommerce.Core.Http;

namespace Tecnm26.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly IVentasRepository _repository;

    public VentasController(IVentasRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<Ventas>>>> GetAll()
    {
        var data = await _repository.GetAllAsync();
        return Ok(new Response<List<Ventas>> { Data = data });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<Ventas>>> Get(int id)
    {
        var data = await _repository.GetById(id);

        if (data == null)
            return NotFound(new Response<Ventas> { Errors = { "Venta no encontrada" } });

        return Ok(new Response<Ventas> { Data = data });
    }

    [HttpPost]
    public async Task<ActionResult<Response<Ventas>>> Post([FromBody] Ventas venta)
    {
        var result = await _repository.SaveAsync(venta);
        return Ok(new Response<Ventas> { Data = result });
    }

    [HttpPut]
    public async Task<ActionResult<Response<Ventas>>> Put([FromBody] Ventas venta)
    {
        var result = await _repository.UpdateAsync(venta);
        return Ok(new Response<Ventas> { Data = result });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var result = await _repository.DeleteAsync(id);
        return Ok(new Response<bool> { Data = result });
    }
}