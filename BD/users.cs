namespace MethodHelper.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class users
    {
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

        public virtual user_class user_class { get; set; }

        public virtual user_role user_role { get; set; }
    }
}
