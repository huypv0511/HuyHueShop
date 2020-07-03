namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
       
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Fullname { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string GroupID { get; set; }

        [StringLength(50)]
        public string SDT { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Datetime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Date { get; set; }

        public bool? Permission { get; set; }

        public decimal? Money { get; set; }

        public bool? Status { get; set; }
    }
}
