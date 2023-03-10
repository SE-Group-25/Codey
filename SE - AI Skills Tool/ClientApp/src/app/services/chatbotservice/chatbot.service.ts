import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MessageDto, MessageResponseDto } from '../../interfaces/message-dto';

@Injectable({
  providedIn: 'root'
})
export class ChatbotService {
  baseUrl?: string;

  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public sendMessage(route: string, messageDto: MessageDto): Observable<MessageResponseDto> {
    return this._http.post<MessageResponseDto>(this.baseUrl + route, messageDto);
  }
}
