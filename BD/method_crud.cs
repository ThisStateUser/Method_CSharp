namespace MethodHelper.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class method_crud
    {
        public int id { get; set; }

        [Column(TypeName = "text")]
        public string TextBox { get; set; }

        public int? ComboBox { get; set; }

        public virtual method_crud_combobox method_crud_combobox { get; set; }
    }
}
