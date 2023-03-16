using IBM.Cloud.SDK.Core.Authentication.Iam;
using SE_AI_Skills_Tool.Context;
using IBM.Watson.Assistant.v2;
using IBM.Watson.Assistant.v2.Model;
using SE_AI_Skills_Tool.BusinessLogic;

namespace SE_AI_Skills_Tool.Services
{
    public interface IWatson
    {
        MessageResponseDto Message(string msgString, string sessionId);

        string CreateSession();

        void DeleteSession(string sessionId);
    }

    public class Watson: IWatson
    {
        private readonly AstDevContext _astDev;

        // Watson API calls and handling returned values from watson. Ensure async running

        private readonly string apikey = "";
        private readonly string url = "";
        private readonly string versionDate = "2022-06-01";
        private readonly string assistantId = "";
        private readonly string environmentId = "";

        public Watson(AstDevContext astDev)
        {
            _astDev = astDev;
        }

        public string CreateSession()
        {
            IamAuthenticator authenticator = new IamAuthenticator(apikey: $"{apikey}");

            AssistantService service = new AssistantService($"{versionDate}", authenticator);
            service.SetServiceUrl($"{url}");

            var result = service.CreateSession(
                                               assistantId: $"{environmentId}"
                                              );
            return result.Result.SessionId;
        }

        public void DeleteSession(string sessionId)
        {
            IamAuthenticator authenticator = new IamAuthenticator(apikey: $"{apikey}");
        
            AssistantService service = new AssistantService($"{versionDate}", authenticator);
            service.SetServiceUrl($"{url}");
        
            var result = service.DeleteSession(
                                               assistantId: $"{environmentId}",
                                               sessionId: $"{sessionId}"
                                              );
        }

        public MessageResponseDto Message(string msgString, string sessionId)
        {
            IamAuthenticator authenticator = new IamAuthenticator(apikey: $"{apikey}");

            AssistantService service = new AssistantService($"{versionDate}", authenticator);
            service.SetServiceUrl($"{url}");

            var result = service.Message(
                                         assistantId: $"{environmentId}",
                                         sessionId: $"{sessionId}",
                                         input: new MessageInput()
                                                {
                                                    Text = msgString
                                                });

            MessageResponseDto messageResponse = new MessageResponseDto();
            messageResponse.ResponseString = result.Response;
            messageResponse.IsSuccessful = true;

            return messageResponse;
        }
    }
}
