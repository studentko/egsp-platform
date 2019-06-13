import { Component, OnInit } from '@angular/core';
import { Station } from '../station';
import { StationService } from '../station.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  constructor(private stationService: StationService) { }

  newStation: Station = new Station();
  stationEditing: boolean;
  errMsg: string;

  ngOnInit() {
    this.stationService.getStations().subscribe();
  }

  deleteStation(station: Station) : void {
    this.stationService.deleteStation(station.Id).subscribe();
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

}
