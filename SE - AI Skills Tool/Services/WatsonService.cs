using IBM.Cloud.SDK.Core.Authentication.Iam;
using SE_AI_Skills_Tool.Context;
using IBM.Watson.Assistant.v2;
using IBM.Watson.Assistant.v2.Model;

namespace SE_AI_Skills_Tool.Services
{
    public interface IWatson
    {
        // Task<string> SendInputToWatson(string input);
        void Message();

        void CreateSession();

        void DeleteSession();
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
        string sessionId;
        private readonly string inputString = "Hello World!";

        // public Watson(AstDevContext astDev)
        // {
        //     _astDev = astDev;
        // }

        public void CreateSession()
        {
            IamAuthenticator authenticator = new IamAuthenticator(apikey: $"{apikey}");

            AssistantService service = new AssistantService($"{versionDate}", authenticator);
            service.SetServiceUrl($"{url}");

            var result = service.CreateSession(
                                               assistantId: $"{environmentId}"
                                              );

            Console.WriteLine(result.Response);

            sessionId = result.Result.SessionId;
        }

        public void DeleteSession()
        {
            IamAuthenticator authenticator = new IamAuthenticator(apikey: $"{apikey}");

            AssistantService service = new AssistantService($"{versionDate}", authenticator);
            service.SetServiceUrl($"{url}");

            var result = service.DeleteSession(
                                               assistantId: $"{environmentId}",
                                               sessionId: $"{sessionId}"
                                              );
        }

        public void Message()
        {
            IamAuthenticator authenticator = new IamAuthenticator(apikey: $"{apikey}");

            AssistantService service = new AssistantService($"{versionDate}", authenticator);
            service.SetServiceUrl($"{url}");

            var result = service.Message(
                                         assistantId: $"{environmentId}",
                                         sessionId: $"{sessionId}",
                                         input: new MessageInput()
                                                {
                                                    Text = "What courses does the IBM Skills build provide"
                                                });
            Console.WriteLine(result.Response);
        }


        // public async Task<string> SendInputToWatson(string input)
        // {
        //     IamAuthenticator authenticator = new IamAuthenticator(apikey: "{apikey}");
        //
        //     AssistantService assistant = new AssistantService("{version}", authenticator);
        //     assistant.SetServiceUrl("https://api.au-syd.assistant.watson.cloud.ibm.com/instances/dfad6e7a-2714-4f7f-ba32-4537dd5370f8");
        //
        //     MessageInput messageInput = new MessageInput()
        //                          {
        //                              Text = "Hello"
        //                          };
        //
        //     MessageResponse response = assistant.Message("14fdb056-2832-4a06-9169-4fe7179a08cc", messageInput);
        // }
    }
}
