namespace Tax.Simulator;

public static class Simulateur
{
    private static readonly decimal[] TranchesImposition = { 10225m, 26070m, 74545m, 160336m, 500000m }; // Plafonds des tranches
    private static readonly decimal[] TauxImposition = {0.0m, 0.11m, 0.30m, 0.41m, 0.45m, 0.48m}; // Taux correspondants

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
        decimal impot = 0;
        var tranches = TranchesImposition
            .Select((tranche, i) => new
            {
                Plafond = tranche,
                Base = i > 0 ? TranchesImposition[i - 1] : 0,
                Taux = TauxImposition[i]
            })
            .Where(x => revenuImposableParPart > x.Base);

        impot = tranches
            .Select(x => (Math.Min(revenuImposableParPart, x.Plafond) - x.Base) * x.Taux)
            .Sum();

        if (revenuImposableParPart > TranchesImposition[^1])
        {
            impot += (revenuImposableParPart - TranchesImposition[^1]) * TauxImposition[^1];
        }

        return impot;
        
    }
}