namespace Tax.Simulator;

public static class Simulateur
{
    private static readonly List<TrancheFiscale> Imposition =
    [
        new(10225m, 0.0m),
        new(26070m, 0.11m),
        new(74545m, 0.30m),
        new(160336m, 0.41m),
        new(500000m, 0.45m),
        new(decimal.MaxValue, 0.48m)
    ];

    public static decimal CalculerImpotsAnnuel(Situation situation)
    {
        decimal revenuAnnuel = revenueAnnuel(situation);
        

        var baseQuotient = situation.SituationFamiliale == SituationFamiliale.MariéPacsé ? 2 : 1;
        decimal quotientEnfants = quotientEnfant(situation.NombreEnfant);

        var partsFiscales = baseQuotient + quotientEnfants;
        var revenuImposableParPart = revenuAnnuel / partsFiscales;

        decimal impot = Simulateur.impotParPart(revenuImposableParPart);


        return Math.Round(impot * partsFiscales, 2);
    }

    private static decimal revenueAnnuel(Situation situation)
    {
        decimal revenuAnnuel;
        if (situation.SituationFamiliale == SituationFamiliale.MariéPacsé)
        {
            revenuAnnuel = (situation.SalaireMensuel + situation.SalaireMensuelConjoint) * 12;
        }
        else
        {
            revenuAnnuel = situation.SalaireMensuel * 12;
        }
        return revenuAnnuel;
    }

    private static decimal quotientEnfant(int nombreEnfants)
    {
        decimal quotientEnfants = (decimal) Math.PI;

        switch (nombreEnfants)
        {
            case 0:
                quotientEnfants = 0;
                break;
            case 1:
                quotientEnfants = 0.5m;
                break;
            case 2:
                quotientEnfants = 1.0m;
                break;
            default:
                quotientEnfants = 1.0m + (nombreEnfants - 2) * 0.5m;
                break;
        }
        return quotientEnfants;
    }

    private static decimal impotParPart(decimal revenuImposableParPart)
    {
        return Imposition
            .Select((tranche, index) => new
            {
                tranche.Plafond,
                Base = index > 0 ? Imposition[index - 1].Plafond : 0,
                tranche.Taux
            })
            .Where(x => revenuImposableParPart > x.Base)
            .Sum(x => (Math.Min(revenuImposableParPart, x.Plafond) - x.Base) * x.Taux);
    }

}