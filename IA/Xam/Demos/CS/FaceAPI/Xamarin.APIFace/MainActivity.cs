using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Com.Microsoft.Projectoxford.Face;
using GoogleGson;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Xamarin.APIFace.Resources.Model;
using Android.Content.PM;
using Android.Runtime;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Media.Abstractions;

namespace Xamarin.APIFace
{
    /*Theme = "@style/Theme.AppCompat.Light.NoActionBar"*/
    [Activity(Label = "Xamarin.APIFace", MainLauncher = true)]
    public class MainActivity : Activity
    {

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        => PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        public ImageView imageView;
        public Bitmap mBitmap;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            var btnProcess = FindViewById<Button>(Resource.Id.btnProcess);
            imageView = FindViewById<ImageView>(Resource.Id.ImageView);

            var btnTakePhoto = FindViewById<Button>(Resource.Id.takePhoto);

            mBitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.Sergio);
            imageView.SetImageBitmap(mBitmap);

            btnProcess.Click += delegate
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

            btnTakePhoto.Click += async delegate
            {
                byte[] bitmapData;
                

                
                await CrossMedia.Current.Initialize();
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "sample",
                    Name= "test.jpg"
                });

                if (file == null) return; 

                mBitmap = BitmapFactory.DecodeStream(file.GetStream());
                
                using (var stream = new MemoryStream())
                {
                    mBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                    bitmapData = stream.ToArray();
                }

                var inputStream = new MemoryStream(bitmapData);
                new DetectTask(this).Execute(inputStream);
            };
        }
        class DetectTask : AsyncTask<Stream, string, string>
        {
            private MainActivity mainActivity;
            private ProgressDialog pd = new ProgressDialog(Application.Context);
            public DetectTask(MainActivity mainActivity)
            {
                this.mainActivity = mainActivity;
            }

            protected override string RunInBackground(params Stream[] @params)
            {
                PublishProgress("Detecting... ");
                var faceServiceClient = new FaceServiceRestClient("https://eastus.api.cognitive.microsoft.com/face/v1.0", "cecb2840aa6e4a3e85ec2a5f543bc474");
                Com.Microsoft.Projectoxford.Face.Contract.Face[] result = faceServiceClient.Detect(@params[0],
                    true,  //FaceId
                    false,  // Face Landmarks 
                    null); //Face Attributes 

                if (result == null)
                {
                    PublishProgress("Detection Finished. Nothing detected");
                    return null;
                }

                PublishProgress($"Detection Finished. {result.Length} face(s) detected");

                Gson gson = new Gson();
                var strResult = gson.ToJson(result);

                return strResult;
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
                var faces = JsonConvert.DeserializeObject<List<FaceModel>>(result);
                var bitmap = DrawRectanglesOnBitmap(mainActivity.mBitmap, faces);
                mainActivity.imageView.SetImageBitmap(bitmap);
                pd.Dismiss();
            }

            private Bitmap DrawRectanglesOnBitmap(Bitmap mBitmap, List<FaceModel> faces)
            {
                Bitmap bitmap = mBitmap.Copy(Bitmap.Config.Argb8888, true);
                Canvas canvas = new Canvas(bitmap);
                Paint paint = new Paint
                {
                    AntiAlias = true
                };
                paint.SetStyle(Paint.Style.Stroke);
                paint.Color = Color.White;
                paint.StrokeWidth = 12;

                foreach (var face in faces)
                {
                    var faceRectangle = face.faceRectangle;
                    canvas.DrawRect(faceRectangle.left,
                        faceRectangle.top,
                        faceRectangle.left + faceRectangle.width,
                        faceRectangle.top + faceRectangle.height,
                        paint);
                }
                return bitmap;

            }
        }

    }


}

