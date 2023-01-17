

export interface DeskAllocation {
    deskName: string;
    username: string;
    reservedFrom: string;
    reservedUntil: string;
    errorMessage: string | null;
}