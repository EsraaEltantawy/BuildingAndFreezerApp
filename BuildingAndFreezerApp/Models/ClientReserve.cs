//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BuildingAndFreezerApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientReserve
    {
        public int id { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> BuildID { get; set; }
        public Nullable<int> FiatNum { get; set; }
        public Nullable<decimal> Prices { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Building Building { get; set; }
        public virtual Reserve Reserve { get; set; }
    }
}
