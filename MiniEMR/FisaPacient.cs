//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MiniEMR
{
    using System;
    using System.Collections.Generic;
    
    public partial class FisaPacient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FisaPacient()
        {
            this.Cazs = new HashSet<Caz>();
            this.Alergies = new HashSet<Alergie>();
        }
    
        public int IdFisa { get; set; }
        public string NumarFisa { get; set; }
        public int IdPacient { get; set; }
        public System.DateTime DataDeschidereFisa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Caz> Cazs { get; set; }
        public virtual Pacient Pacient { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alergie> Alergies { get; set; }
    }
}