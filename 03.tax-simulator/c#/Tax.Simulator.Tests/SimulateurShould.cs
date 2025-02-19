using FluentAssertions;
using Xunit;

namespace Tax.Simulator.Tests;
public class SimulateurShould
{
    [Fact]
    public void ImpotCelibataireShould()
    {
        Simulateur.CalculerImpotsAnnuel("CÚlibataire", 2000, 0, 0)
            .Should()
            .Be(1515.25m);
        
        Simulateur.CalculerImpotsAnnuel("CÚlibataire", 1978123.98m, 0, 0)
            .Should()
            .Be(10661178.05m);
    }

    [Fact]
    public void ImpotCelibataireThrow()
    {
        Action act = () =>
          Simulateur.CalculerImpotsAnnuel("CÚlibataire", -2000, 0, 0);
        act.Should().ThrowExactly<ArgumentException>();
    }
}