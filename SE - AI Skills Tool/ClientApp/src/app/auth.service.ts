import { Injectable } from '@angular/core';
import * as AppID from 'ibmcloud-appid-js';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private appId;
  private accessToken: string = "";
  private userInfo: any;

  constructor() {
    this.appId = new AppID();
    this.initClient().then(() => console.log('initialized AuthService.'));
  }

  private async initClient() {
    await this.appId.init({
      clientId: '6ba65701-7332-45ac-8a41-839563e7cbf6',
      discoveryEndpoint: 'https://eu-gb.appid.cloud.ibm.com/oauth/v4/5970f38e-ee73-49f5-9bd8-64c9268e7933/.well-known/openid-configuration'
    });
    if ((sessionStorage.getItem('token')) !== null) {
      await this.setUser(sessionStorage.getItem('token'));
    }
  }

  get isAuthenticated(): boolean {
    return !!this.accessToken;
  }

  async setUser(token: any) {
    this.accessToken = token;
    this.userInfo = await this.getUserInfo();
  }

  async signIn() {
    const { accessToken } = await this.appId.signin();
    this.accessToken = accessToken;
    sessionStorage.setItem('token', this.accessToken);
    this.userInfo = await this.getUserInfo();
  }

  async signOut() {
    sessionStorage.removeItem('token');
    this.accessToken = "";
    this.userInfo = undefined;
  }

  get name() {
      console.log(this.userInfo.name);
      return this.userInfo.name;
  }

  get email() {
    console.log(this.userInfo.email);
    return this.userInfo.email;
  }

  async getUserInfo() {
    console.log(await this.appId.getUserInfo(this.accessToken));
    return await this.appId.getUserInfo(this.accessToken);
  }
}
