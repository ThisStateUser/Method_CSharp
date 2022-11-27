namespace MethodHelper.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ip_address
    {
        public int id { get; set; }

        public int user_id { get; set; }

        [Required]
        [StringLength(50)]
        public string ip_auth_address { get; set; }

        [Required]
        [StringLength(50)]
        public string computer_name { get; set; }

        public bool remember { get; set; }

        public virtual users users { get; set; }
    }
}
