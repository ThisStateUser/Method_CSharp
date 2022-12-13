namespace MethodHelper.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public users()
        {
            app_settings = new HashSet<app_settings>();
            basket = new HashSet<basket>();
            ip_address = new HashSet<ip_address>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string first_name { get; set; }

        [StringLength(50)]
        public string last_name { get; set; }

        public int role_id { get; set; }

        public int class_id { get; set; }

        [Required]
        [StringLength(50)]
        public string login { get; set; }

        [Required]
        public string password { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        [StringLength(50)]
        public string token { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<app_settings> app_settings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<basket> basket { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ip_address> ip_address { get; set; }

        public virtual user_class user_class { get; set; }

        public virtual user_role user_role { get; set; }
    }
}
