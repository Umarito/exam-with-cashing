public class ProductInsertDto
{
    public string Name{get;set;}=null!;
    public decimal Price{get;set;}
    public string? Description{get;set;}
}
public class ProductUpdateDto
{
    public int Id{get;set;}
    public string Name{get;set;}=null!;
    public decimal Price{get;set;}
    public string? Description{get;set;}
}
public class ProductGetDto
{
    public int Id{get;set;}
    public string Name{get;set;}=null!;
    public decimal Price{get;set;}
    public string? Description{get;set;}
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
}