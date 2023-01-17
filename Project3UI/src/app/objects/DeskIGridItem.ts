import { GridsterItem } from 'angular-gridster2';

export class DeskGridItem {
    
    item: GridsterItem;
    name: string;
    rotationIndex: number;
    isOccupied: boolean;
    occupyingUser: string | null;
    startDate: string | null;
    endDate: string | null;

    private _oldName: string;
    public get oldName() {
        return this._oldName;
    }
    
    constructor(
        name: string, 
        isOccupied: boolean, 
        rotationIndex: number, 
        item: GridsterItem, 
        occupyingUser: string | null,
        startDate: string | null,
        endDate: string | null
        ) 
    {
        this.name = name;
        this._oldName = name;
        this.isOccupied = isOccupied;
        this.rotationIndex = rotationIndex;
        this.item = item;
        this.occupyingUser = occupyingUser;
        this.startDate = startDate;
        this.endDate = endDate;
    }
}