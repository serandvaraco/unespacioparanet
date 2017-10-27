using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFaceApiIdentification
{
    class Program
    {
        FaceServiceClient faceServiceClient = new FaceServiceClient("cecb2840aa6e4a3e85ec2a5f543bc474", "https://eastus.api.cognitive.microsoft.com/face/v1.0");

        public async Task CreatePersonGroup(string personGroupId, string personGroupName)
        {
            try
            {
                await faceServiceClient.CreatePersonGroupAsync(personGroupId, personGroupName);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error create Person Group\n " + ex.Message);
            }
        }

        public async Task AddPersonToGroup(string personGroupId, string name, string pathImage)
        {
            try
            {
                await faceServiceClient.GetPersonGroupAsync(personGroupId).ContinueWith(async (x) =>
                {
                    CreatePersonResult person = await faceServiceClient.CreatePersonAsync(personGroupId, name);
                    await DetectFaceAndRegister(personGroupId, person, pathImage);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error add Person to Group\n " + ex.Message);
            }
        }

        private async Task DetectFaceAndRegister(string personGroupId, CreatePersonResult person, string pathImage)
        {
            foreach (var imgPath in Directory.GetFiles(pathImage, "*.jpg"))
            {
                using (Stream s = File.OpenRead(imgPath))
                {
                    await faceServiceClient.AddPersonFaceAsync(personGroupId, person.PersonId, s);
                }
            }
        }

        public async Task RecognitionFace(string personGroupId, string imgPath)
        {
            using (Stream s = File.OpenRead(imgPath))
            {
                await faceServiceClient.DetectAsync(s).ContinueWith(async (x) =>
                {

                    var faces = await x;

                    var faceids = faces.Select(f => f.FaceId).ToArray();

                    try
                    {
                        await faceServiceClient.IdentifyAsync(personGroupId, faceids).ContinueWith(async (y) =>
                        {
                            try
                            {
                                var results = await y;

                                foreach (var item in results)
                                {
                                    Console.WriteLine($"Result of face: { item.FaceId }");
                                    if (item.Candidates.Length == 0)
                                        Console.WriteLine("Not identified!!");
                                    else
                                    {
                                        var candidateId = item.Candidates[0].PersonId;
                                        await faceServiceClient.GetPersonAsync(personGroupId, candidateId).ContinueWith(async (z) =>
                                        {
                                            var person = await z;
                                            Console.WriteLine($"Identified as {person.Name}");

                                        });
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        });

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                });

            }
        }

        public async Task TrainingAI(string personGroupId)
        {
            await faceServiceClient.TrainPersonGroupAsync(personGroupId);
            TrainingStatus trainingStatus = null;
            while (true)
            {
                trainingStatus = await faceServiceClient.GetPersonGroupTrainingStatusAsync(personGroupId);
                if (trainingStatus.Status != Status.Running)
                    break;
                await Task.Delay(1000);
            }

            Console.WriteLine("TRaining AI completed");
        }

        static async void Evaluate()
        {

            //
            var p = new Program();
            /*
              new Program().CreatePersonGroup("serandvaraco", "Hollywood Star");
              await p.AddPersonToGroup("serandvaraco", "Tom Cruise", @"C:\Git\unespacioparanet\Xamarin\IA Xamarin Conference\Demos\CognitiveService\Xamarin Face Identity\Images\TomCruise\")
                .ContinueWith(async (x) =>
                {
                    await p.AddPersonToGroup("serandvaraco", "Natalie Rushman", @"C:\Git\unespacioparanet\Xamarin\IA Xamarin Conference\Demos\CognitiveService\Xamarin Face Identity\Images\Natalie Rushman\");
                }).ContinueWith(async (x) =>
                {
                    await p.AddPersonToGroup("serandvaraco", "Robert Downey Jr", @"C:\Git\unespacioparanet\Xamarin\IA Xamarin Conference\Demos\CognitiveService\Xamarin Face Identity\Images\Robert\");
                }).ContinueWith(async (x) =>
                {
                    await p.AddPersonToGroup("serandvaraco", "Scarlett Johansson", @"C:\Git\unespacioparanet\Xamarin\IA Xamarin Conference\Demos\CognitiveService\Xamarin Face Identity\Images\scarlett johansson\");
                }).ContinueWith(async (x) =>
                {
                    await p.TrainingAI("serandvaraco");
                });
            */
           // await p.AddPersonToGroup("serandvaraco", "Sergio A Vargas", @"C:\Git\unespacioparanet\Xamarin\IA Xamarin Conference\Demos\CognitiveService\Xamarin Face Identity\Images\SergioVargas\"); 
            string testImageFile = @"C:\Git\unespacioparanet\Xamarin\IA Xamarin Conference\Demos\CognitiveService\Xamarin Face Identity\Images\SergioVargas\Sergio1.jpg";
            await p.TrainingAI("serandvaraco").ContinueWith(async (x) =>
            {
                await p.RecognitionFace("serandvaraco", testImageFile);
            });

        }

        static void Main(string[] args)
        {
            Evaluate();
            Console.ReadKey();
        }
    }
}
