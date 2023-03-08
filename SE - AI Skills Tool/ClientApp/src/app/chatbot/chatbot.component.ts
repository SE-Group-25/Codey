import {AfterViewChecked, Component} from '@angular/core';
import { ChatbotService } from '../services/chatbotservice/chatbot.service';
import { MessageDto, MessageResponseDto } from '../interfaces/message-dto';

@Component({
  selector: 'app-chatbot',
  templateUrl: './chatbot.component.html',
  styleUrls: ['./chatbot.component.css']
})
export class ChatbotComponent implements AfterViewChecked {
  public messages: Array<any>;
  private canSend: boolean = true;

  // private oldChatbotDiv: HTMLElement;

  public updateScroll: boolean = false;
  public messageDto: MessageDto = {};

  constructor(private _chatbot: ChatbotService) {
    this.messages = [];

    let landingPage = '{"output":{"generic":[{"response_type":"text","text":"Hi, I\'m Codey!\\nHow can I help you today?"},{"options":[{"label":"Tell me about the Skills Build?","value":{"input":{"text":"Tell me about the Skills Build?"}}},{"label":"What can you do to help?","value":{"input":{"text":"What can you do to help?"}}},{"label":"Can you recommend me a course?","value":{"input":{"text":"Can you recommend me a course?"}}},{"label":"Can you direct me to the IBM Skills Build?","value":{"input":{"text":"Can you direct me to the IBM Skills Build?"}}}],"response_type":"option","repeat_on_reprompt":true}]}}';
    this.addResponse(landingPage);
  }

  public ngAfterViewChecked(): void {
    this.scroll();
  }

  scroll() {
    if (!this.updateScroll) return;

    let chatbotDiv = window.document.getElementById('scroll_view')!;
    chatbotDiv.scrollTop = chatbotDiv.scrollHeight;
    this.updateScroll = false;
  }

  addUserMessage(s: string) {
    this.messages.push({"bot":false, "texts":[s]});
    this.updateScroll = true;
  }

  addBotMessage(message: any) {
    this.messages.push(message);
    this.updateScroll = true;
  }

  splitMessage(s: string) {
    return s.split('\n');
  }

  addResponse(s: string) {
    let newMessage : any = {"bot": true, "texts": []};
    let json = JSON.parse(s);
    for (let k of json['output']['generic'].keys()) {
      switch(json['output']['generic'][k]['response_type']) {
        case 'text':
          newMessage['texts'].push(json['output']['generic'][k]['text']);
          break;
        case 'option':
          newMessage['options'] = json['output']['generic'][k]['options'];
          break;
        case 'suggestion':
          newMessage['texts'].push(json['output']['generic'][k]['title']);
          newMessage['suggestions'] = json['output']['generic'][k]['suggestions'];
          break;
      }
    }
    this.messageDto.sessionId = json['user_id'];
    this.addBotMessage(newMessage);
    console.log(this.messages);
  }

  onSubmit(event: any) {
    let message = event.target.text.value;
    if (!this.canSend || message == "") return;
    this.addUserMessage(message);
    // CALL WATSON API WITH message
    this.messageDto!.msgString = message;

    console.log(this.messageDto.sessionId);

    this._chatbot.sendMessage('chatbot/Message', this.messageDto!)
      .subscribe({
        next: (res: MessageResponseDto) => {
          this.addResponse(res.responseString);
          //console.log(res.responseString);
          // let jsonRes = JSON.parse(res.responseString);
          //   this.addBotMessage(jsonRes.output.generic[0].text);
          //   this.messageDto!.sessionId = jsonRes.user_id;
          //   console.log(jsonRes.output.generic[0].text)
        }});

    event.target.text.value = "";
  }

  chooseOption(option: any) {
    if (!this.canSend) return;
    this.addUserMessage(option['label']);
    // CALL WATSON API WITH message

    this.messageDto!.msgString = option['label'];

    this._chatbot.sendMessage('chatbot/Message', this.messageDto!)
      .subscribe({
        next: (res: MessageResponseDto) => {
          this.addResponse(res.responseString);
        }});
  }
}
