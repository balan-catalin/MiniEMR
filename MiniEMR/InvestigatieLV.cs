using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEMR
{
    public class InvestigatieLV
    {
        public String CodInvestigatie { set; get; }
        public String DenumireInvestigatie { set; get; }
        public bool Selectat { set; get; }
        public double CostAditional { set; get; }

        public InvestigatieLV()
        {

        }

        public bool Equals(InvestigatieLV tratament)
        {
            return this.CodInvestigatie == tratament.CodInvestigatie && this.Selectat == tratament.Selectat;
        }
    }
}
