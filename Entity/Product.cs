namespace API.Entity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
