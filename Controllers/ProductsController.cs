using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repopattern.Model;
using Repopattern.Repository.Interface;

namespace Repopattern.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            return Ok(product);
        }

        [HttpPost("transaction")]
        public async Task<IActionResult> CreateWithTransaction(List<Product> products)
        {
            if (products == null || !products.Any())
                return BadRequest("Product list is empty");

            await _unitOfWork.BeginTransactionAsync();

            try
            {

                foreach (var dto in products)
                {
                    await _unitOfWork.Products.AddAsync(dto);
                }


                await _unitOfWork.CommitAsync();

                return Ok("Transaction committed");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest($"Transaction failed: {ex.Message}");
            }
        }
    }
}
