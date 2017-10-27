using Android.App;
using Android.Widget;
using Android.OS;
using Com.Microsoft.Projectoxford.Emotion;
using Android.Graphics;
using System.IO;
using System.Collections.Generic;
using FaceEmotion.Model;
using Newtonsoft.Json;
using Android.Views;
using System;
using FaceEmotion.Helper;
using System.Linq;
using Android.Content.PM;
using Android.Runtime;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Media.Abstractions;
namespace FaceEmotion
{
    [Activity(Label = "FaceEmotion", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public EmotionServiceRestClient emotionServiceRestClient = new EmotionServiceRestClient("3a34c183d51743cbbd14683fa72c7c46");
        ImageView imageView;
        Bitmap mBitmap;

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            mBitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.angry);
            imageView = FindViewById<ImageView>(Resource.Id.ImageView);
            imageView.SetImageBitmap(mBitmap);

            Button btnProcess = FindViewById<Button>(Resource.Id.btnProcess);
            Button btnTake = FindViewById<Button>(Resource.Id.takePhoto);


            byte[] bitmapData;
            using (var stream = new MemoryStream())
            {
                mBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                bitmapData = stream.ToArray();
            }

            Stream inputStream = new MemoryStream(bitmapData);
            btnProcess.Click += delegate { new EmotionTask(this).Execute(inputStream); };

            btnTake.Click += async delegate
            {
                await CrossMedia.Current.Initialize();
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "sample",
                    Name = "test.jpg"
                });

                if (file == null) return;

                mBitmap = BitmapFactory.DecodeStream(file.GetStream());

                using (var stream = new MemoryStream())
                {
                    mBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                    bitmapData = stream.ToArray();
                }

                imageView.SetImageBitmap(mBitmap);
                inputStream = file.GetStream();

            };

        }

        class EmotionTask : AsyncTask<Stream, string, string>
        {
            private MainActivity mainActivity;
            private ProgressDialog pd = new ProgressDialog(Application.Context);
            public EmotionTask(MainActivity mainActivity)
            {
                this.mainActivity = mainActivity;
            }
            protected override string RunInBackground(params Stream[] @params)
            {
                try
                {
                    PublishProgress("Recognizing...");
                    var result = mainActivity.emotionServiceRestClient.RecognizeImage(@params[0]);
                    var list = new List<EmotionModel>();

                    foreach (var item in result)
                    {
                        Com.Microsoft.Projectoxford.Emotion.Contract.FaceRectangle faceRect = item.FaceRectangle;
                        Com.Microsoft.Projectoxford.Emotion.Contract.Scores scores = item.Scores;
                        var emotionModel = new EmotionModel
                        {
                            FaceRectangle = new FaceRectangle
                            {
                                top = faceRect.Top,
                                width = faceRect.Width,
                                height = faceRect.Height,
                                left = faceRect.Left
                            },
                            scores = new Scores
                            {
                                anger = scores.Anger,
                                contempt = scores.Contempt,
                                disgust = scores.Disgust,
                                fear = scores.Fear,
                                happiness = scores.Happiness,
                                neutral = scores.Neutral,
                                sadness = scores.Sadness,
                                surprise = scores.Surprise
                            }
                        };
                        list.Add(emotionModel);
                    }

                    string strResult = JsonConvert.SerializeObject(list);
                    return strResult;
                }
                catch (System.Exception ex)
                {
                    PublishProgress("Error: " + ex.Message);
                    return null;
                }
            }
            protected override void OnPreExecute()
            {
                pd.Window.SetType(WindowManagerTypes.SystemAlert);
                pd.Show();
            }
            protected override void OnProgressUpdate(params string[] values)
            {
                pd.SetMessage(values[0]);
            }
            protected override void OnPostExecute(string result)
            {
                pd.Dismiss();
                var list = JsonConvert.DeserializeObject<List<EmotionModel>>(result);
                foreach (var item in list)
                {
                    string status = GetEmo(item);
                    mainActivity.imageView.SetImageBitmap(ImageHelper.DrawRectOnBitmap(mainActivity.mBitmap, item.FaceRectangle, status));

                }
            }

            private string GetEmo(EmotionModel item)
            {
                List<double> list = new List<double>();
                Scores scores = item.scores;
                list.Add(scores.anger);
                list.Add(scores.contempt);
                list.Add(scores.disgust);
                list.Add(scores.fear);
                list.Add(scores.happiness);
                list.Add(scores.neutral);
                list.Add(scores.sadness);
                list.Add(scores.surprise);

                var listSorted = list.OrderBy(i => i).ToList();
                double maxElementInList = listSorted[listSorted.Count - 1];
                if (maxElementInList == scores.anger)
                    return "Anger";
                if (maxElementInList == scores.contempt)
                    return "Contemp";
                if (maxElementInList == scores.disgust)
                    return "Disgust";
                if (maxElementInList == scores.fear)
                    return "Fear";
                if (maxElementInList == scores.happiness)
                    return "Happy";
                if (maxElementInList == scores.sadness)
                    return "Sadness";
                if (maxElementInList == scores.surprise)
                    return "Surprise";

                return "Neutral";

            }
        }
    }
}


