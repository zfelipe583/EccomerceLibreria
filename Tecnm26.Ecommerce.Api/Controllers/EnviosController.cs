using Microsoft.AspNetCore.Mvc;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;
using Tecnm26.Ecommerce.Core.Http;

namespace Tecnm26.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnviosController : ControllerBase
{
    private readonly IEnviosRepository _repository;

    public EnviosController(IEnviosRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<Envios>>>> GetAll()
    {
        var data = await _repository.GetAllAsync();
        return Ok(new Response<List<Envios>> { Data = data });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<Envios>>> Get(int id)
    {
        var response = new Response<Envios>();

        if (id <= 0)
        {
            response.Errors.Add("Id inválido");
            return BadRequest(response);
        }

        var data = await _repository.GetById(id);

        if (data == null)
        {
            response.Errors.Add("Envío no encontrado");
            return NotFound(response);
        }

        response.Data = data;
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Envios>>> Post([FromBody] Envios envio)
    {
        var response = new Response<Envios>();

        if (envio == null)
        {
            response.Errors.Add("Datos requeridos");
            return BadRequest(response);
        }

        if (envio.IdVenta <= 0)
            response.Errors.Add("IdVenta es requerido");

        if (string.IsNullOrWhiteSpace(envio.DireccionEnvio))
            response.Errors.Add("DireccionEnvio es requerida");

        if (string.IsNullOrWhiteSpace(envio.Ciudad))
            response.Errors.Add("Ciudad es requerida");

        if (string.IsNullOrWhiteSpace(envio.CodigoPostal))
            response.Errors.Add("CodigoPostal es requerido");

        if (!string.IsNullOrWhiteSpace(envio.EstadoEnvio) && envio.EstadoEnvio.Length > 50)
            response.Errors.Add("EstadoEnvio no puede superar 50 caracteres");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.SaveAsync(envio);
        response.Data = result;

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Envios>>> Put([FromBody] Envios envio)
    {
        var response = new Response<Envios>();

        if (envio.Id <= 0)
            response.Errors.Add("Id es requerido");

        if (envio.IdVenta <= 0)
            response.Errors.Add("IdVenta es requerido");

        if (string.IsNullOrWhiteSpace(envio.DireccionEnvio))
            response.Errors.Add("DireccionEnvio es requerida");

        if (string.IsNullOrWhiteSpace(envio.Ciudad))
            response.Errors.Add("Ciudad es requerida");

        if (string.IsNullOrWhiteSpace(envio.CodigoPostal))
            response.Errors.Add("CodigoPostal es requerido");

        if (!string.IsNullOrWhiteSpace(envio.EstadoEnvio) && envio.EstadoEnvio.Length > 50)
            response.Errors.Add("EstadoEnvio no puede superar 50 caracteres");

        if (response.Errors.Count > 0)
            return BadRequest(response);

        var result = await _repository.UpdateAsync(envio);
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