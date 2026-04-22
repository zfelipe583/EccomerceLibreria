using Microsoft.AspNetCore.Mvc;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;
using Tecnm26.Ecommerce.Core.Http;

namespace Tecnm26.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioRepository _repository;

    public UsuariosController(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<Usuario>>>> GetAll()
    {
        var data = await _repository.GetAllAsync();
        return Ok(new Response<List<Usuario>> { Data = data });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<Usuario>>> Get(int id)
    {
        var data = await _repository.GetById(id);

        if (data == null)
        {
            return NotFound(new Response<Usuario> { Errors = { "Usuario no encontrado" } });
        }

        return Ok(new Response<Usuario> { Data = data });
    }

    [HttpPost]
    public async Task<ActionResult<Response<Usuario>>> Post([FromBody] Usuario usuario)
    {
        var result = await _repository.SaveAsync(usuario);
        return Ok(new Response<Usuario> { Data = result });
    }

    [HttpPut]
    public async Task<ActionResult<Response<Usuario>>> Put([FromBody] Usuario usuario)
    {
        var result = await _repository.UpdateAsync(usuario);
        return Ok(new Response<Usuario> { Data = result });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var result = await _repository.DeleteAsync(id);
        return Ok(new Response<bool> { Data = result });
    }
}