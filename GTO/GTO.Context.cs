﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GTO
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GTOEntities : DbContext
    {
        public GTOEntities()
            : base("name=GTOEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Headquarters> Headquarters { get; set; }
        public virtual DbSet<Medal> Medal { get; set; }
        public virtual DbSet<Normativs> Normativs { get; set; }
        public virtual DbSet<NormativSubTypes> NormativSubTypes { get; set; }
        public virtual DbSet<NormativTypes> NormativTypes { get; set; }
        public virtual DbSet<Personal_data> Personal_data { get; set; }
        public virtual DbSet<Results> Results { get; set; }
        public virtual DbSet<Results_people> Results_people { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Standarts> Standarts { get; set; }
        public virtual DbSet<Steps> Steps { get; set; }
    }
}
