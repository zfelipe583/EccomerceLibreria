using Microsoft.AspNetCore.Mvc;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;
using Tecnm26.Ecommerce.Core.Http;

namespace Tecnm26.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BooksController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<Book>>>> GetAll()
    {
        var books = await _bookRepository.GetAllAsync();
        var response = new Response<List<Book>> { Data = books };
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<Book>>> Get(int id)
    {
        var book = await _bookRepository.GetById(id);
        var response = new Response<Book> { Data = book };
        if (book == null)
        {
            response.Errors.Add("Book not found.");
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Book>>> Post([FromBody] Book book)
    {
        if (book.Price <= 0) return BadRequest(new Response<Book> { Errors = { "Invalid Price" } });
        
        var result = await _bookRepository.SaveAsync(book);
        return Created($"/api/Books/{result.Id}", new Response<Book> { Data = result });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var result = await _bookRepository.DeleteAsync(id);
        return Ok(new Response<bool> { Data = result });
    }
}