﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcKutuphane.Models.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Dbo_KutuphaneEntities : DbContext
    {
        public Dbo_KutuphaneEntities()
            : base("name=Dbo_KutuphaneEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tbl_Cezalar> Tbl_Cezalar { get; set; }
        public virtual DbSet<Tbl_Hareket> Tbl_Hareket { get; set; }
        public virtual DbSet<Tbl_Kasa> Tbl_Kasa { get; set; }
        public virtual DbSet<Tbl_Kategoriler> Tbl_Kategoriler { get; set; }
        public virtual DbSet<Tbl_Kitaplar> Tbl_Kitaplar { get; set; }
        public virtual DbSet<Tbl_Personeller> Tbl_Personeller { get; set; }
        public virtual DbSet<Tbl_Uyeler> Tbl_Uyeler { get; set; }
        public virtual DbSet<Tbl_Yazarlar> Tbl_Yazarlar { get; set; }
        public virtual DbSet<Tbl_Hakkımızda> Tbl_Hakkımızda { get; set; }
        public virtual DbSet<Tbl_Iletisim> Tbl_Iletisim { get; set; }
        public virtual DbSet<Tbl_Egitimler> Tbl_Egitimler { get; set; }
        public virtual DbSet<Tbl_Mesajlar> Tbl_Mesajlar { get; set; }
        public virtual DbSet<Tbl_Duyurular> Tbl_Duyurular { get; set; }
        public virtual DbSet<Tbl_Admin> Tbl_Admin { get; set; }
    
        public virtual ObjectResult<string> EnFazlaKitapYazar()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("EnFazlaKitapYazar");
        }
    
        public virtual int UpdateTamAd()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateTamAd");
        }
    }
}
