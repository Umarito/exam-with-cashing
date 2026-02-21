public interface IProductService
{
    Task<Response<string>> AddProductAsync(ProductInsertDto ProductInsertDto);
    Task<Response<List<ProductGetDto>>> GetAllProductsAsync();
    Task<Response<ProductGetDto>> GetProductByIdAsync(int ProductId);
    Task<Response<string>> DeleteAsync(int ProductId);
    Task<Response<string>> UpdateAsync(int ProductId,ProductUpdateDto ProductUpdateDto);
}