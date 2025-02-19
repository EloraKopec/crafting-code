namespace Tax.Simulator;

public static class Simulateur
{
    private static readonly decimal[] TranchesImposition = {10225m, 26070m, 74545m, 160336m}; // Plafonds des tranches
    private static readonly decimal[] TauxImposition = {0.0m, 0.11m, 0.30m, 0.41m, 0.45m}; // Taux correspondants

    private static decimal CalculImpotInferieurA500K(Situation situation, decimal revenueAnnuelInferieur500k)
    {
        var baseQuotient = situation.SituationFamiliale == SituationFamiliale.MariéPacsé ? 2 : 1;
        decimal quotientEnfants = quotientEnfant(situation.NombreEnfant);

        var partsFiscales = baseQuotient + quotientEnfants;
        var revenuImposableParPart = revenueAnnuelInferieur500k / partsFiscales;

        decimal impot = Simulateur.impotParPart(revenuImposableParPart);
        

        return impot * partsFiscales;
    }

    public static decimal CalculerImpotsAnnuel(Situation situation)
    {
        decimal revenuAnnuel = revenueAnnuel(situation);

        if (revenuAnnuel <= 500000)
        {
            return CalculImpotInferieurA500K(situation, revenuAnnuel);
        }
        decimal revenueSup500k = revenuAnnuel - 500000m;

        decimal impotInf500k = CalculImpotInferieurA500K(situation,500000);

        decimal impotSup500k = revenueSup500k * 0.48m;

        return Math.Round(impotInf500k + impotSup500k, 2);
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

        if (nombreEnfants == 0)
        {
            quotientEnfants = 0;
        }
        else if (nombreEnfants == 1)
        {
            quotientEnfants = 0.5m;
        }
        else if (nombreEnfants == 2)
        {
            quotientEnfants = 1.0m;
        }
        else
        {
            quotientEnfants = 1.0m + (nombreEnfants - 2) * 0.5m;
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

        var impotParPart = impot;
        return impotParPart;
        
    }
}