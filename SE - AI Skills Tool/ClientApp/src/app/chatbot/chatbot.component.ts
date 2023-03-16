import {AfterViewChecked, Component} from '@angular/core';
import { ChatbotService } from '../services/chatbotservice/chatbot.service';
import { MessageDto, MessageResponseDto } from '../interfaces/message-dto';

@Component({
  selector: 'app-chatbot',
  templateUrl: './chatbot.component.html',
  styleUrls: ['./chatbot.component.css']
})
export class ChatbotComponent implements AfterViewChecked {
  public messages: Array<any> = [];
  private canSend: boolean = true;
  public isChatting: boolean = true;

  // private oldChatbotDiv: HTMLElement;

  public updateScroll: boolean = false;
  public messageDto: MessageDto = {};
  public userCourses: any = [];

  public variables: string[] = [];

  constructor(private _chatbot: ChatbotService) {
    this.initialize();
  }

  public ngAfterViewChecked(): void {
    this.scroll();
  }

  initialize() {
    this.messages = [];
    let landingPage = '{"output":{"generic":[{"response_type":"text","text":"Hi, I\'m Codey!\\nHow can I help you today?"},{"options":[{"label":"Tell me about the IBM Skills Build?","value":{"input":{"text":"Tell me about the IBM Skills Build?"}}},{"label":"What can you do to help?","value":{"input":{"text":"What can you do to help?"}}},{"label":"Can you recommend me a course?","value":{"input":{"text":"Can you recommend me a course?"}}},{"label":"Can you direct me to the IBM Skills Build?","value":{"input":{"text":"Can you direct me to the IBM Skills Build?"}}}],"response_type":"option","repeat_on_reprompt":true}]}}';
    this.addResponse(landingPage);
  }

  scroll() {
    if (!this.updateScroll) return;

    let chatbotDiv = window.document.getElementById('scroll_view')!;
    if (chatbotDiv != null) chatbotDiv.scrollTop = chatbotDiv.scrollHeight;
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

  processResults(variables: string[]) {
    this.isChatting = false;
    console.log(variables);
    // Process Results (add to userCourses)
    this._chatbot.getCourses('Course/GetCourses', variables).subscribe(
      {next: res => (this.userCourses = res)}
    )    ;
  }

  addResponse(s: string) {
    let newMessage : any = {"bot": true, "texts": []};
    let json = JSON.parse(s);
    for (let k of json['output']['generic'].keys()) {
      switch(json['output']['generic'][k]['response_type']) {
        case 'text':
          let text = json['output']['generic'][k]['text'];
          if (text.includes('::')) {
            let variables = text.split('::').filter((str: string) => str !== '');
            let id = variables.shift();
            switch (id) {
              case 'd93dd9de-2a11-4a40-b10f-42e67e32a945':
                this.variables = [...new Set([...this.variables, ...variables])];
                break;
              default:
                this.initialize();
                break;
            }
          }
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
    if (this.variables.length != 0) this.processResults(this.variables);
    this.messageDto.sessionId = json['user_id'];
    this.addBotMessage(newMessage);
  }

  parseURL(s: string) {
    return s.replace(/\[([^\[]+)\](\(([^)]*))\)/gim, '<a class="link" href="$3">$1</a>');
  }

  onSubmit(event: any) {
    let message = event.target.text.value;
    if (!this.canSend || message == "") return;
    this.addUserMessage(message);
    this.callWatson(message);

    event.target.text.value = "";
  }

  callWatson(message: string) {
    this.messageDto!.msgString = message;
    this._chatbot.sendMessage('chatbot/Message', this.messageDto!).subscribe({
      next: (res: MessageResponseDto) => {
        this.addResponse(res.responseString);
      }
    });
  }

  chooseOption(option: any) {
    if (!this.canSend) return;
    this.addUserMessage(option['label']);
    this.callWatson(option['label']);
  }
}
