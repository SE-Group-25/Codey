import {AfterViewChecked, Component} from '@angular/core';
import { ChatbotService } from '../services/chatbotservice/chatbot.service';

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
  constructor(private _chatbot: ChatbotService) {
    this.messages = [];

    let landingPage = '{"output":{"generic":[{"response_type":"text","text":"Hi, I\'m Codey!\\nHow can I help you today?"},{"options":[{"label":"Tell me about the IBM Skills Build?","value":{"input":{"text":"Tell me about the IBM Skills Build?"}}},{"label":"What can you do to help?","value":{"input":{"text":"What can you do to help?"}}},{"label":"Can you recommend me a course?","value":{"input":{"text":"Can you recommend me a course?"}}},{"label":"Can you direct me to the IBM Skills Build?","value":{"input":{"text":"Can you direct me to the IBM Skills Build?"}}}],"response_type":"option","repeat_on_reprompt":true}]}}';
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
    this.messages.push({"bot":false, "text":s});
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
    let newMessage : any = {"bot": true};
    let json = JSON.parse(s);
    for (let k of json['output']['generic'].keys()) {
      switch(json['output']['generic'][k]['response_type']) {
        case 'text':
          newMessage['text'] = json['output']['generic'][k]['text'];
          break;
        case 'option':
          newMessage['options'] = json['output']['generic'][k]['options'];
          break;
      }
    }
    this.addBotMessage(newMessage);
  }

  onSubmit(event: any) {
    let message = event.target.text.value;
    if (!this.canSend || message == "") return;
    this.addUserMessage(message);
    // CALL WATSON API WITH message
    this._chatbot.sendMessage('chatbot/Message', message)
      .subscribe((res: string)  => {
        this.messages.push(res);
      })

    event.target.text.value = "";
  }

  chooseOption(option: any) {
    if (!this.canSend) return;
    this.addUserMessage(option['value']['input']['text']);
    // CALL WATSON API WITH message
  }
}
