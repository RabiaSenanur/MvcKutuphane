//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Tbl_Cezalar
    {
        public int ID { get; set; }
        public Nullable<int> Uye { get; set; }
        public Nullable<int> Hareket { get; set; }
        public Nullable<System.DateTime> Baslangıc { get; set; }
        public Nullable<System.DateTime> Bitis { get; set; }
        public Nullable<decimal> Para { get; set; }
    
        public virtual Tbl_Hareket Tbl_Hareket { get; set; }
        public virtual Tbl_Uyeler Tbl_Uyeler { get; set; }
    }
}
