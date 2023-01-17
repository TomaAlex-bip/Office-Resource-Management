
export interface Desk {
    name: string;
    gridPositionX: number
    gridPositionY: number
    orientation: number
    occupyingUser: string | null
    startDate: string | null
    endDate: string | null
    errorMessage: string | null
}