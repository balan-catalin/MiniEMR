using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEMR
{
    public class DiagnosticLV
    {
        public String CodDiagnostic { set; get; }
        public String DenumireDiagnosctic { set; get; }
        public bool Selectat { set; get; }

        public DiagnosticLV()
        {

        }

        public bool Equals(DiagnosticLV diag)
        {
            if (
                this.CodDiagnostic == diag.CodDiagnostic &&
                this.DenumireDiagnosctic == diag.DenumireDiagnosctic &&
                this.Selectat == diag.Selectat
              )
                return true;
            else
                return false;
        }
    }
}
