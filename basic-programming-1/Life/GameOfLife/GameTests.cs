using NUnit.Framework;

namespace GameOfLife
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void SomeTest()
        {
            var gameField = new [,]
            {
                {false, false, false},
                {false, true, false},
                {false, false, false},
            };

            var expected = new[,]
            {
                {false, false, false},
                {false, true, false},
                {false, false, false},
            };
            
            //---
            //-+-
            //---
            
            //---
            //---
            //---

            var actual = Game.NextStep(gameField);
            
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void FindNeighbours()
        {
            var gameField = new [,]
            {
                {false, false, false},
                {false, true, false},
                {false, false, false},
            };

            var expected = new[] {false, false, false, false, false, false, false, false};

            var actual = Game.FindNeighbours(gameField,1,1);
            
            CollectionAssert.AreEquivalent(expected, actual);
        }
        
        [Test]
        public void FindNeighbours2()
        {
            var gameField = new [,]
            {
                {false, false, false},
                {false, true, false},
                {false, false, false},
            };

            var expected = new[] {false, false, true};

            var actual = Game.FindNeighbours(gameField,0,0);
            
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}