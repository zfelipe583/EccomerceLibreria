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
        var response = new Response<Detalle_Libros>();

        if (id <= 0)
        {
            response.Errors.Add("Id inválido");
            return BadRequest(response);
        }

        var data = await _repository.GetById(id);

        if (data == null)
        {
            response.Errors.Add("Detalle no encontrado");
            return NotFound(response);
        }

        response.Data = data;
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Detalle_Libros>>> Post([FromBody] Detalle_Libros detalle)
    {
        var response = new Response<Detalle_Libros>();

        if (detalle == null)
        {
            response.Errors.Add("Datos requeridos");
            return BadRequest(response);
        }

        if (detalle.IdLibro <= 0)
            response.Errors.Add("IdLibro es requerido");

        if (detalle.Paginas < 0)
            response.Errors.Add("Paginas no puede ser negativo");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.SaveAsync(detalle);
        response.Data = result;

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Detalle_Libros>>> Put([FromBody] Detalle_Libros detalle)
    {
        var response = new Response<Detalle_Libros>();

        if (detalle.Id <= 0)
            response.Errors.Add("Id es requerido");

        if (detalle.IdLibro <= 0)
            response.Errors.Add("IdLibro es requerido");

        if (detalle.Paginas < 0)
            response.Errors.Add("Paginas no puede ser negativo");

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