import { Line } from './line';

export class DepartureTable {
    Id: number
    DayOfWeek: number
    DayOfWeekString: string
    DepartureTimes: string
    BusLineId: number
    BusLine: Line
}
