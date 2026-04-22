using Microsoft.AspNetCore.Mvc;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;
using Tecnm26.Ecommerce.Core.Http;

namespace Tecnm26.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleRepository _repository;

    public RolesController(IRoleRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<Role>>>> GetAll()
    {
        var data = await _repository.GetAllAsync();
        return Ok(new Response<List<Role>> { Data = data });
    }

    [HttpPost]
    public async Task<ActionResult<Response<Role>>> Post(Role role)
    {
        var result = await _repository.SaveAsync(role);
        return Ok(new Response<Role> { Data = result });
    }
}