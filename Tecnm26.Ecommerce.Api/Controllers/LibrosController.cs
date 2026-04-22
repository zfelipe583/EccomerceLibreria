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
        var data = await _repository.GetById(id);

        if (data == null)
            return NotFound(new Response<Libros> { Errors = { "Libro no encontrado" } });

        return Ok(new Response<Libros> { Data = data });
    }

    [HttpPost]
    public async Task<ActionResult<Response<Libros>>> Post([FromBody] Libros libro)
    {
        var result = await _repository.SaveAsync(libro);
        return Ok(new Response<Libros> { Data = result });
    }

    [HttpPut]
    public async Task<ActionResult<Response<Libros>>> Put([FromBody] Libros libro)
    {
        var result = await _repository.UpdateAsync(libro);
        return Ok(new Response<Libros> { Data = result });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var result = await _repository.DeleteAsync(id);
        return Ok(new Response<bool> { Data = result });
    }
}