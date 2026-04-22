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
        var response = new Response<Usuario>();

        if (id <= 0)
        {
            response.Errors.Add("Id inválido");
            return BadRequest(response);
        }

        var data = await _repository.GetById(id);

        if (data == null)
        {
            response.Errors.Add("Usuario no encontrado");
            return NotFound(response);
        }

        response.Data = data;
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Usuario>>> Post([FromBody] Usuario usuario)
    {
        var response = new Response<Usuario>();

        if (usuario == null)
        {
            response.Errors.Add("Datos requeridos");
            return BadRequest(response);
        }

        if (usuario.IdRol <= 0)
            response.Errors.Add("IdRol es requerido");

        if (string.IsNullOrWhiteSpace(usuario.Username))
            response.Errors.Add("Username es requerido");

        if (string.IsNullOrWhiteSpace(usuario.Password))
            response.Errors.Add("Password es requerido");

        if (string.IsNullOrWhiteSpace(usuario.Email) || !usuario.Email.Contains("@"))
            response.Errors.Add("Email inválido");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.SaveAsync(usuario);
        response.Data = result;

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Usuario>>> Put([FromBody] Usuario usuario)
    {
        var response = new Response<Usuario>();

        if (usuario.Id <= 0)
            response.Errors.Add("Id es requerido");

        if (usuario.IdRol <= 0)
            response.Errors.Add("IdRol es requerido");

        if (string.IsNullOrWhiteSpace(usuario.Username))
            response.Errors.Add("Username es requerido");

        if (string.IsNullOrWhiteSpace(usuario.Email) || !usuario.Email.Contains("@"))
            response.Errors.Add("Email inválido");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.UpdateAsync(usuario);
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

    [HttpPost("login")]
    public async Task<ActionResult<Response<Usuario>>> Login([FromBody] Usuario usuario)
    {
        var response = new Response<Usuario>();

        if (usuario == null ||
            string.IsNullOrWhiteSpace(usuario.Username) ||
            string.IsNullOrWhiteSpace(usuario.Password))
        {
            response.Errors.Add("Username y Password son requeridos");
            return BadRequest(response);
        }

        var result = await _repository.Login(usuario.Username, usuario.Password);

        if (result == null)
        {
            response.Errors.Add("Credenciales incorrectas");
            return Unauthorized(response);
        }

        response.Data = result;
        return Ok(response);
    }
}