using API.DTOs.Product;

namespace API.DTOs.Category
{
    public class GetCategoryDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GetProductDTO> ProductsDTOs { get; set; }
    }
}
