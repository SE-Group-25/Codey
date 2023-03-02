import {AfterViewChecked, Component} from '@angular/core';

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
  constructor() {
    this.messages = [
      {"bot": true, "text": "Hi, my name is Codey!\nWhat course are you studying?"}
    ];
    // this.oldChatbotDiv = window.document.getElementById('scroll_view')!;
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

  addMessage(s: string, b: boolean) {
    this.messages.push({"bot":b, "text":s});
    this.updateScroll = true;
  }

  splitMessage(s: string) {
    return s.split('\n');
  }

  onSubmit(event: any) {
    let message = event.target.text.value;
    if (!this.canSend || message == "") return;
    this.addMessage(message, false);
    // CALL WATSON API WITH message
    event.target.text.value = "";
  }
}
