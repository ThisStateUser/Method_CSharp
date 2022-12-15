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
        public virtual DbSet<basket> basket { get; set; }
        public virtual DbSet<image_in_page> image_in_page { get; set; }
        public virtual DbSet<ip_address> ip_address { get; set; }
        public virtual DbSet<method_crud> method_crud { get; set; }
        public virtual DbSet<method_crud_combobox> method_crud_combobox { get; set; }
        public virtual DbSet<order> order { get; set; }
        public virtual DbSet<page_desc> page_desc { get; set; }
        public virtual DbSet<point> point { get; set; }
        public virtual DbSet<product> product { get; set; }
        public virtual DbSet<start_page_desk> start_page_desk { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<type_image> type_image { get; set; }
        public virtual DbSet<user_class> user_class { get; set; }
        public virtual DbSet<user_role> user_role { get; set; }
        public virtual DbSet<users> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<basket>()
                .HasMany(e => e.order)
                .WithRequired(e => e.basket)
                .HasForeignKey(e => e.basket_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<image_in_page>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<ip_address>()
                .Property(e => e.ip_auth_address)
                .IsUnicode(false);

            modelBuilder.Entity<method_crud>()
                .Property(e => e.row_text)
                .IsUnicode(false);

            modelBuilder.Entity<method_crud_combobox>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<method_crud_combobox>()
                .HasMany(e => e.method_crud)
                .WithOptional(e => e.method_crud_combobox)
                .HasForeignKey(e => e.row_combo);

            modelBuilder.Entity<page_desc>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<page_desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<page_desc>()
                .HasMany(e => e.image_in_page)
                .WithRequired(e => e.page_desc)
                .HasForeignKey(e => e.page_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<point>()
                .HasMany(e => e.order)
                .WithRequired(e => e.point)
                .HasForeignKey(e => e.point_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .Property(e => e.price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<product>()
                .Property(e => e.desk)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.basket)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<start_page_desk>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<start_page_desk>()
                .HasMany(e => e.app_settings)
                .WithOptional(e => e.start_page_desk)
                .HasForeignKey(e => e.start_page);

            modelBuilder.Entity<type_image>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<type_image>()
                .HasMany(e => e.image_in_page)
                .WithRequired(e => e.type_image)
                .HasForeignKey(e => e.type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user_class>()
                .Property(e => e._class)
                .IsUnicode(false);

            modelBuilder.Entity<user_class>()
                .HasMany(e => e.users)
                .WithRequired(e => e.user_class)
                .HasForeignKey(e => e.class_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user_role>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<user_role>()
                .HasMany(e => e.users)
                .WithRequired(e => e.user_role)
                .HasForeignKey(e => e.role_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<users>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.token)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .HasMany(e => e.app_settings)
                .WithRequired(e => e.users)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<users>()
                .HasMany(e => e.basket)
                .WithRequired(e => e.users)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<users>()
                .HasMany(e => e.ip_address)
                .WithRequired(e => e.users)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);
        }
    }
}
