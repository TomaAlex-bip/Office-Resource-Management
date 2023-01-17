import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";


@Injectable({
    providedIn: 'root'
})
export class HttpRequestService{

    constructor(private httpClient: HttpClient) {

    }

    async makePostRequest(body: any, header: any, url: string): Promise<any> {
        return await new Promise<any>( resolve => {
            this.httpClient.post(url, body, header)
            .subscribe({

                next: (data) => {
                    let response = JSON.parse(JSON.stringify(data));
                    resolve(response);
                },

                error: (error) => {
                    let response = JSON.parse(JSON.stringify(error));
                    resolve(response);
                }
                
            });
        });
    }

    async makeDeleteRequest(header: any, url: string): Promise<any> {
        return await new Promise<any>( resolve => {
            this.httpClient.delete(url, header)
            .subscribe({

                next: (data) => {
                    let response = JSON.parse(JSON.stringify(data));
                    resolve(response);
                },

                error: (error) => {
                    let response = JSON.parse(JSON.stringify(error));
                    resolve(response);
                }
                
            });
        });
    }

    async makeUpdateRequest(body: any, header: any, url: string): Promise<any> {
        return await new Promise<any>( resolve => {
            this.httpClient.put(url, body, header)
            .subscribe({

                next: (data) => {
                    let response = JSON.parse(JSON.stringify(data));
                    resolve(response);
                },

                error: (error) => {
                    let response = JSON.parse(JSON.stringify(error));
                    resolve(response);
                }
                
            });
        });
    }

}