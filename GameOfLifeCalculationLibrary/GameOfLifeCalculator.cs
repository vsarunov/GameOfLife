namespace GameOfLifeCalculationLibrary
{
    using GameOfLifeCalculationLibrary.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class to calculate which cells will stay or come alive in the next generation
    /// based on the current grid.
    /// </summary>
    public class GameOfLifeCalculator : IGameOfLifeCalculator
    {
        // This are the values plus/minus to the grid cell coordinates to see if they have neighbours there
        private readonly List<Point> _neighbourPoints = new List<Point> {
            new Point(){x=-1,y=0},
            new Point(){x=-1,y=-1},
            new Point(){x=-1,y=1},
            new Point(){x=0,y=-1},
            new Point(){x=0,y=1},
            new Point(){x=1,y=-1},
            new Point(){x=1,y=0},
            new Point(){x=1,y=1},
        };

        /// <summary>
        /// Main calculation method for a new generation of cells.
        /// I would not actually include the grid size parameters of x and y
        /// Because the function would be more abstract and independed, however
        /// Front end, if there is such, should not handle the logic
        /// of checking if the cell is actually in the boundaries of the grid.
        /// Another possible solution would be extend the grid as long as we create
        /// a new cell in non existing grid coordinates.
        /// 
        /// This solution does not need to loop through the whole grid and check
        /// every possible cell, all we need to know if the life cells to
        /// calculate which cells will come alive and which will die out.
        /// </summary>
        /// <param name="x">Grid size x</param>
        /// <param name="y">Grid size y</param>
        /// <param name="oldGeneration"></param>
        /// <returns>New generation of cells</returns>
        public IEnumerable<GridCell> GetNewGeneration(int x, int y, IEnumerable<GridCell> oldGeneration)
        {
            //Fail fast
            if (oldGeneration == null || !oldGeneration.Any()) return Enumerable.Empty<GridCell>();

            var stayingAliveCells = oldGeneration.Where(t =>
            {
                var sum = ExecuteCellNeighbourFunction((Point p) =>
                {
                    var isLiveNeighbour = oldGeneration.FirstOrDefault(c => c.XAxysCoordinates == (t.XAxysCoordinates + p.x) && c.YAxisCoordinates == (t.YAxisCoordinates + p.y));
                    return isLiveNeighbour == null ? 0 : 1;
                }).Sum();

                return sum == 3 || sum == 2;
            });

            // Getting new generation cells that will come alive
            var newGeneration = oldGeneration
                            .SelectMany(g =>
                            {
                                return ExecuteCellNeighbourFunction((p) =>
                                {
                                    return new GridCell() { XAxysCoordinates = g.XAxysCoordinates + p.x, YAxisCoordinates = g.YAxisCoordinates + p.y };
                                });
                            })
                            .Where(g => !oldGeneration.Any(o => o.XAxysCoordinates == g.XAxysCoordinates && o.YAxisCoordinates == g.YAxisCoordinates) && IsFitting(g.XAxysCoordinates, g.YAxisCoordinates, x, y))
                            .GroupBy(g => new { g.YAxisCoordinates, g.XAxysCoordinates })
                            .Where(g => g.Count() == 3)
                            .Select(g => new GridCell() { XAxysCoordinates = g.Key.XAxysCoordinates, YAxisCoordinates = g.Key.YAxisCoordinates });


            return stayingAliveCells.Concat(newGeneration);
        }

        /// <summary>
        /// Abstracting out the iteration over Point collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="neighbourFunction"></param>
        /// <returns></returns>
        private IEnumerable<T> ExecuteCellNeighbourFunction<T>(Func<Point, T> neighbourFunction)
        {
            var newNeighbours = new List<T>();
            _neighbourPoints.ForEach(i =>
            {
                newNeighbours.Add(neighbourFunction(i));
            });

            return newNeighbours;
        }

        /// <summary>
        /// Method to check if the Cell will fit in the grid, if it does not
        /// break the boundaries of the grid.
        /// </summary>
        /// <param name="xaxis"></param>
        /// <param name="yaxis"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>True if the cell fits in the grid</returns>
        private bool IsFitting(int xaxis, int yaxis, int x, int y)
        {
            return xaxis < x && yaxis < y;
        }
    }
}
