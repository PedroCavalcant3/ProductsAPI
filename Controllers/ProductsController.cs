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
        public async Task<ActionResult> GetProducts()
        {
            return Ok(_context.Products.ToListAsync());
        }
        //criação do metodo construtivo post
        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product product)
        {
           await _context.Products.AddAsync(product);

           await _context.SaveChangesAsync();

            return Ok(product);

        }
        
        
    }
}
