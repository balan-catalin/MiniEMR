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
    
    public partial class Observatie
    {
        public int IdObservatie { get; set; }
        public string TextObservatie { get; set; }
        public int IdCaz { get; set; }
        public System.DateTime DataObservatie { get; set; }
    
        public virtual Caz Caz { get; set; }
    }
}