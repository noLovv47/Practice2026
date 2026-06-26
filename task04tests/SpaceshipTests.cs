using Xunit;
using task04;

namespace task04tests
{
    public class SpaceshipTests
    {
        [Fact]
        public void Cruiser_ShouldHaveCorrectStats()
        {
            ISpaceship cruiser = new Cruiser();
            Assert.Equal(50, cruiser.Speed);
            Assert.Equal(100, cruiser.FirePower);
        }

        [Fact]
        public void Fighter_ShouldHaveCorrectStats()
        {
            ISpaceship fighter = new Fighter();
            Assert.Equal(100, fighter.Speed);
            Assert.Equal(50, fighter.FirePower);
        }

        [Fact]
        public void Fighter_ShouldBeFasterThanCruiser()
        {
            var fighter = new Fighter();
            var cruiser = new Cruiser();
            Assert.True(fighter.Speed > cruiser.Speed);
        }

        [Fact]
        public void Cruiser_ShouldHaveMoreFirePowerThanFighter()
        {
            var cruiser = new Cruiser();
            var fighter = new Fighter();
            Assert.True(cruiser.FirePower > fighter.FirePower);
        }

        [Fact]
        public void MoveForward_ShouldNotThrowException()
        {
            ISpaceship cruiser = new Cruiser();
            ISpaceship fighter = new Fighter();

            var exception = Record.Exception(() =>
            {
                cruiser.MoveForward();
                fighter.MoveForward();
            });

            Assert.Null(exception);
        }

        [Fact]
        public void Rotate_ShouldNotThrowException()
        {
            ISpaceship cruiser = new Cruiser();
            var exception = Record.Exception(() => cruiser.Rotate(45));
            Assert.Null(exception);
        }

        [Fact]
        public void Fire_ShouldNotThrowException()
        {
            ISpaceship fighter = new Fighter();
            var exception = Record.Exception(() => fighter.Fire());
            Assert.Null(exception);
        }

        [Fact]
        public void Polymorphism_ShouldWork()
        {
            ISpaceship[] ships = new ISpaceship[]
            {
                new Cruiser(),
                new Fighter()
            };

            Assert.Equal(50, ships[0].Speed);
            Assert.Equal(100, ships[1].Speed);
        }
    }
}