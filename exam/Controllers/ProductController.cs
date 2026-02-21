using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService ProductService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> AddProductAsync(ProductInsertDto Product)
    {
        return await ProductService.AddProductAsync(Product);
    }
    [HttpPut("{ProductId}")]
    public async Task<Response<string>> UpdateAsync(int ProductId,ProductUpdateDto Product)
    {
        return await ProductService.UpdateAsync(ProductId,Product);
    }
    [HttpDelete("{ProductId}")]
    public async Task<Response<string>> DeleteAsync(int ProductId)
    {
        return await ProductService.DeleteAsync(ProductId);
    }
    [HttpGet]
    public async Task<Response<List<ProductGetDto>>> GetAllProducts()
    {
        return await ProductService.GetAllProductsAsync();   
    }
    
    [HttpGet("{ProductId}")]
    public async Task<Response<ProductGetDto>> GetProductByIdAsync(int ProductId)
    {
        return await ProductService.GetProductByIdAsync(ProductId);
    }
}