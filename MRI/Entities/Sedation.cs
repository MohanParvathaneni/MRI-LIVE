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
    
    public partial class Sedation
    {
        public Sedation()
        {
            this.ScanResults = new HashSet<ScanResult>();
        }
    
        public int SedationId { get; set; }
        public string SedationCode { get; set; }
        public string SedationDescription { get; set; }
    
        public virtual ICollection<ScanResult> ScanResults { get; set; }
    }
}
