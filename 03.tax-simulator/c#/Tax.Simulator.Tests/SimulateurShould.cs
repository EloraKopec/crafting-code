using FluentAssertions;
using Xunit;

namespace Tax.Simulator.Tests;
public class SimulateurShould
{
    [Fact]
    public void ImpotCelibataireShould()
    {
        Simulateur.CalculerImpotsAnnuel("Célibataire", 2000, 0, 0)
            .Should()
            .Be(1515.25m);
        
        Simulateur.CalculerImpotsAnnuel("Célibataire", 1978123.98m, 0, 0)
            .Should()
            .Be(10661178.05m);
    }


    [Fact]
    public void ImpotCelibataireThrow()
    {
        Action act = () =>
          Simulateur.CalculerImpotsAnnuel("Célibataire", -2000, 0, 0);
        act.Should().ThrowExactly<ArgumentException>();

    }

    [Fact (DisplayName = "Marié/Pacsé 3 enfants")]
    public void ImpotMariePacse3Enfants()
    {
        Simulateur.CalculerImpotsAnnuel("Marié/Pacsé", 3000, 3000, 3)
            .Should()
            .Be(3983.37m);
        
        Simulateur.CalculerImpotsAnnuel("Marié/Pacsé", 3000, 3000, 3)
            .Should()
            .NotBe(3982.37m);
        
    }
    
    [Fact (DisplayName = "Marié/Pacsé -1 enfant")]
    public void ImpotMariePacseMoins1Enfant()
    {
        Action action = () => 
        Simulateur.CalculerImpotsAnnuel("Marié/Pacsé", 3000, 3000, -1);
            action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("Le nombre d'enfants ne peut pas être négatif.");
    }
    
    [Fact (DisplayName = "Situation familial invalide")]
    public void ImpotSituationFamilialeInvalide()
    {
        Action action = () => 
        Simulateur.CalculerImpotsAnnuel("Divorcé", 3000, 3000, 3);
            action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("Situation familiale invalide.");
    } 
    
    [Fact (DisplayName = "Les salaires doivent être positifs")]
    public void ImpotSalaireNegatif()
    {
        Action action = () => 
        Simulateur.CalculerImpotsAnnuel("Marié/Pacsé", -3000, 3000, 3);
            action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("Les salaires doivent être positifs.");
    }
    
    
}