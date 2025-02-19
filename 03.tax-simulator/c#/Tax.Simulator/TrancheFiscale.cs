using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tax.Simulator
{
    public class TrancheFiscale
    {
        public decimal Plafond { get; set; }
        public decimal Taux { get; set; }

        public TrancheFiscale(decimal plafond, decimal taux)
        {
            Plafond = plafond;
            Taux = taux;
        }
    }

}
