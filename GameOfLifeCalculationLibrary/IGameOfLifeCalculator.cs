namespace GameOfLifeCalculationLibrary
{
    using GameOfLifeCalculationLibrary.Models;
    using System.Collections.Generic;

    public interface IGameOfLifeCalculator
    {
        IEnumerable<GridCell> GetNewGeneration(int x, int y, IEnumerable<GridCell> oldGeneration);
    }
}
