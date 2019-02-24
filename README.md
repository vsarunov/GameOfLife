# GameOfLife
Conways game of life

The solution is an SPA type .NET core solution.

To run: clean and build the solution, this should install npm packages. Otherwise cmd to ClientApp and run "npm install".
If the application throws an exception when running the web app, try to restart sometimes angular timeout occurs when connecting.

The Front-end grid is made of table and buttons, not the best solution but for logic demonstration purpose it is fine IMHO.
To start testing just select the initial shape by clicking on the cells in the grid and click next generation button,
you can click it further after. Previous generation can go only to the last generation, it does not go further, did not want
to overload the browser with unnecessary data. You can change the grid size if you want to if you make the grid big, for example 40x40 
and select a cell wich would be in coordinates 39 39 (zero based) and then you make it smaller to 20 x 20 that cell would be dismissed.
Otherwise if it still fits, will stay there.

Most of the calculation logic is with comments, which should explain why I did what.

Otherwise there are so points to notice:

The logic of the application is to perform calculation based on the cells that are alive. Thus we expect only to received a list of
coordinates of x and y with cells that are alive.

There are two model classes "Point" and "GridCell" which are from first look similar, but they have different purpose, thus I kept them
separately, but the argument to reuse just one is as well valid as it keeps it dry, but while one is only for calculation purpose
the other is for keeping track of coordinates, did not want to mix up both.

As well, the calculation method accepts x and y - gird size. However, IMHO, the calculations should not care about it, it would make the
calculations like they are and that is it. But front end should not handle the logic of checking which live cell fits in the grid, thus keeping
it inside the calculation logic, otherwise would actually add a service on top between the controller and the calculation class which
would handle the grid size and fitting cell filterings. Leaving the calculation logic to be more abstract from the front end and
reusable as a separate dll.

Some things I would add or do differently:
1. Would make front end more robust and find a better solution to display cell grids as this is a bit overkill with the buttons.
2. Would add more security and security headers e.g csp, cors.
3. Would think about more abtsraction on calculation logic.
4. Would make the calculation library a .NET standar solution, to be reusable across.
