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
    
    public partial class County
    {
        public County()
        {
            this.CountyZipCodes = new HashSet<CountyZipCode>();
            this.Scans = new HashSet<Scan>();
        }
    
        public int CountyId { get; set; }
        public int StateId { get; set; }
        public string CountyCode { get; set; }
        public string CountyName { get; set; }
        public bool ExcludeZipCode { get; set; }
    
        public virtual State State { get; set; }
        public virtual ICollection<CountyZipCode> CountyZipCodes { get; set; }
        public virtual ICollection<Scan> Scans { get; set; }
    }
}
