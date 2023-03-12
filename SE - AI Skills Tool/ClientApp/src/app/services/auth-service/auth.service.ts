import { Inject, Injectable } from '@angular/core';
import * as AppID from 'ibmcloud-appid-js';
import { HttpClient } from "@angular/common/http";
import { UserDto } from "../../interfaces/user-dto";

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private appId;
  private accessToken: string = "";
  private userInfo: any;
  private userDTo: UserDto = {};
  // private attributes: any;

  private isInitialised: boolean = false;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.appId = new AppID();
    this.initClient().then(() => { console.log('Initialized AuthService.'); this.isInitialised = true; });
  }

  private async initClient() {
    await this.appId.init({
      clientId: '6ba65701-7332-45ac-8a41-839563e7cbf6',
      discoveryEndpoint: 'https://eu-gb.appid.cloud.ibm.com/oauth/v4/5970f38e-ee73-49f5-9bd8-64c9268e7933/.well-known/openid-configuration',
      popup: {
        height: window.screen.height * 0.60,
        width: window.screen.height * 0.40
      }
    });
    if ((sessionStorage.getItem('token')) !== null) {
      await this.setUser(sessionStorage.getItem('token'));
    }
  }

  get isAuthenticated(): boolean {
    return (!!this.userInfo) && this.isInitialised;
  }

  async setUser(token: any) {
    this.accessToken = token;
    this.userInfo = await this.getUserInfo();
    // this.attributes = await this.getAttributes();
  }

  async signIn() {
    // const tokens = await this.appId.signin();
    // console.log(tokens);
    // const accessToken = tokens.accessToken;
    try {
      const { accessToken } = await this.appId.signin();
      await this.setUser(accessToken);
      sessionStorage.setItem('token', this.accessToken);
      this.userDTo.Id = this.ID;
      this.http.post<any>(this.baseUrl + 'User/CreateUser', this.userDTo!).subscribe({
        next: () => {
          console.log("User added to DB");
        }
      });
    } catch {
      console.log("Login Failed.");
    }
  }

  async signOut() {
    sessionStorage.removeItem('token');
    this.accessToken = "";
    this.userInfo = undefined;
  }

  get name() {
      return this.userInfo.name;
  }
  get givenName() {
    return this.userInfo.given_name;
  }

  get email() {
    return this.userInfo.email;
  }

  get ID() {
    return this.userInfo.sub;
  }

  get user() {
    return this.userInfo;
  }

  async getUserInfo() {
    return await this.appId.getUserInfo(this.accessToken);
  }
}
