using Microsoft.AspNetCore.Mvc;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;
using Tecnm26.Ecommerce.Core.Http;

namespace Tecnm26.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibrosController : ControllerBase
{
    private readonly ILibrosRepository _repository;

    public LibrosController(ILibrosRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<Libros>>>> GetAll()
    {
        var data = await _repository.GetAllAsync();
        return Ok(new Response<List<Libros>> { Data = data });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<Libros>>> Get(int id)
    {
        var response = new Response<Libros>();

        if (id <= 0)
        {
            response.Errors.Add("Id inválido");
            return BadRequest(response);
        }

        var data = await _repository.GetById(id);

        if (data == null)
        {
            response.Errors.Add("Libro no encontrado");
            return NotFound(response);
        }

        response.Data = data;
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Libros>>> Post([FromBody] Libros libro)
    {
        var response = new Response<Libros>();

        if (libro == null)
        {
            response.Errors.Add("Datos requeridos");
            return BadRequest(response);
        }

        if (string.IsNullOrWhiteSpace(libro.Titulo))
            response.Errors.Add("El título es obligatorio");

        if (libro.IdAutora <= 0)
            response.Errors.Add("IdAutora es requerido");

        if (libro.Precio <= 0)
            response.Errors.Add("El precio debe ser mayor a 0");

        if (libro.Stock < 0)
            response.Errors.Add("El stock no puede ser negativo");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.SaveAsync(libro);
        response.Data = result;

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Libros>>> Put([FromBody] Libros libro)
    {
        var response = new Response<Libros>();

        if (libro.Id <= 0)
            response.Errors.Add("Id es requerido");

        if (string.IsNullOrWhiteSpace(libro.Titulo))
            response.Errors.Add("El título es obligatorio");

        if (libro.IdAutora <= 0)
            response.Errors.Add("IdAutora es requerido");

        if (libro.Precio <= 0)
            response.Errors.Add("El precio debe ser mayor a 0");

        if (libro.Stock < 0)
            response.Errors.Add("El stock no puede ser negativo");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.UpdateAsync(libro);
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