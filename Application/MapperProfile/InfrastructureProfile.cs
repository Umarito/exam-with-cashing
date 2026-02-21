using AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Product,ProductGetDto>();
        CreateMap<ProductInsertDto,Product>();
        CreateMap<ProductUpdateDto,Product>();
    }
}