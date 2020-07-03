namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }

        public string Name { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public decimal? Price { get; set; }

        public decimal? PromotionPice { get; set; }

        public string MoreImages { get; set; }

        public int Quantity { get; set; }

        public long? CategoryID { get; set; }

        public string MetaTilte { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public string MetaKeywords { get; set; }

        [StringLength(50)]
        public string MetaDescriptions { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }
    }
}
