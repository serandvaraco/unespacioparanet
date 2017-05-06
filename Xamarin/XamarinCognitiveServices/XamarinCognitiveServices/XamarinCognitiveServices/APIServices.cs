
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

#region Nuget's Requeridos
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Vision.Contract;
using Microsoft.ProjectOxford.Vision;
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

            EmotionServiceClient clientEmotion = new EmotionServiceClient("cc6f615ca9ad42e0b4df4f78718adbbd");
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
            VisionServiceClient client =
                new VisionServiceClient("a051bcb1c4864f6188409e99010e177b");
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
