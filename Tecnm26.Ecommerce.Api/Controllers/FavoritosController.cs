using Microsoft.AspNetCore.Mvc;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;
using Tecnm26.Ecommerce.Core.Http;

namespace Tecnm26.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritosController : ControllerBase
{
    private readonly IFavoritosRepository _repository;

    public FavoritosController(IFavoritosRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<Favoritos>>>> GetAll()
    {
        var data = await _repository.GetAllAsync();
        return Ok(new Response<List<Favoritos>> { Data = data });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<Favoritos>>> Get(int id)
    {
        var data = await _repository.GetById(id);

        if (data == null)
            return NotFound(new Response<Favoritos> { Errors = { "Favorito no encontrado" } });

        return Ok(new Response<Favoritos> { Data = data });
    }

    [HttpPost]
    public async Task<ActionResult<Response<Favoritos>>> Post([FromBody] Favoritos favorito)
    {
        var result = await _repository.SaveAsync(favorito);
        return Ok(new Response<Favoritos> { Data = result });
    }

    [HttpPut]
    public async Task<ActionResult<Response<Favoritos>>> Put([FromBody] Favoritos favorito)
    {
        var result = await _repository.UpdateAsync(favorito);
        return Ok(new Response<Favoritos> { Data = result });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var result = await _repository.DeleteAsync(id);
        return Ok(new Response<bool> { Data = result });
    }
}