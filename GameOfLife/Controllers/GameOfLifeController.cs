namespace GameOfLife.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GameOfLifeCalculationLibrary;
    using GameOfLifeCalculationLibrary.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class GameOfLifeController : ControllerBase
    {
        private readonly IGameOfLifeCalculator _gameOfLifeCalculator;
        public GameOfLifeController(IGameOfLifeCalculator gameOfLifeCalculator)
        {
            _gameOfLifeCalculator = gameOfLifeCalculator;
        }

        [HttpPut("{x}/{y}")]
        [ProducesResponseType(typeof(List<GridCell>), 200)]
        public IActionResult Get(int x, int y, [FromBody]List<GridCell> gridCells)
        {
            //It is not a deal breaker so still Ok.
            if (gridCells == null || !gridCells.Any())
            {
                return Ok(new List<GridCell>());
            }

            var result = _gameOfLifeCalculator.GetNewGeneration(x, y, gridCells);

            return Ok(result);
        }
    }
}