//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MRI.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class PhysicianSpecialty
    {
        public PhysicianSpecialty()
        {
            this.Physicians = new HashSet<Physician>();
        }
    
        public int PhysicianSpecialtyId { get; set; }
        public string PhysicianSpecialtyCode { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public int StateId { get; set; }
    
        public virtual ICollection<Physician> Physicians { get; set; }
    }
}
