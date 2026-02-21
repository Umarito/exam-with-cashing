using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

public class ProductService(IMapper mapper,IProductRepository ProductRepository,ILogger<ProductService> logger,IMemoryCache cache) : IProductService
{
    private readonly IProductRepository _ProductRepository = ProductRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IMemoryCache _cache = cache;
    private readonly ILogger<ProductService> _logger = logger;
    private string cache = "all_products";
    public async Task<Response<string>> AddProductAsync(ProductInsertDto ProductInsertDto)
    {
        try
        {
            var Product = _mapper.Map<Product>(ProductInsertDto);
            await _ProductRepository.AddAsync(Product);
            _cache.Remove(cache);
            return new Response<string>(HttpStatusCode.OK, "Product was added successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int ProductId)
    {
        try
        {
            await _ProductRepository.DeleteAsync(ProductId);
            _cache.Remove(cache);
            return new Response<string>(HttpStatusCode.OK, "Product was deleted successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<List<ProductGetDto>>> GetAllProductsAsync()
    {
        try{
        if (_cache.TryGetValue(cache, out Response<List<ProductGetDto>> cachedData))
        {
            _logger.LogInformation("Cache OK");
            return cachedData;
        }
        _logger.LogInformation("Cache Miss");
        var Products = await _ProductRepository.GetAllProductsAsync();
        if (Products == null)
        {
            return new Response<List<ProductGetDto>>(HttpStatusCode.NotFound,"There is no contact messages");
        }
        else
        { 
            var res = _mapper.Map<Response<List<ProductGetDto>>>(Products);
            var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)).SetSlidingExpiration(TimeSpan.FromMinutes(1)).SetPriority(CacheItemPriority.High);
            _cache.Set(cache, res, cacheOptions);
            return res;
        }
        }
        catch(Exception ex)
        {
            return new Response<List<ProductGetDto>>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<ProductGetDto>> GetProductByIdAsync(int ProductId)
    {
        try
        {
            if (_cache.TryGetValue(cache, out Response<ProductGetDto> cachedData))
            {
                _logger.LogInformation("Cache OK");
                return cachedData;
            }
            _logger.LogInformation("Cache Miss");
            var Product = await _ProductRepository.GetByIdAsync(ProductId);
            if(Product == null)
            {
                return new Response<ProductGetDto>(HttpStatusCode.NotFound,"Product was not found");
            }
            else
            {
                var res = _mapper.Map<ProductGetDto>(Product);
                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)).SetSlidingExpiration(TimeSpan.FromMinutes(1)).SetPriority(CacheItemPriority.High);
                _cache.Set(cache, res, cacheOptions);
                return new Response<ProductGetDto>(HttpStatusCode.OK,"The data that you were searching for:",res);
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<ProductGetDto>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(int ProductId, ProductUpdateDto Product)
    {
        try
        {
            var res = await _ProductRepository.GetByIdAsync(ProductId);

            if (res == null)
            {   
                return new Response<string>(HttpStatusCode.NotFound,"Product not found");
            }
            else
            {
                _mapper.Map(Product, res);
                await _ProductRepository.UpdateAsync(res);
                _cache.Remove(cache);
                return new Response<string>(HttpStatusCode.OK,"Product updated successfully");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }
}