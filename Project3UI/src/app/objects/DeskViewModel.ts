

export class DeskViewModel {
    
    name: string;
    gridPositionX: number;
    gridPositionY: number;
    orientation: number;

    constructor(name: string, gridPosX: number, gridPosY: number, orientation: number) {
        this.name = name;
        this.gridPositionX = gridPosX;
        this.gridPositionY = gridPosY;
        this.orientation = orientation;
    }

}