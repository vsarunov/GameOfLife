import { Component, OnInit, AfterViewChecked, OnDestroy } from '@angular/core';
import { GridCell } from '../typings/types.typings';
import { CellService } from '../services/cell.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-lifegrid',
  templateUrl: './lifegrid.component.html',
  styleUrls: ['./lifegrid.component.css']
})
export class LifegridComponent implements OnInit, OnDestroy, AfterViewChecked {

  cells: number[][] = [];// to control the numbers of cells in the table
  liveCells: GridCell[] = [];//live cells
  previousGeneration: GridCell[] = [];
  xaxys: number = 10;//default grid size
  yaxys: number = 10;//default grid size
  private cellSubscription: Subscription;
  constructor(private _cellService: CellService) { }

  ngOnInit() {
    this.updateGridSize();
  }

  //Probably there is a better way to do it and loop through in the view
  public updateGridSize(): void {
    this.removeOutOfGridSizeLiveCells();
    this.RefreshCells();
  }

  private RefreshCells() {
    this.cells = [];
    for (var i: number = 0; i < this.xaxys; i++) {
      this.cells[i] = [];
      for (var j: number = 0; j < this.yaxys; j++) {
        this.cells[i][j] = 0;
      }
    }
  }

  public select(event, x: number, y: number): void {
    var target = event.target || event.srcElement || event.currentTarget;
    var idAttr = target.attributes.id;
    var value = idAttr.nodeValue;

    var result = this.liveCells.map(function (e) { return e.xAxysCoordinates + " " + e.yAxisCoordinates; }).indexOf(x + " " + y);
    if (result === -1) {
      this.liveCells.push({ xAxysCoordinates: x, yAxisCoordinates: y });
      document.getElementById(value).style.backgroundColor = "#000000";
    }
    else {
      this.liveCells.splice(result, 1);
      document.getElementById(value).style.backgroundColor = "#ffffff";
    }
  }

  public getNextGeneration(): void {
    this.cellSubscription = this._cellService.getLiveCells(this.xaxys, this.yaxys, this.liveCells).subscribe((cells: GridCell[]) => {
      this.previousGeneration=this.liveCells;
      this.liveCells = cells;
      console.log(cells);
      this.RefreshCells();
      this.updateLiveCellAfterGridSizeChange();
    });
  }

  // When grid size changes it removes all the old buttons and we have to remark those that have been originally selected 
  //and are in grid range
  private updateLiveCellAfterGridSizeChange(): void {
    for (var i: number = 0; i < this.xaxys; i++) {
      for (var j: number = 0; j < this.yaxys; j++) {
        var result = this.liveCells.map(function (e) { return e.xAxysCoordinates + " " + e.yAxisCoordinates; }).indexOf(i + " " + j);
        if (result !== -1) {
          document.getElementById(i + "" + j).style.backgroundColor = "#000000";
        }
      }
    }
  }

  // If grid gets smaller and we selected the cells that are out of range, remove those cells from live cells.
  private removeOutOfGridSizeLiveCells(): void {
    var res = this.liveCells.filter(i => i.xAxysCoordinates < this.xaxys && i.yAxisCoordinates < this.yaxys);
    this.liveCells = res;
  }

  private getPreviousGeneration(): void {
    this.liveCells = this.previousGeneration;
    this.RefreshCells();
    this.updateLiveCellAfterGridSizeChange();
  }

  ngAfterViewChecked(): void {
    this.updateLiveCellAfterGridSizeChange();// Has to be done after the grid size changed as otherwise the buttons do not exist
  }

  ngOnDestroy(): void {
    this.cellSubscription.unsubscribe();
  }
}
