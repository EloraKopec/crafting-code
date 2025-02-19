using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Tax.Simulator
{
    public class Situation
    {
        private readonly SituationFamiliale _situationFamiliale;
        private readonly decimal _salaireMensuel;
        private readonly decimal _salaureMensuelConjoint;
        private readonly int _nombreEnfant;

        public Situation(string situationFamiliale, decimal salaireMensuel, decimal salaureMensuelConjoint, int nombreEnfant)
        {
            _situationFamiliale = situationFamiliale.ToSituationFamiliale();
            _salaireMensuel = salaireMensuel;
            _salaureMensuelConjoint = salaureMensuelConjoint;
            _nombreEnfant = nombreEnfant;
        }
    }
}
