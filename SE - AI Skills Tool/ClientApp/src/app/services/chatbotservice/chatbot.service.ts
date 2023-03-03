import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatbotService {
  baseUrl?: string;

  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public sendMessage(route: string, message: string): Observable<string> {
    return this._http.post<string>(this.baseUrl + route, message);
  }
}
