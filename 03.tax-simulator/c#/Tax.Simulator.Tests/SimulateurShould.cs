using FluentAssertions;
using Xunit;

namespace Tax.Simulator.Tests;
public class SimulateurShould
{
    [Fact]
    public void ImpotCelibataire()
    {
        Simulateur.CalculerImpotsAnnuel("C�libataire", 2000, 0, 0)
            .Should()
            .Be(1515.25m);
    }
}