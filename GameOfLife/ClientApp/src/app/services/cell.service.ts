import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/retry';
import { GridCell } from '../typings/types.typings';

@Injectable()
export class CellService {

    private baseUrl: string;
    constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    getLiveCells(x: number, y: number, liveCells: GridCell[]): Observable<GridCell[]> {

        var cells: GridCell[] = [];

        return this._http.put(this.baseUrl + 'api/GameOfLife/' + x + "/" + y, liveCells)
            .map((response: Response) => { return response.json() })
            .retry(1)
            .catch((error: any) => {
                return Observable.of(cells);
            });
    }
}