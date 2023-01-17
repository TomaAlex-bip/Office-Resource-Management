
export class User{
    
    private _username: string;
    get username() {
        return this._username;
    }
    
    private _role: string;
    get role() {
        return this._role;
    }

    constructor(username: string, role: string) {
        this._username = username;
        this._role = role;
    }

}