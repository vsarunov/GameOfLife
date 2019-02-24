import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { LifegridComponent } from './lifegrid/lifegrid.component';

import { CellService } from './services/cell.service';

@NgModule({
  declarations: [
    AppComponent,
    LifegridComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpModule,
    FormsModule
  ],
  providers: [CellService],
  bootstrap: [AppComponent]
})
export class AppModule { }
