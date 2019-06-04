using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEMR
{
    public class ServiciuLV
    {
        public String CodServiciu { set; get; }
        public String NumeServiciu { set; get; }
        public bool Selectat { set; get; }
        public double CostAditional { set; get; }

        public ServiciuLV()
        {

        }

        public bool Eqals(ServiciuLV serv)
        {
            return (this.CodServiciu == serv.CodServiciu && this.Selectat == serv.Selectat);
        }
    }
}
