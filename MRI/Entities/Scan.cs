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
    
    public partial class Scan
    {
        public Scan()
        {
            this.ScanResults = new HashSet<ScanResult>();
        }
    
        public int ScanId { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public System.Guid CreatedByGUID { get; set; }
        public string CreatedByDisplayName { get; set; }
        public string CreatedByUserId { get; set; }
        public int SiteId { get; set; }
        public System.DateTime DateOfScan { get; set; }
        public string AgeOfPatient { get; set; }
        public int AgeCodeId { get; set; }
        public int SexId { get; set; }
        public string ZipCode { get; set; }
        public int CountyId { get; set; }
        public int PatientStatusId { get; set; }
        public int ClinicalStatusId { get; set; }
        public int PhysicianSiteId { get; set; }
        public int SourceOfPaymentId { get; set; }
        public string PatientName { get; set; }
        public string AccountNumber { get; set; }
        public string MedicalRecordNumber { get; set; }
        public string ScanDescription { get; set; }
        public bool ScanUpdated { get; set; }
    
        public virtual AgeCode AgeCode { get; set; }
        public virtual ClinicalStatu ClinicalStatu { get; set; }
        public virtual County County { get; set; }
        public virtual PatientStatu PatientStatu { get; set; }
        public virtual PhysicianSite PhysicianSite { get; set; }
        public virtual Sex Sex { get; set; }
        public virtual Site Site { get; set; }
        public virtual SourceOfPayment SourceOfPayment { get; set; }
        public virtual ICollection<ScanResult> ScanResults { get; set; }
    }
}
