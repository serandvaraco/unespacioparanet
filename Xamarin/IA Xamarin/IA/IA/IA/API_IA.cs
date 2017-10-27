using Microsoft.ProjectOxford.Common;
using Microsoft.ProjectOxford.Common.Contract;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IA
{

    public class Emotion : Microsoft.ProjectOxford.Common.Contract.Emotion

    {

        public new Scores Scores
        {
            get
            {
                return (Scores)base.Scores;
            }
        }
    }





    public class API_IA
    {
        public static async Task<MediaFile> TakePhoto(bool useCamera)
        {
            await Plugin.Media.CrossMedia.Current.Initialize();
            if (useCamera)
                if (!CrossMedia.Current.IsCameraAvailable ||
                    !CrossMedia.Current.IsTakePhotoSupported)
                    return null;

            string nameSecuenceFile = new Random(DateTime.Now.Millisecond).Next(100, 999).ToString();
            var file = useCamera ? await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "FolderImagesCognitive",
                        Name = $"TestImages-{nameSecuenceFile}.jpg",
                        AllowCropping = true,
                        CompressionQuality = 50,
                        CustomPhotoSize = 50,
                        PhotoSize = PhotoSize.Small

                    })
                    : await CrossMedia.Current.PickPhotoAsync();

            return file;
        }



        public static async Task<Dictionary<string, float>> GetEmotionsAPI(System.IO.Stream stream)
        {

            EmotionServiceClient clientEmotion = new EmotionServiceClient("3a34c183d51743cbbd14683fa72c7c46", "https://westus.api.cognitive.microsoft.com/emotion/v1.0");
            var emotion = await clientEmotion.RecognizeAsync(stream);

            if (emotion == null || emotion.Count() == 0)
                return null;
            return emotion[0].Scores
                            .ToRankedList()
                            .ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Obtiene reconocimiento de imagenes usando CognitiveServices
        /// </summary>
        /// <param name="stream">Recibe Foto o Video</param>
        /// <returns>Obtiene resultado del análisis </returns>
        public static AnalysisResult GetAnalisysComputerVisio(System.IO.Stream stream)
        {
            VisionServiceClient client =
                new VisionServiceClient("1f7cabd9c33e48218730596cdf035669", "https://eastus.api.cognitive.microsoft.com/vision/v1.0");
            VisualFeature[] features =
                    {
                      VisualFeature.Adult,
                      VisualFeature.Faces,
                      VisualFeature.Color,
                      VisualFeature.Description,
                      VisualFeature.Tags,
                      VisualFeature.ImageType,
                      VisualFeature.Categories
            };

            return client.AnalyzeImageAsync(stream, features).Result;
        }

        public static string GetEmotions(System.IO.Stream stream)
        {
            var client = new HttpClient();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "3a34c183d51743cbbd14683fa72c7c46");

            string uri = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize?";
            HttpResponseMessage response;
            string responseContent = string.Empty;


            using (var content = new StreamContent(stream))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                response = client.PostAsync(uri, content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public static string GetVisionJson(System.IO.Stream stream)
        {

            try
            {
                var client = new HttpClient();

                // Request headers - replace this example key with your valid subscription key.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "1f7cabd9c33e48218730596cdf035669");

                // Request parameters. A third optional parameter is "details".
                string requestParameters = "visualFeatures=Categories,Description,Color&language=en";
                string uri = "https://eastus.api.cognitive.microsoft.com/vision/v1.0/analyze?" + requestParameters;
                Console.WriteLine(uri);

                HttpResponseMessage response;

                var content = new StreamContent(stream);

                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = client.PostAsync(uri, content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message);
                return ex.Message;
            }

        }
    }
}
