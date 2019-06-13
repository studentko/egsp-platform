import { Component, OnInit } from '@angular/core';
import { LineService } from '../line.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Line } from '../line';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit {

  constructor(private lineService : LineService) { }

  lines: Line[] = [];
  selectedLine: Line;
  errMsg: string;

  ngOnInit() {
    //this.selectedLine = new Line();
    this.lineService.getLines().subscribe(aaa =>
      {
        if(aaa instanceof HttpErrorResponse){
          this.errMsg = "Failed to obtain schedule";
        }
        else{
          this.lines = this.lineService.lines;
          this.selectedLine = this.lines[0];
          console.log(this.lines.length);
          console.log(this.lines);
          console.log(this.selectedLine);
        }
      }
      )
  }

  onLineSelect(  ) {
    //this.selectedLine=line;
    //console.log(line);
    console.log(this.selectedLine);
    console.log(this.selectedLine.Id);
    //console.log($event);
  }

}
