import { Injectable } from '@angular/core';
import * as AppID from 'ibmcloud-appid-js';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private appId;
  private accessToken: string = "";
  private userInfo: any;
  // private attributes: any;

  constructor() {
    this.appId = new AppID();
    this.initClient().then(() => console.log('Initialized AuthService.'));
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
    return !!this.userInfo;
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
    const { accessToken } = await this.appId.signin();
    await this.setUser(accessToken);
    sessionStorage.setItem('token', this.accessToken);
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
    console.log(await this.appId.getUserInfo(this.accessToken));
    return await this.appId.getUserInfo(this.accessToken);
  }
}