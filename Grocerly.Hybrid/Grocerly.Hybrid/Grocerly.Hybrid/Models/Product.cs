using System;
namespace Grocerly.Hybrid.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Quantity { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
