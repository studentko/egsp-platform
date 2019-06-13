import { Component, OnInit } from '@angular/core';
import { Station } from '../station';
import { StationService } from '../station.service';
import { LineService } from '../line.service';
import { Line } from '../line';
import { DepartureTable } from '../departure-table';
import { DepartureTableService } from '../departure-table.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  constructor(private stationService: StationService,
    private lineService: LineService,
    private departureTableService: DepartureTableService) { }

  newStation: Station = new Station();
  stationEditing: boolean;

  newTable: DepartureTable = new DepartureTable();
  tableEditing: boolean;

  newLine: Line = new Line();
  lineEditing: boolean;
  newLineStations = [];

  errMsg: string;
  errMsgL: string;
  errMsgD: string;

  ngOnInit() {
    this.stationService.getStations().subscribe();
    this.lineService.getLines().subscribe();
    this.newLineStations = this.stationService.stations.map(x => false);
    this.departureTableService.getTables().subscribe();
  }

  deleteStation(station: Station) : void {
    this.stationService.deleteStation(station.Id).subscribe();
    this.lineService.getLines().subscribe();
  }

  editStation(station: Station){
    this.newStation = station;
    this.stationEditing = true;
  }

  addOrEditStation() : void {
    if(!this.stationEditing){
      this.stationService.addStation(this.newStation).subscribe(
        data => {
          if(data.IsSuccess === false){
            this.errMsg = data.ErrorMessage;
          }
        }
      );
    }
    else{
      this.stationService.editStation(this.newStation).subscribe(
        data => {
          if(data.IsSuccess === false){
            this.errMsg = data.ErrorMessage;
          }
        }
      );
    }
  }

  addOrEditLine() : void {
    if(!this.lineEditing){
      this.newLine.BusStations = [];
      this.newLineStations.forEach((x, i) => {
        if(x){
          this.newLine.BusStations.push(this.stationService.stations[i]);
        }
      })
      this.lineService.addLine(this.newLine).subscribe(
        data => {
          if(data.IsSuccess === false){
            this.errMsgL = data.ErrorMessage;
            this.stationService.getStations().subscribe();
          }
        }
      );
    }
    else{
      this.newLine.BusStations = [];
      this.newLineStations.forEach((x, i) => {
        if(x){
          this.newLine.BusStations.push(this.stationService.stations[i]);
        }
      })
      this.lineService.editLine(this.newLine).subscribe(
        data => {
          if(data.IsSuccess === false){
            this.errMsg = data.ErrorMessage;
            this.stationService.getStations();
          }
        }
      );
    }
  }

  deleteLine(line: Line) : void {
    this.lineService.deleteLine(line.Id).subscribe(x => {
      this.stationService.getStations();
    });
  }

  editLine(line: Line){
    this.newLine = line;
    this.lineEditing = true;
    this.newLineStations = this.stationService.stations.map(x => line.BusStations.find(y => x.Id === y.Id) != undefined);
  }

  deleteTable(table: DepartureTable) : void {
    this.departureTableService.deleteTable(table.Id).subscribe();
    this.lineService.getLines().subscribe();
  }

  editTable(table: DepartureTable){
    this.newTable = table;
    this.tableEditing = true;
  }

  addOrEditTable() : void {
    if(!this.tableEditing){
      this.departureTableService.addTable(this.newTable).subscribe(
        data => {
          if(data.IsSuccess === false){
            this.errMsgD = data.ErrorMessage;
          }
        }
      );
    }
    else{
      this.departureTableService.editTable(this.newTable).subscribe(
        data => {
          if(data.IsSuccess === false){
            this.errMsgD = data.ErrorMessage;
          }
        }
      );
    }
  }
}
