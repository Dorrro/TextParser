import { Component } from "@angular/core";
import { Http, Headers } from "@angular/http";

@Component({
    selector: "convert-text",
    templateUrl: "./convert-text.component.html",
    styleUrls: ["./convert-text.component.css"]
})
export class ConvertTextComponent {
    public vm = this;
    public textToConvert: string = "";
    public convertedText = "";
    public errorOccured = false;
    public isLoading = false;

    private http: Http;
    private baseApiUrl: string;

    constructor(http: Http) {
        this.http = http;
        this.baseApiUrl = "http://localhost:11932/api/Text/";
    }

    public toXml(): void {
        this.serializeText("ToXml");
    }

    public toCsv(): void {
        this.serializeText("ToCsv");
    }

    private serializeText(type: string): void {
        this.errorOccured = false;
        this.isLoading = true;
        const headers = new Headers({ 'Content-Type': "text/json" });

        this.http.post(
                this.baseApiUrl + type,
                { text: this.textToConvert },
                {
                    headers: headers
                }
            )
            .subscribe(
                result => {
                    this.convertedText = result.text();
                    this.isLoading = false;
                },
                error => {
                    console.error(error);
                    this.errorOccured = true;
                    this.isLoading = false;
                });
    }
}