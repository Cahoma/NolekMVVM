//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NolekWPF.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Component
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Component()
        {
            this.EquipmentComponents = new HashSet<EquipmentComponent>();
        }
    
        public int ComponentId { get; set; }
        public string ComponentType { get; set; }
        public string ComponentDescription { get; set; }
        public string ComponentOrderNumber { get; set; }
        public string ComponentSerialNumber { get; set; }
        public string ComponentSupplyNumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EquipmentComponent> EquipmentComponents { get; set; }
    }
}
