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

        [Required] public int? SupplierId { get; set; }

        [Required] public int? CategoryId { get; set; }

        public string QuantityPerUnit { get; set; }

        [DataType(DataType.Currency)]
        [Range(0, 9999999.99)]
        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        /*  public Categories Category { get; set; }
        public Suppliers Supplier { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }*/
        public List<SupplierViewModel> AllSuppliers { get; set; }
        public List<CategoryViewModel> AllCategories { get; set; }
    }
}