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
    
    public partial class Diagnostic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Diagnostic()
        {
            this.ListaDiagnostices = new HashSet<ListaDiagnostice>();
        }
    
        public string CodDiagnostic { get; set; }
        public string NumeDiagnostic { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListaDiagnostice> ListaDiagnostices { get; set; }
    }
}
