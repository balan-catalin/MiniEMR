using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEMR
{
    public class RaportTrimis
    {
            public int Id { get; set; }
            public string CodSpital { get; set; }
            public string CNP { get; set; }
            public string NumarCaz { get; set; }
            public DateTime? DataInchidereCaz { get; set; }
            public string CodDiagnosticPrincipal { get; set; }
            public string CodInvestigatie { get; set; }
            public double? CostAditionalInvestigatie { get; set; }
            public string CodServiciuMedical { get; set; }
            public double? CostAditionalServiciuMedical { get; set; }
    }
}
