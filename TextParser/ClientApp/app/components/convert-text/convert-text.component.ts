import { Component } from "@angular/core";
import { Http, Headers } from "@angular/http";

@Component({
    selector: "convert-text",
    templateUrl: "./convert-text.component.html"
})
export class ConvertTextComponent {
    public vm: ConvertTextComponent = this;
    public textToConvert: string = "";
    public convertedText = "";

    private http: Http;
    private baseApiUrl: string;

    constructor(http: Http) {
        this.http = http;
        this.baseApiUrl = "http://localhost:11932/api/Text/";
    }

    public toXml() {
        this.serializeText("ToXml");
    }

    public toCsv() {
        this.serializeText("ToCsv");
    }

    private serializeText(type: string) {
        const headers = new Headers({ 'Content-Type': "text/json" });

        this.http.post(
                this.baseApiUrl + type,
                { text: this.textToConvert },
                {
                    headers: headers
                }
            )
            .subscribe(result => {
                    this.convertedText = result.text();
                },
                error => console.error(error));
    }
}