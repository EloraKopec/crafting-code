namespace Tax.Simulator;

public static class Simulateur
{
    private static readonly decimal[] TranchesImposition = {10225m, 26070m, 74545m, 160336m}; // Plafonds des tranches
    private static readonly decimal[] TauxImposition = {0.0m, 0.11m, 0.30m, 0.41m, 0.45m}; // Taux correspondants

    public static decimal CalculerImpotsAnnuel(
        string situationFamiliale,
        decimal salaireMensuel,
        decimal salaireMensuelConjoint,
        int nombreEnfants)
    {
        if (situationFamiliale != "Célibataire" && situationFamiliale != "Marié/Pacsé")
        {
            throw new ArgumentException("Situation familiale invalide.");
        }

        if (salaireMensuel <= 0)
        {
            throw new ArgumentException("Les salaires doivent être positifs.");
        }

        if (situationFamiliale == "Marié/Pacsé" && salaireMensuelConjoint < 0)
        {
            throw new InvalidDataException("Les salaires doivent être positifs.");
        }

        if (nombreEnfants < 0)
        {
            throw new ArgumentException("Le nombre d'enfants ne peut pas être négatif.");
        }

        decimal revenuAnnuel;
        if (situationFamiliale == "Marié/Pacsé")
        {
            revenuAnnuel = (salaireMensuel + salaireMensuelConjoint) * 12;
        }
        else
        {
            revenuAnnuel = salaireMensuel * 12;
        }

        var baseQuotient = situationFamiliale == "Marié/Pacsé" ? 2 : 1;
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

        var partsFiscales = baseQuotient + quotientEnfants;
        var revenuImposableParPart = revenuAnnuel / partsFiscales;

        decimal impot = 0;
        for (var i = 0; i < TranchesImposition.Length; i++)
        {
            if (revenuImposableParPart <= TranchesImposition[i])
            {
                impot += (revenuImposableParPart - (i > 0 ? TranchesImposition[i - 1] : 0)) * TauxImposition[i];
                break;
            }
            else
            {
                impot += (TranchesImposition[i] - (i > 0 ? TranchesImposition[i - 1] : 0)) * TauxImposition[i];
            }
        }

        if (revenuImposableParPart > TranchesImposition[^1])
        {
            impot += (revenuImposableParPart - TranchesImposition[^1]) * TauxImposition[^1];
        }

        var impotParPart = impot;

        return Math.Round(impotParPart * partsFiscales, 2);
        
    }

    public static decimal calculerImpotAnnuelleBis(string situationFamiliale, decimal salaireMensuel, decimal salaireMensuelConjoint, int nombreEnfants)
    {
        if (situationFamiliale != "Célibataire" && situationFamiliale != "Marié/Pacsé")
        {
            throw new ArgumentException("Situation familiale invalide.");
        }

        if (salaireMensuel <= 0)
        {
            throw new ArgumentException("Les salaires doivent être positifs.");
        }

        if (situationFamiliale == "Marié/Pacsé" && salaireMensuelConjoint < 0)
        {
            throw new InvalidDataException("Les salaires doivent être positifs.");
        }

        if (nombreEnfants < 0)
        {
            throw new ArgumentException("Le nombre d'enfants ne peut pas être négatif.");
        }

        decimal revenuAnnuel = revenueAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjoint);
        

        var baseQuotient = situationFamiliale == "Marié/Pacsé" ? 2 : 1;
        decimal quotientEnfants = quotientEnfant(nombreEnfants);

        var partsFiscales = baseQuotient + quotientEnfants;
        var revenuImposableParPart = revenuAnnuel / partsFiscales;

        decimal impot = 0;
        for (var i = 0; i < TranchesImposition.Length; i++)
        {
            if (revenuImposableParPart <= TranchesImposition[i])
            {
                impot += (revenuImposableParPart - (i > 0 ? TranchesImposition[i - 1] : 0)) * TauxImposition[i];
                break;
            }
            else
            {
                impot += (TranchesImposition[i] - (i > 0 ? TranchesImposition[i - 1] : 0)) * TauxImposition[i];
            }
        }

        if (revenuImposableParPart > TranchesImposition[^1])
        {
            impot += (revenuImposableParPart - TranchesImposition[^1]) * TauxImposition[^1];
        }

        var impotParPart = impot;

        return Math.Round(impotParPart * partsFiscales, 2);
    }

    private static decimal revenueAnnuel(string situationFamiliale, decimal salaireMensuel, decimal salaireMensuelConjoint)
    {
        decimal revenuAnnuel;
        if (situationFamiliale == "Marié/Pacsé")
        {
            revenuAnnuel = (salaireMensuel + salaireMensuelConjoint) * 12;
        }
        else
        {
            revenuAnnuel = salaireMensuel * 12;
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
        
        impot = TranchesImposition.Sum()
    }
}