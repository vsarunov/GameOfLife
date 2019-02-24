namespace GameOfLifeCalculationLibraryTests
{
    using GameOfLifeCalculationLibrary;
    using GameOfLifeCalculationLibrary.Models;
    using Shouldly;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Xunit;

    public class GameOfLifeCalculatorTests
    {
        private readonly GameOfLifeCalculator _classUnderTest;
        public GameOfLifeCalculatorTests()
        {
            _classUnderTest = new GameOfLifeCalculator();
        }

        [Fact]
        public void GetNewGeneration_OldGeneration_Null_Expected_Empty()
        {
            var result = _classUnderTest.GetNewGeneration(10, 10, null);

            result.ShouldBeEmpty();
        }

        [Fact]
        public void GetNewGeneration_OldGeneration_Empty_Expected_Empty()
        {
            var result = _classUnderTest.GetNewGeneration(10, 10, Enumerable.Empty<GridCell>());

            result.ShouldBeEmpty();
        }

        [Fact]
        public void GetNewGeneration_OneLiveCell_Expected_Empty()
        {
            var mockedCells = new List<GridCell>()
            {
                new GridCell(){XAxysCoordinates=5,YAxisCoordinates=5}
            };

            var result = _classUnderTest.GetNewGeneration(10, 10, mockedCells);

            result.ShouldBeEmpty();
        }

        [Fact]
        public void GetNewGeneration_TwoLiveCell_Expected_Empty()
        {
            var mockedCells = new List<GridCell>()
            {
                new GridCell(){XAxysCoordinates=5,YAxisCoordinates=5},
                new GridCell(){XAxysCoordinates=5,YAxisCoordinates=4},
            };

            var result = _classUnderTest.GetNewGeneration(10, 10, mockedCells);

            result.ShouldBeEmpty();
        }

        [Fact]
        public void GetNewGeneration_ThreeLiveCell_Expected_ThreeCells()
        {
            var mockedCells = new List<GridCell>()
            {
                new GridCell(){XAxysCoordinates=5,YAxisCoordinates=5},
                new GridCell(){XAxysCoordinates=4,YAxisCoordinates=5},
                new GridCell(){XAxysCoordinates=3,YAxisCoordinates=5},
            };

            var result = _classUnderTest.GetNewGeneration(10, 10, mockedCells);

            result.ShouldNotBeEmpty();
            result.ShouldContain(x => x.XAxysCoordinates == 4 && x.YAxisCoordinates == 4);
            result.ShouldContain(x => x.XAxysCoordinates == 4 && x.YAxisCoordinates == 5);
            result.ShouldContain(x => x.XAxysCoordinates == 4 && x.YAxisCoordinates == 6);
        }

        [Fact]
        public void GetNewGeneration_FourLiveCell_Expected_InASquare_Expected_UnChanged()
        {
            var mockedCells = new List<GridCell>()
            {
                new GridCell(){XAxysCoordinates=5,YAxisCoordinates=5},
                new GridCell(){XAxysCoordinates=5,YAxisCoordinates=6},
                new GridCell(){XAxysCoordinates=4,YAxisCoordinates=5},
                new GridCell(){XAxysCoordinates=4,YAxisCoordinates=6},
            };

            var result = _classUnderTest.GetNewGeneration(10, 10, mockedCells);

            result.ShouldNotBeEmpty();
            result.ShouldContain(x => x.XAxysCoordinates == 5 && x.YAxisCoordinates == 5);
            result.ShouldContain(x => x.XAxysCoordinates == 5 && x.YAxisCoordinates == 6);
            result.ShouldContain(x => x.XAxysCoordinates == 4 && x.YAxisCoordinates == 5);
            result.ShouldContain(x => x.XAxysCoordinates == 4 && x.YAxisCoordinates == 6);
        }

        [Fact]
        public void GetNewGeneration_ThreeLiveCell_GridTo_Small_Expected_One_Cell()
        {
            var mockedCells = new List<GridCell>()
            {
                new GridCell(){XAxysCoordinates=5,YAxisCoordinates=5},
                new GridCell(){XAxysCoordinates=4,YAxisCoordinates=5},
                new GridCell(){XAxysCoordinates=3,YAxisCoordinates=5},
            };

            var result = _classUnderTest.GetNewGeneration(5, 4, mockedCells);

            result.ShouldNotBeEmpty();
            result.ShouldContain(x => x.XAxysCoordinates == 4 && x.YAxisCoordinates == 5);
        }

        [Fact]
        public void GetNewGeneration_Cell_Has_Four_Neighbours_Expected_Cell_Dead()
        {
            var mockedCells = new List<GridCell>()
            {
                new GridCell(){XAxysCoordinates=5,YAxisCoordinates=5},
                new GridCell(){XAxysCoordinates=5,YAxisCoordinates=4},
                new GridCell(){XAxysCoordinates=5,YAxisCoordinates=3},
                new GridCell(){XAxysCoordinates=6,YAxisCoordinates=4},//Cell with four neighbours
                new GridCell(){XAxysCoordinates=7,YAxisCoordinates=4},
            };

            var result = _classUnderTest.GetNewGeneration(10, 10, mockedCells);

            result.ShouldNotBeEmpty();
            result.ShouldNotContain(x => x.XAxysCoordinates == 6 && x.YAxisCoordinates == 4);
        }
    }
}
