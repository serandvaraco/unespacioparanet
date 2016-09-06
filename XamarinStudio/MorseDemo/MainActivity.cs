using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Media;

namespace MorseDemo
{
    [Activity(Label = "MorseDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private TextView mTextView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.MorseCode);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.button);
            mTextView = (TextView)FindViewById(Resource.Id.text);

            button.Click += delegate
            {
                // Get the text out of the view
                String text = mTextView.Text.ToString();

                // convert it using the function defined above.  See the docs for
                // android.os.Vibrator for more info about the format of this array
                long[] pattern = MorseCodeConverter.GetPattern(text);

                // Start the vibration
                Vibrator vibrator = (Vibrator)GetSystemService(Context.VibratorService);
                vibrator.Vibrate(pattern, -1);
                //Start the Tone
                ToneGenerator toneGen1 = new ToneGenerator(Android.Media.Stream.Music, Volume.Max);
                foreach (var item in pattern)
                {
                    if (item > 0)
                        toneGen1.StartTone(Tone.CdmaAbbrAlert, (int)item);

                    System.Threading.Thread.Sleep(180);
                }

            };
        }
    }
}

