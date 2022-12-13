namespace MethodHelper.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("basket")]
    public partial class basket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public basket()
        {
            order = new HashSet<order>();
        }

        public int id { get; set; }

        public int product_id { get; set; }

        public int user_id { get; set; }

        public int count_prod { get; set; }

        public virtual product product { get; set; }

        public virtual users users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> order { get; set; }
    }
}
