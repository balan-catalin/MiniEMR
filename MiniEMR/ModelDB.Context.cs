﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DataBaseEMREntities : DbContext
    {
        public DataBaseEMREntities()
            : base("name=DataBaseEMREntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Alergie> Alergies { get; set; }
        public virtual DbSet<Caz> Cazs { get; set; }
        public virtual DbSet<Diagnostic> Diagnostics { get; set; }
        public virtual DbSet<FisaPacient> FisaPacients { get; set; }
        public virtual DbSet<Investigatie> Investigaties { get; set; }
        public virtual DbSet<ListaAlergie> ListaAlergies { get; set; }
        public virtual DbSet<ListaServiciiMedicale> ListaServiciiMedicales { get; set; }
        public virtual DbSet<Observatie> Observaties { get; set; }
        public virtual DbSet<Pacient> Pacients { get; set; }
        public virtual DbSet<PersonalMedical> PersonalMedicals { get; set; }
        public virtual DbSet<ServiciuMedical> ServiciuMedicals { get; set; }
        public virtual DbSet<Spital> Spitals { get; set; }
        public virtual DbSet<ListaDiagnostice> ListaDiagnostices { get; set; }
        public virtual DbSet<ListaInvestigatii> ListaInvestigatiis { get; set; }
        public virtual DbSet<ListaObservatii> ListaObservatiis { get; set; }
        public virtual DbSet<ListaCazuri> ListaCazuris { get; set; }
        public virtual DbSet<ListaPacienti> ListaPacientis { get; set; }
        public virtual DbSet<RaportCA> RaportCAS { get; set; }
    }
}
