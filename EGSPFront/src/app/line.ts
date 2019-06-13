import { Station } from './station';

export class Line {
    Id: number
    LineNumber: string;
    BusStations: Station[];
    DepartureTables: [];
}
