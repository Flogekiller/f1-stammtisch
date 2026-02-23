using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        return await context.Products.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> Get(int id)
    {
        Product? product = await context.Products.FindAsync(id);

        return product == null ? (ActionResult<Product>)NotFound() : (ActionResult<Product>)product;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        context.Entry(product).State = EntityState.Modified;

        try
        {
            _ = await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Post(Product product)
    {
        _ = context.Products.Add(product);
        _ = await context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Product? product = await context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _ = context.Products.Remove(product);
        _ = await context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(int id)
    {
        return context.Products.Any(e => e.Id == id);
    }
}