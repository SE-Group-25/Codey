using IBM.Cloud.SDK.Core.Authentication.Iam;
using SE_AI_Skills_Tool.Context;
using IBM.Watson.Assistant.v2;
using IBM.Watson.Assistant.v2.Model;

namespace SE_AI_Skills_Tool.Services
{
    public interface IWatson
    {
        string Message(string msgString, string sessionId);

        string CreateSession();

        void DeleteSession(string sessionId);
    }

    public class Watson: IWatson
    {
        private readonly AstDevContext _astDev;

        // TODO: Watson API calls and handling returned values from watson. Ensure async running

        private readonly string apikey = "JdmrMV_bf1p5RBwUFvmHYfC8dQzI8GNC7bUR4RRtBr_s";
        private readonly string url = "https://api.au-syd.assistant.watson.cloud.ibm.com/instances/dfad6e7a-2714-4f7f-ba32-4537dd5370f8";
        private readonly string versionDate = "2022-06-01";
        private readonly string assistantId = "14fdb056-2832-4a06-9169-4fe7179a08cc";
        private readonly string environmentId = "c98904e6-021b-4e21-9c66-15b4c5c41ab4";

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

        public string Message(string msgString, string sessionId)
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
            Console.WriteLine(result.Response);
            return result.Response;
        }
    }
}
