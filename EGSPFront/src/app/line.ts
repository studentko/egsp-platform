import { Station } from './station';

export class Line {
    Id: number
    LineNumber: string;
    UpdateVerion: number;
    BusStations: Station[];
    DepartureTables: [];
}
