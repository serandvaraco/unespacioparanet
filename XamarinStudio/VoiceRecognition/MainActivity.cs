using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Speech;
using Android.Views;
using Android.Widget;
using Android.Util;


namespace VoiceRecognition
{
    [Activity(Label = "VoiceRecognition", MainLauncher = true, Icon = "@drawable/icon")]
    public class VoiceRecognitionActivity : Activity
    {
        private static String TAG = "VoiceRecognition";
        private const int VOICE_RECOGNITION_REQUEST_CODE = 1234;
        private ListView mList;
        public Handler mHandler;
        private Spinner mSupportedLanguageView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            mHandler = new Handler();
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.voicerecognition);

            // Get our button from the layout resource,
            // and attach an event to it
            Button speakButton = FindViewById<Button>(Resource.Id.btn_speak);
            mList = FindViewById<ListView>(Resource.Id.list);

            mSupportedLanguageView = FindViewById<Spinner>(Resource.Id.supported_languages);

            // Check to see if a recognition activity is present
            PackageManager pm = PackageManager;
            IList<ResolveInfo> activities = pm.QueryIntentActivities(new Intent(RecognizerIntent.ActionRecognizeSpeech), 0);

            if (activities.Count != 0)
                speakButton.Click += speakButton_Click;
            else
            {
                speakButton.Enabled = false;
                speakButton.Text = "No presente o no reconicido";
            }

            RefreshVoiceSettings();
        }

        private void speakButton_Click(object sender, EventArgs e)
        {
            View v = (View)sender;

            if (v.Id == Resource.Id.btn_speak)
                StartVoiceRecognitionActivity();
        }

        private void StartVoiceRecognitionActivity()
        {
            Intent intent = new Intent(RecognizerIntent.ActionRecognizeSpeech);

            // Specify the calling package to identify your application
            intent.PutExtra(RecognizerIntent.ExtraCallingPackage, PackageName);

            // Display an hint to the user about what he should say.
            intent.PutExtra(RecognizerIntent.ExtraPrompt, "Reconicmiento de Voz Demo");

            // Given an hint to the recognizer about what the user is going to say
            intent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

            // Specify how many results you want to receive. The results will be sorted
            // where the first result is the one with higher confidence.
            intent.PutExtra(RecognizerIntent.ExtraMaxResults, 5);

            // Specify the recognition language. This parameter has to be specified only if the
            // recognition has to be done in a specific language and not the default one (i.e., the
            // system locale). Most of the applications do not have to set this parameter.
            if (mSupportedLanguageView.SelectedItem != null && mSupportedLanguageView.SelectedItem.ToString() != "Default")
            {
                intent.PutExtra(RecognizerIntent.ExtraLanguage,
                                mSupportedLanguageView.SelectedItem.ToString());
            }

            StartActivityForResult(intent, VOICE_RECOGNITION_REQUEST_CODE);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == VOICE_RECOGNITION_REQUEST_CODE && resultCode == Result.Ok)
            {
                // Fill the list view with the strings the recognizer thought it could have heard
                IList<String> matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                mList.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, matches);
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }

        void RefreshVoiceSettings()
        {
            Log.Info(TAG, "Sending broadcast");
            SendOrderedBroadcast(RecognizerIntent.GetVoiceDetailsIntent(this), null,
                                 new SupportedLanguageBroadcastReceiver(this), null, Result.Ok, null, null);
        }

        public void UpdateSupportedLanguages(IList<String> languages)
        {
            // We add "Default" at the beginning of the list to simulate default language.
            languages.Add("Default");

            var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, languages);
            mSupportedLanguageView.Adapter = adapter;
        }

        public void UpdateLanguagePreference(String language)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.language_preference);
            textView.Text = language;
        }

        /**
     	* Handles the response of the broadcast request about the recognizer supported languages.
     	*
     	* The receiver is required only if the application wants to do recognition in a specific
     	* language.
     	*/
        class SupportedLanguageBroadcastReceiver : BroadcastReceiver
        {
            VoiceRecognitionActivity self;

            public SupportedLanguageBroadcastReceiver(VoiceRecognitionActivity s)
            {
                self = s;
            }

            public override void OnReceive(Context context, Intent intent)
            {
                Log.Info(TAG, "Receiving broadcast " + intent);

                Bundle extra = GetResultExtras(false);

                if (ResultCode != Result.Ok)
                {
                    self.mHandler.Post(() =>
                    {
                        Toast.MakeText(self, "Error code:" + ResultCode, ToastLength.Short).Show();
                    });
                }

                if (extra == null)
                {
                    self.mHandler.Post(() =>
                    {
                        Toast.MakeText(self, "No extra", ToastLength.Short).Show();
                    });
                }

                else if (extra.ContainsKey(RecognizerIntent.ExtraSupportedLanguages))
                {
                    self.mHandler.Post(() =>
                    {
                        self.UpdateSupportedLanguages(extra.GetStringArrayList(RecognizerIntent.ExtraSupportedLanguages));
                    });
                }

                else if (extra.ContainsKey(RecognizerIntent.ExtraLanguagePreference))
                {
                    self.mHandler.Post(() =>
                    {
                        self.UpdateLanguagePreference(extra.GetString(RecognizerIntent.ExtraLanguagePreference));
                    });
                }
            }
        }
    }
}

