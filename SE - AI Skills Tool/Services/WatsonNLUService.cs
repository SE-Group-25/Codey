using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.NaturalLanguageUnderstanding.v1.Model;
using IBM.Watson.NaturalLanguageUnderstanding.v1;
using System;
using System.Collections.Generic;


namespace SE_AI_Skills_Tool.Services
{
    public interface IWatsonNLUService
    {
    }

    public class WatsonNLUService: IWatsonNLUService
    {
        private readonly string apikey = "P-oZ0Z5vSY16EUzlNvDBjh8LwMrsbLLtKeQ_wrvlgQG0";
        private readonly string url = "https://api.eu-gb.natural-language-understanding.watson.cloud.ibm.com/instances/531dc7fd-26ae-433f-93df-65321656e8bd";
        private readonly string versionDate = "2022-06-01";

        private string text =
            "Analyze various features of text content at scale. Provide text, raw HTML, or a public URL, and IBM Watson Natural Language Understanding will give you results for the features you request. The service cleans HTML content before analysis by default, so the results can ignore most advertisements and other unwanted content.";

        private string modelId;

        public void Analyze()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                                                                  apikey: $"{apikey}"
                                                                 );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService($"{versionDate}", authenticator);
            service.SetServiceUrl($"{url}");

            var features = new Features()
                           {
                               Keywords = new KeywordsOptions()
                                          {
                                              Limit = 10,
                                              Sentiment = false,
                                              Emotion = false
                                          },
                               Entities = new EntitiesOptions()
                                          {
                                              Sentiment = false,
                                              Limit = 2
                                          }
                           };

            var result = service.Analyze(
                                         features: features,
                                         text: "Themes will be chosen from contemporary areas of computer vision including the following: edge features, contours and shape fitting. feature points for object detection and classification. stereo vision (3D point clouds and depth images). object classification using distributions of gradient information. background modelling and object tracking. end-to-end image classification and real-time object detection via deep machine learning. image and video mosaicking and 3D scene reconstruction. visual odometry for autonomous navigation."
                                        );

            Console.WriteLine(result.Response);
        }
    }
}
