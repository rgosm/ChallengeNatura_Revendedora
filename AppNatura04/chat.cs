using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppNaturaRevendedora {
    [Activity(Label = "chat")]
    public class Chat : AppCompatActivity {

        private readonly string nome = "teste da silva";
        private readonly string email = "teste.silva@fiap.com.br";
        private readonly string cel = "(11)99999-9999";

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.chat);

            WebView webView = FindViewById<WebView>(Resource.Id.webview);
            FloatingActionButton BtnHide = FindViewById<FloatingActionButton>(Resource.Id.btnHide);
            FloatingActionButton BtnShow = FindViewById<FloatingActionButton>(Resource.Id.btnShow);
            FloatingActionButton BtnClose = FindViewById<FloatingActionButton>(Resource.Id.btnClose);
            ImageView ImgShow = FindViewById<ImageView>(Resource.Id.imgWW);
            webView.Settings.JavaScriptEnabled = true;
            webView.Settings.DomStorageEnabled = true;
            webView.SetWebViewClient(new HelloWebViewClient());
            webView.LoadUrl("https://mdh-chat.metasix.solutions/livechat?mode=popout");
            _ = DelayAction(7000);

            async Task DelayAction(int delay) {
                await Task.Delay(delay);
                webView.EvaluateJavascript($"document.getElementById('guestName').value = '{nome}'", null);
                webView.EvaluateJavascript($"document.getElementById('guestEmail').value = '{email}'", null);
                webView.EvaluateJavascript($"document.getElementById('guestPhone').value = '{cel}'", null);
            }

            BtnHide.Click += (o, e) => {
                webView.Visibility = ViewStates.Invisible;
                BtnHide.Visibility = ViewStates.Invisible;
                BtnShow.Visibility = ViewStates.Visible;
                ImgShow.Visibility = ViewStates.Visible;
            };

            BtnShow.Click += (o, e) => {
                webView.Visibility = ViewStates.Visible;
                BtnHide.Visibility = ViewStates.Visible;
                BtnShow.Visibility = ViewStates.Invisible;
                ImgShow.Visibility = ViewStates.Invisible;
            };

            BtnClose.Click += (o, e) => {
                Finish();
                Accelerometer.ShakeDetected -= new MainActivity().ShakeDetected;
                new MainActivity().ToggleAccelerometer();
            };
        }


        private void HideOnClick(object sender, EventArgs eventArgs) {

        }

        public override bool OnCreateOptionsMenu(IMenu menu) {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item) {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings) {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            Finish();
            Accelerometer.ShakeDetected -= new MainActivity().ShakeDetected;
            new MainActivity().ToggleAccelerometer();
        }
    }
}