﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NolekWPF.DataAccess
{
    using Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class wiki_nolek_dk_dbEntities : DbContext
    {
        public wiki_nolek_dk_dbEntities()
            : base("name=wiki_nolek_dk_dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public virtual DbSet<EquipmentComponent> EquipmentComponents { get; set; }
        public virtual DbSet<EquipmentConfiguration> EquipmentConfigurations { get; set; }
        public virtual DbSet<EquipmentType> EquipmentTypes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        
        public virtual DbSet<Error> Errors { get; set; }
        public virtual DbSet<ContactPerson> ContactPersons { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerDepartment> CustomerDepartments { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Nolek> Noleks { get; set; }
        
    }
}
