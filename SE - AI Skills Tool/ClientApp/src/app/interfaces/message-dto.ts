export interface MessageDto {
  msgString?: string;
  sessionId?: string;
}

export interface MessageResponseDto {
  responseString: string;
  isSuccessful: boolean;
}
