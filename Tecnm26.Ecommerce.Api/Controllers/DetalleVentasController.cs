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
        var response = new Response<Detalle_Ventas>();

        if (id <= 0)
        {
            response.Errors.Add("Id inválido");
            return BadRequest(response);
        }

        var data = await _repository.GetById(id);

        if (data == null)
        {
            response.Errors.Add("Detalle de venta no encontrado");
            return NotFound(response);
        }

        response.Data = data;
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Detalle_Ventas>>> Post([FromBody] Detalle_Ventas detalle)
    {
        var response = new Response<Detalle_Ventas>();

        if (detalle == null)
        {
            response.Errors.Add("Datos requeridos");
            return BadRequest(response);
        }

        if (detalle.IdVenta <= 0)
            response.Errors.Add("IdVenta es requerido");

        if (detalle.IdLibro <= 0)
            response.Errors.Add("IdLibro es requerido");

        if (detalle.Cantidad <= 0)
            response.Errors.Add("Cantidad debe ser mayor a 0");

        if (detalle.PrecioUnitario <= 0)
            response.Errors.Add("PrecioUnitario debe ser mayor a 0");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.SaveAsync(detalle);
        response.Data = result;

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Detalle_Ventas>>> Put([FromBody] Detalle_Ventas detalle)
    {
        var response = new Response<Detalle_Ventas>();

        if (detalle.Id <= 0)
            response.Errors.Add("Id es requerido");

        if (detalle.IdVenta <= 0)
            response.Errors.Add("IdVenta es requerido");

        if (detalle.IdLibro <= 0)
            response.Errors.Add("IdLibro es requerido");

        if (detalle.Cantidad <= 0)
            response.Errors.Add("Cantidad debe ser mayor a 0");

        if (detalle.PrecioUnitario <= 0)
            response.Errors.Add("PrecioUnitario debe ser mayor a 0");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.UpdateAsync(detalle);
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