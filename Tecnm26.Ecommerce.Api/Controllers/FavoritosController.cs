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
        var response = new Response<Favoritos>();

        if (id <= 0)
        {
            response.Errors.Add("Id inválido");
            return BadRequest(response);
        }

        var data = await _repository.GetById(id);

        if (data == null)
        {
            response.Errors.Add("Favorito no encontrado");
            return NotFound(response);
        }

        response.Data = data;
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Favoritos>>> Post([FromBody] Favoritos favorito)
    {
        var response = new Response<Favoritos>();

        if (favorito == null)
        {
            response.Errors.Add("Datos requeridos");
            return BadRequest(response);
        }

        if (favorito.IdUsuario <= 0)
            response.Errors.Add("IdUsuario es requerido");

        if (favorito.IdLibro <= 0)
            response.Errors.Add("IdLibro es requerido");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.SaveAsync(favorito);
        response.Data = result;

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Favoritos>>> Put([FromBody] Favoritos favorito)
    {
        var response = new Response<Favoritos>();

        if (favorito.Id <= 0)
            response.Errors.Add("Id es requerido");

        if (favorito.IdUsuario <= 0)
            response.Errors.Add("IdUsuario es requerido");

        if (favorito.IdLibro <= 0)
            response.Errors.Add("IdLibro es requerido");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.UpdateAsync(favorito);
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