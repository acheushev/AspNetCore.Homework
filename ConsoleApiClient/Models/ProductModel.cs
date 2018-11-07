using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApiClient.Models
{
    public class ProductModel
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

       
    }
}