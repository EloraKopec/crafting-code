using FluentAssertions;
using Xunit;

namespace Tax.Simulator.Tests;
public class SimulateurShould
{
    [Fact]
    public void ImpotCelibataire()
    {
        Simulateur.CalculerImpotsAnnuel("Célibataire", 2000, 0, 0)
            .Should()
            .Be(1515.25m);
        
        Simulateur.CalculerImpotsAnnuel("Célibataire", 1978123.98m, 0, 0)
            .Should()
            .Be(10661178.05m);
    }
}