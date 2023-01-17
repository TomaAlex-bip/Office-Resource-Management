

export class DeskReservationViewModel {
    
    deskName: string;
    reservedFrom: string;
    reservedUntil: string;

    constructor(deskName: string, reservedFrom: string, reservedUntil: string) {
        this.deskName = deskName;
        this.reservedFrom = reservedFrom;
        this.reservedUntil = reservedUntil;
    }

}