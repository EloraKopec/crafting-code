using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tax.Simulator
{
    public enum SituationFamiliale
    {
        MariéPacsé,
        Célibataire
    }

    public static class SituationFamilialeExtensions
    {
        public static SituationFamiliale ToSituationFamiliale(this string situationFamiliale)
        {
            return (situationFamiliale?.Trim()?.ToLower()) switch
            {
                "marié/pacsé" => SituationFamiliale.MariéPacsé,
                "célibataire" => SituationFamiliale.Célibataire,
                _ => throw new ArgumentException("Situation familiale invalide."),
            };
        }
    }
}
