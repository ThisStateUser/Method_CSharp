namespace MethodHelper.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product")]
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            basket = new HashSet<basket>();
        }

        public int id { get; set; }

        public decimal price { get; set; }

        public int count { get; set; }

        [Required]
        [StringLength(50)]
        public string title { get; set; }

        [Column(TypeName = "text")]
        public string desk { get; set; }

        [Column(TypeName = "image")]
        public byte[] image { get; set; }

        [StringLength(50)]
        public string article { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<basket> basket { get; set; }
    }
}
