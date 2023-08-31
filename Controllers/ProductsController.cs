using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        //injeção de dependencia e inversão de controle, do mesmo modo feito no spring boot
        private readonly ProductsContext _context;

        public ProductsController(ProductsContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/products")]
        public async Task<ActionResult> GetProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }
        //criação do metodo construtivo post
        [HttpPost]
        [Route("/products")]
        public async Task<ActionResult> CreateProduct(Product product)
        {
           await _context.Products.AddAsync(product);
           await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut]
        [Route("/products")]
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            var dbProduct = await _context.Products.FindAsync(product.Id);

            if (dbProduct == null)
                return NotFound();

            dbProduct.Name = product.Name;
            dbProduct.Price = product.Price;
            dbProduct.Category = product.Category;

            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete]
        [Route("/products")]
        public async Task<ActionResult> UpdateProduct(Guid id)
        {
            var dbProduct = await _context.Products.FindAsync(id);

            if (dbProduct == null)
                return NotFound();

            _context.Products.Remove(dbProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }




    }
}
