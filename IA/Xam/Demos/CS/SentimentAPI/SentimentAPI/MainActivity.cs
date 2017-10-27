using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SentimentAPI.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SentimentAPI
{
    [Activity(Label = "SentimentAPI", MainLauncher = true)]
    public class MainActivity : Activity
    {
        EditText txtInput;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            Button btnProcess = FindViewById<Button>(Resource.Id.btnProcess);
            txtInput = FindViewById<EditText>(Resource.Id.inputtext);

            btnProcess.Click += delegate
            {
                var textinput = txtInput.Text;
                new SentimentTask(this).Execute(textinput);
            };

        }

        class SentimentTask : AsyncTask<string, string, string>
        {
            private MainActivity mainActivity;
            private SentimentClient sentimentClient = new SentimentClient();
            private ProgressDialog pd = new ProgressDialog(Application.Context);

            public SentimentTask(MainActivity mainActivity)
            {
                this.mainActivity = mainActivity;
            }


            protected override string RunInBackground(params string[] @params)
            {
                PublishProgress("Analizing... ");
                return Task.Run<string>(async () =>
                {
                    SentimentModel sentimentModel = await sentimentClient.GetSentimentAsync(@params[0]);
                    return JsonConvert.SerializeObject(sentimentModel);
                }).Result;
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

                SentimentModel sentimentModel = JsonConvert.DeserializeObject<SentimentModel>(result);
                if (!sentimentModel.documents.Any())
                    throw new Exception();

                var score = Math.Round(sentimentModel.documents.ToList()[0].score * 100, 0);
                string scoreSentiment = $" sentiment {score} %";
                mainActivity.txtInput.Text = scoreSentiment;
            }
        }

    }


}

