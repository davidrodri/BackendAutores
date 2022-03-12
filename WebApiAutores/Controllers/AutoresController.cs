using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AutoresController : ControllerBase
{
    private readonly ApplicationDbContext context;

    public AutoresController(ApplicationDbContext context) 
    { 
        this.context = context;
    }

    [HttpGet]
    [HttpGet("listado")]
    [HttpGet("/list")]
    public async Task< ActionResult<List<Autor>>> Get()
    {
        return await context.Autores.Include(x => x.Libros).ToListAsync();        
    }

    [HttpGet("primerregistro")]
    public async Task<ActionResult<Autor>> ObtenerPrimerAutor()
    {
        return await context.Autores.FirstOrDefaultAsync();
    }


    [HttpPost]
    public async Task<ActionResult> Post(Autor autor) {
        context.Add(autor);
        await context.SaveChangesAsync();
        return Ok();    
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(Autor autor, int id) {
        if (autor.Id != id)
        {
            return BadRequest("El id del autor no coincide con el id de la URL, revisar");
        }

        var existe = await context.Autores.AnyAsync(x => x.Id == id);
        if (!existe)
        {
            return NotFound();
        }

        context.Update(autor);
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id) { 
        var existe = await context.Autores.AnyAsync(x => x.Id == id);
        if (!existe)
        {
            return NotFound();
        }

        context.Remove(new Autor() { Id = id });
        await context.SaveChangesAsync();
        return Ok();
    }
}
