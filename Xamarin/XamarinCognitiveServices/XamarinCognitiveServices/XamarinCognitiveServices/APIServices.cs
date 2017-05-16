
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

#region Nuget's Requeridos
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Vision.Contract;
using Microsoft.ProjectOxford.Vision;

using System.Net.Http;
using System.Net.Http.Headers;

//Xam.Plugin.Media
using Plugin.Media.Abstractions;
using Plugin.Media;
#endregion 

namespace XamarinCognitiveServices
{
    /// <summary>
    /// Permite realizar la comunicación con las api's de servicios Cognitivos
    /// </summary>
    public class APIServices
    {
        /// <summary>
        /// Permite analizar las emociones 
        /// </summary>
        /// <param name="stream">Recibe Imagen o Video </param>
        /// <returns>Emociones obtenidas </returns>
        public static async Task<Dictionary<string, float>>
            GetEmotions(System.IO.Stream stream)
        {

            EmotionServiceClient clientEmotion = new EmotionServiceClient("eb307cf2c61642108bda65262460f267");
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
        public static async Task<AnalysisResult>
            GetAnalisysComputerVisio(System.IO.Stream stream)
        {
            try
            {
                
                VisionServiceClient client =
                    new VisionServiceClient("0887cfc2b89c45f5b100992644dbfa11");
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

                return await client.AnalyzeImageAsync(stream, features);
                /*
                var client = new HttpClient();

                // Request headers - replace this example key with your valid subscription key.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "08bed951118543b9b2d96714ec42c20c");

                // Request parameters. A third optional parameter is "details".
                string requestParameters = "visualFeatures=Categories&language=en";
                string uri = "https://westus.api.cognitive.microsoft.com/vision/v1.0/analyze?" + requestParameters;
                Console.WriteLine(uri);

                HttpResponseMessage response;

                using (var content = new ByteArrayContent(stream))
                {
                    // This example uses content type "application/octet-stream".
                    // The other content types you can use are "application/json" and "multipart/form-data".
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response = await client.PostAsync(uri, content);
                    string contentString = await response.Content.ReadAsStringAsync();
                    JsonConverter
                }*/
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //xam.Plugin.Media
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
                        Directory = "FolderImagesCognitive"
                    ,
                        Name = $"TestImages-{nameSecuenceFile}.jpg"
                    })
                    : await CrossMedia.Current.PickPhotoAsync();

            return file;
        }

    }
}
