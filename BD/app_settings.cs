namespace MethodHelper.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class app_settings
    {
        public int id { get; set; }

        public int user_id { get; set; }

        public int? start_page { get; set; }

        public bool? menu_anim { get; set; }

        public virtual start_page_desk start_page_desk { get; set; }

        public virtual users users { get; set; }
    }
}
