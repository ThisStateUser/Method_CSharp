using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MethodHelper.BD
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<app_settings> app_settings { get; set; }
        public virtual DbSet<method_crud> method_crud { get; set; }
        public virtual DbSet<method_crud_combobox> method_crud_combobox { get; set; }
        public virtual DbSet<start_page_desk> start_page_desk { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<method_crud>()
                .Property(e => e.TextBox)
                .IsUnicode(false);

            modelBuilder.Entity<method_crud_combobox>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<method_crud_combobox>()
                .HasMany(e => e.method_crud)
                .WithOptional(e => e.method_crud_combobox)
                .HasForeignKey(e => e.ComboBox);

            modelBuilder.Entity<start_page_desk>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<start_page_desk>()
                .HasMany(e => e.app_settings)
                .WithOptional(e => e.start_page_desk)
                .HasForeignKey(e => e.start_page);
        }
    }
}
