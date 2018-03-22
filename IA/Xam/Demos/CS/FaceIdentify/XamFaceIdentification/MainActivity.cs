using Android.App;
using Android.Widget;
using Android.OS;
using Com.Microsoft.Projectoxford.Face;
using Android.Graphics;
using System.Collections.Generic;
using XamFaceIdentification.Model;
using System.IO;
using GoogleGson;
using Newtonsoft.Json;
using Java.Util;
using XamFaceIdentification.Helper;
using Android.Views;
using Plugin.Media;
using Plugin.Permissions;
using Android.Content.PM;
using Android.Runtime;

namespace XamFaceIdentification
{
    [Activity(Label = "Reconocer Rostros", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private FaceServiceRestClient faceServiceRestClient = new FaceServiceRestClient("https://eastus.api.cognitive.microsoft.com/face/v1.0", "e673746d753d4db6940d0d58f98e4a27");
        private string personGroupId = "bigview";
        ImageView imageView;
        Bitmap mBitmap;
        Button btnDetect, btnIdentify, btnTake;
        List<FaceModel> facesDetected = new List<FaceModel>();
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        => PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            mBitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.scarlet);
            imageView = FindViewById<ImageView>(Resource.Id.imageView);
            imageView.SetImageBitmap(mBitmap);

            btnDetect = FindViewById<Button>(Resource.Id.btnDetect);
            btnIdentify = FindViewById<Button>(Resource.Id.btnIdentify);
            btnTake = FindViewById<Button>(Resource.Id.btnTake);



            btnTake.Click += async delegate
            {
                await CrossMedia.Current.Initialize();
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "sample",
                    Name = "test.jpg"
                });

                if (file == null)
                    return;

                mBitmap = BitmapFactory.DecodeStream(file.GetStream());
                imageView.SetImageBitmap(mBitmap);

            };
            btnDetect.Click += delegate
            {
                byte[] bitmapData;
                using (var stream = new MemoryStream())
                {
                    mBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                    bitmapData = stream.ToArray();
                }

                var inputStream = new MemoryStream(bitmapData);
                new DetectTask(this).Execute(inputStream);
            };

            btnIdentify.Click += delegate
            {
                string[] facesID = new string[facesDetected.Count];
                for (int i = 0; i < facesDetected.Count; i++)
                    facesID[i] = facesDetected[i].faceId;

                new IdentificationTask(this, personGroupId).Execute(facesID);
            };

        }
        class IdentificationTask : AsyncTask<string, string, string>
        {
            private MainActivity mainActivity;
            private string personGroupId;
            private ProgressDialog mDialog = new ProgressDialog(Application.Context);
            public IdentificationTask(MainActivity mainActivity, string personGroupId)
            {
                this.mainActivity = mainActivity;
                this.personGroupId = personGroupId;
            }

            protected override string RunInBackground(params string[] @params)
            {
                try
                {
                    PublishProgress("Identifying...");

                    UUID[] uuidList = new UUID[@params.Length];
                    for (int i = 0; i < @params.Length; i++)
                        uuidList[i] = UUID.FromString(@params[i]);

                    var result = mainActivity.faceServiceRestClient.Identity(personGroupId
                        , uuidList
                        , 1); // max number of candidates returned 

                    Gson gson = new Gson();
                    var resultString = gson.ToJson(result);
                    return resultString;

                }
                catch (System.Exception)
                {
                    return null;
                }
            }
            protected override void OnPreExecute()
            {
                mDialog.Window.SetType(WindowManagerTypes.SystemAlert);
                mDialog.Show();
            }
            protected override void OnProgressUpdate(params string[] values)
            {
                mDialog.SetMessage(values[0]);
            }
            protected override void OnPostExecute(string result)
            {
                mDialog.Dismiss();
              

                var identifyList = JsonConvert.DeserializeObject<List<IdentifyResultModel>>(result);
                foreach (var identify in identifyList)
                {
                    if (identify.candidates.Count == 0)
                    {
                        Toast.MakeText(mainActivity.ApplicationContext, "No one detected", ToastLength.Long).Show();
                        continue;
                    }
                    else
                    {
                        var candidate = identify.candidates[0];
                        var personId = candidate.personId;
                        new PersonDetectionTask(mainActivity, personGroupId).Execute(personId);
                    }

                }
            }


        }
        class PersonDetectionTask : AsyncTask<string, string, string>
        {
            private MainActivity mainActivity;
            private string personGroupId;
            private ProgressDialog mDialog = new ProgressDialog(Application.Context);

            public PersonDetectionTask(MainActivity mainActivity, string personGroupId)
            {
                this.mainActivity = mainActivity;
                this.personGroupId = personGroupId;
            }

            protected override string RunInBackground(params string[] @params)
            {
                PublishProgress("Getting person...");
                UUID uuid = UUID.FromString(@params[0]);

                var person = mainActivity.faceServiceRestClient.GetPerson(personGroupId, uuid);
                Gson gson = new Gson();
                var result = gson.ToJson(person);
                return result;
            }
            protected override void OnPreExecute()
            {
                mDialog.Window.SetType(WindowManagerTypes.SystemAlert);
                mDialog.Show();
            }
            protected override void OnProgressUpdate(params string[] values)
            {
                mDialog.SetMessage(values[0]);
            }

            protected override void OnPostExecute(string result)
            {
                mDialog.Dismiss();
                var person = JsonConvert.DeserializeObject<PersonModel>(result);
                mainActivity.imageView.SetImageBitmap(
                    DrawHelper.DrawRectangleOnBitmap(mainActivity.mBitmap,
                     mainActivity.facesDetected,
                     person.name));

            }
        }
        class DetectTask : AsyncTask<Stream, string, string>
        {
            private MainActivity mainActivity;
            private ProgressDialog mDialog = new ProgressDialog(Application.Context);
            public DetectTask(MainActivity mainActivity)
            {
                this.mainActivity = mainActivity;
            }
            protected override string RunInBackground(params Stream[] @params)
            {
                PublishProgress("Detecting...");
                var result = mainActivity.faceServiceRestClient.Detect(@params[0], true, false, null);
                if (result == null)
                {
                    PublishProgress("Detection Finished. Nothing detected");
                    return null;
                }
                PublishProgress($"Detection Finished. {result.Length} face(s) detected");

                Gson gson = new Gson();
                var stringResult = gson.ToJson(result);
                return stringResult;
            }
            protected override void OnPreExecute()
            {
                mDialog.Window.SetType(WindowManagerTypes.SystemAlert);
                mDialog.Show();
            }
            protected override void OnPostExecute(string result)
            {
                mDialog.Dismiss();
                var faces = JsonConvert.DeserializeObject<List<FaceModel>>(result);
                mainActivity.facesDetected = faces;

                //when detected face, set enable  for identify button 
                if (mainActivity.facesDetected.Count != 0)
                    mainActivity.btnIdentify.Enabled = true;
            }
            protected override void OnProgressUpdate(params string[] values)
            {
                mDialog.SetMessage(values[0]);
            }

        }
    }


}

