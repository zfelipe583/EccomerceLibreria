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
        var response = new Response<Ventas>();

        if (id <= 0)
        {
            response.Errors.Add("Id inválido");
            return BadRequest(response);
        }

        var data = await _repository.GetById(id);

        if (data == null)
        {
            response.Errors.Add("Venta no encontrada");
            return NotFound(response);
        }

        response.Data = data;
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Ventas>>> Post([FromBody] Ventas venta)
    {
        var response = new Response<Ventas>();

        if (venta == null)
        {
            response.Errors.Add("Datos requeridos");
            return BadRequest(response);
        }

        if (venta.IdUsuario <= 0)
            response.Errors.Add("IdUsuario es requerido");

        if (venta.Total <= 0)
            response.Errors.Add("El total debe ser mayor a 0");

        if (string.IsNullOrWhiteSpace(venta.MetodoPago))
            response.Errors.Add("MetodoPago es requerido");

        if (!string.IsNullOrWhiteSpace(venta.MetodoPago) && venta.MetodoPago.Length > 50)
            response.Errors.Add("MetodoPago no puede exceder 50 caracteres");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.SaveAsync(venta);
        response.Data = result;

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Ventas>>> Put([FromBody] Ventas venta)
    {
        var response = new Response<Ventas>();

        if (venta.Id <= 0)
            response.Errors.Add("Id es requerido");

        if (venta.IdUsuario <= 0)
            response.Errors.Add("IdUsuario es requerido");

        if (venta.Total <= 0)
            response.Errors.Add("El total debe ser mayor a 0");

        if (string.IsNullOrWhiteSpace(venta.MetodoPago))
            response.Errors.Add("MetodoPago es requerido");

        if (!string.IsNullOrWhiteSpace(venta.MetodoPago) && venta.MetodoPago.Length > 50)
            response.Errors.Add("MetodoPago no puede exceder 50 caracteres");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.UpdateAsync(venta);
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