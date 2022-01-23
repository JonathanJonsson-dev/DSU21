using NUnit.Framework;
using DSU21.Helpers;
using System.Drawing;
using static DSU21.Helpers.Ship;

namespace DSU21.Tests
{
    [TestFixture] // Attribut som säger att detta är ett test
    public class BattleShipAttackTests
    {
        private Ship _ship;
        [SetUp]
        public void Setup()
        {
            // Arrange
            Point point = new Point(3, 3);

            _ship = new Ship(point, Direction.Horizontal);
        }

        [TestCase(3, 3, ExpectedResult = Result.Hit)]
        [TestCase(3, 4, ExpectedResult = Result.Hit)]
        [TestCase(3, 5, ExpectedResult = Result.Hit)]
        [TestCase(3, 6, ExpectedResult = Result.Sunk)]
        public Result ShipUnderAttack_ShouldReturnHitAndSunk(int x, int y)
        {
            // Act
            var point = new Point(x, y);

            // Assert
            return _ship.UnderAttack(point);
        }
    }
}