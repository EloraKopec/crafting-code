using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tax.Simulator
{
    public class TrancheFiscale
    {
        private readonly decimal _plafond;
        private readonly decimal _taux;

        public TrancheFiscale(decimal plafond, decimal taux)
        {
            _plafond = plafond;
            _taux = taux;
        }

        public decimal Plafond => _plafond;
        public decimal Taux => _taux;
    }

}
