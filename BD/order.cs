namespace MethodHelper.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("order")]
    public partial class order
    {
        public int id { get; set; }

        public int basket_id { get; set; }

        public int point_id { get; set; }

        public int? group_order { get; set; }

        public virtual basket basket { get; set; }

        public virtual point point { get; set; }
    }
}
