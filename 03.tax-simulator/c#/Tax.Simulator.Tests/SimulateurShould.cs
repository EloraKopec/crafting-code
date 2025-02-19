using FluentAssertions;
using System.Security.Cryptography;
using Xunit;

namespace Tax.Simulator.Tests;
public class SimulateurShould
{
    [Fact]
    public void ImpotCelibataireShould()
    {
        Situation situation = new("Célibataire", 2000, 0, 0);
        Simulateur.CalculerImpotsAnnuel(situation)
            .Should()
            .Be(1515.25m);

        Situation situation2 = new Situation("Célibataire", 1978123.98m, 0, 0);
        Simulateur.CalculerImpotsAnnuel(situation2)
            .Should()
            .Be(11358302.68m);
    }


    [Fact]
    public void ImpotCelibataireThrow()
    {

        Action action = () => new Situation("Célibataire", -2000, 0, 0);

        action.Should()
            .ThrowExactly<ArgumentException>();

    }

    [Fact(DisplayName = "Marié/Pacsé 3 enfants")]
    public void ImpotMariePacse3Enfants()
    {
        Situation situation = new("Marié/Pacsé", 3000, 3000, 3);
        Simulateur.CalculerImpotsAnnuel(situation)
            .Should()
            .Be(3983.37m);

        Simulateur.CalculerImpotsAnnuel(situation)
            .Should()
            .NotBe(3982.37m);
    }

    [Fact(DisplayName = "Marié/Pacsé -1 enfant")]
    public void ImpotMariePacseMoins1Enfant()
    {
        Action action = () => new Situation("Marié/Pacsé", 3000, 3000, -1);

        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("Le nombre d'enfants ne peut pas être négatif.");
    }


    [Fact]
    public void ImpotMarieShould()
    {
        Situation situation = new("Marié/Pacsé", 2000, 2500, 0);
        Simulateur.CalculerImpotsAnnuel(situation)
            .Should()
            .Be(4043.90m);
    }

    [Fact]
    public void ImpotMarieThrow()
    {

        Action action = () => new Situation("Marié/Pacsé", -2500, 2000, 0);

        action.Should()
            .ThrowExactly<ArgumentException>();
    }

    [Fact(DisplayName = "Situation familial invalide")]
    public void ImpotSituationFamilialeInvalide()
    {

        Action action = () => new Situation("Divorcé", 3000, 3000, 3);

        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("Situation familiale invalide.");


    }

    [Fact(DisplayName = "Les salaires doivent être positifs")]
    public void ImpotSalaireNegatif()
    {
        Action action = () => new Situation("Marié/Pacsé", -3000, 3000, 3);

        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("Les salaires doivent être positifs.");

        Action action2 = () => new Situation("Marié/Pacsé", 3000, -3000, 3);

        action2.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("Les salaires doivent être positifs.");

        Action action3 = () => new Situation("Marié/Pacsé", 0, 3000, 3);

        action3.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("Les salaires doivent être positifs.");
    }

    [Fact(DisplayName = "Salaire 2 millions")]
    public void ImpotSalaire2Millions()
    {
        Situation situation = new("Marié/Pacsé", 2000000, 10000, 3);
        Simulateur.CalculerImpotsAnnuel(situation)
            .Should()
            .Be(11452679.96m);
    }

    [Fact]
    public void ImpotSuperieur500k()
    {
        Situation situation = new("Célibataire", 45000, 0, 0);
        Simulateur.CalculerImpotsAnnuel(situation)
            .Should()
            .Be(223508.56m);

    }
    [Fact]
    public void ImpotSuperieur500kMarie()
    {
        Situation situation = new("Marié/Pacsé", 25000, 30000, 2);
        Simulateur.CalculerImpotsAnnuel(situation)
            .Should()
            .Be(234925.68m);
    }

}

