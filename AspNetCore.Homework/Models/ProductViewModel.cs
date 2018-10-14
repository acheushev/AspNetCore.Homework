using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AspNetCore.Homework.Models
{
    public class ProductViewModel
    {
        public int? ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string ProductName { get; set; }

        [Required]
        public int? SupplierId { get; set; }

        [Required]
        public int? CategoryId { get; set; }
        [Required]
        public string QuantityPerUnit { get; set; }

        [DataType(DataType.Currency)]
        [Range(0, 9999999.99)]
        [Required]
        public decimal? UnitPrice { get; set; }
        [Required]
        public short? UnitsInStock { get; set; }
        [Required]
        public short? UnitsOnOrder { get; set; }
        [Required]
        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        /*  public Categories Category { Get; set; }
        public Suppliers Supplier { Get; set; }
        public ICollection<OrderDetails> OrderDetails { Get; set; }*/
        public List<SupplierViewModel> AllSuppliers { get; set; }
        public List<CategoryViewModel> AllCategories { get; set; }
    }
}