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
    
    public partial class Employess
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employess()
        {
            this.BuildingEmployees = new HashSet<BuildingEmployee>();
        }
    
        public int id { get; set; }
        public string empName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BuildingEmployee> BuildingEmployees { get; set; }
    }
}
