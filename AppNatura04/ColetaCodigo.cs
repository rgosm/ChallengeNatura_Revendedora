using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using Android.Gms.Vision;
using Android.Gms.Vision.Texts;
using Android.Graphics;
using Android.Support.V4.App;
using Android;
using Android.Util;
using Android.Content.PM;
using static Android.Gms.Vision.Detector;
using Java.Lang;
using Java.IO;
using System.Text.RegularExpressions;
using Android.Support.V7.Widget;
using MySql.Data.MySqlClient;
using System.IO;
using System.Linq;
using System;
using System.Data;

namespace AppNaturaRevendedora {
    [Activity(Label = "Direcione para o código do produto")]
    public class ColetaCodigo : AppCompatActivity, ISurfaceHolderCallback, IProcessor {
        private SurfaceView cameraView;
        private TextView textView;
        private CameraSource cameraSource;
        private TextView campo;
        private const int RequestCameraPermissionID = 1001;
        bool varBool = false;
        public static string codigo = null;
        public static string descricao = null;
        public static string preco = null;
        public static string quantidade = null;
        public static Bitmap imagemProduto = null;
        public static long numeroRegistro;

        public  void OnRequestPermissionsResult(int requestCode, string[] permissions, string[] grantResults) {
            switch (requestCode) {
                case RequestCameraPermissionID: {
                        cameraSource.Start(cameraView.Holder);
                    }
                break;
            }  
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.ColetaCodigoView);

            cameraView = FindViewById<SurfaceView>(Resource.Id.surface_view);
            textView = FindViewById<TextView>(Resource.Id.text_view);

            TextRecognizer textRecognizer = new TextRecognizer.Builder(ApplicationContext).Build();
            if (!textRecognizer.IsOperational)
                Log.Error("Coleta Codigo", "Detector dependencies are not yet available");
            else {
                cameraSource = new CameraSource.Builder(ApplicationContext, textRecognizer)
                    .SetFacing(CameraFacing.Back)
                    .SetRequestedPreviewSize(1280, 1024)
                    .SetRequestedFps(2.0f)
                    .SetAutoFocusEnabled(true)
                    .Build();

                cameraView.Holder.AddCallback(this);
                textRecognizer.SetProcessor(this);
            }
        }
        
        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height) {
        }

        public void SurfaceCreated(ISurfaceHolder holder) {
            if (ActivityCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission.Camera) != Android.Content.PM.Permission.Granted) {
                ActivityCompat.RequestPermissions(this, new string[] {
                    Android.Manifest.Permission.Camera
                }, RequestCameraPermissionID);
                return;
            }
            //Toast.MakeText(this, "Teste1", ToastLength.Short).Show();
            cameraSource.Start(cameraView.Holder);
        }

        public void SurfaceDestroyed(ISurfaceHolder holder) {
            cameraSource.Stop();
        }

        public void ReceiveDetections(Detections detections) {
            SparseArray items = detections.DetectedItems;
            if (items.Size() != 0) {
                textView.Post(() => {
                    StringBuilder strBuilder = new StringBuilder();
                    for (int i = 0; i < items.Size(); ++i) {
                        strBuilder.Append(((TextBlock)items.ValueAt(i)).Value);
                        strBuilder.Append("\n");
                    }

                    string source = strBuilder.ToString();

                    Regex regex = new Regex("\\((?<output>[a-zA-Z0-9]+)/*\\).*");
                    GroupCollection capturas = regex.Match(source).Groups;

                    codigo = capturas["output"].ToString();

                    if (codigo != "" && !varBool) { 
                        MySqlConnection conexao = new MySqlConnection(Conexao.strConexao);
                        MySqlCommand verificaProduto = new MySqlCommand(ComandosSQL.verificaProduto, conexao);
                        MySqlCommand contaProdutoCarrinho = new MySqlCommand(ComandosSQL.contaProdutoCarrinho, conexao);
                        MySqlCommand selecionaProdutoCarrinho = new MySqlCommand(ComandosSQL.selecionaProdutoCarrinho, conexao);
                        
                        MySqlDataReader retorno;
                        MySqlDataReader produtoCarrinho;

                        verificaProduto.Parameters.Add("@idProduto", MySqlDbType.VarChar, 60).Value = codigo.ToString();
                        
                        contaProdutoCarrinho.Parameters.Add("@idProduto", MySqlDbType.VarChar, 60).Value = codigo.ToString();
                        contaProdutoCarrinho.Parameters.Add("@idRevendedor", MySqlDbType.VarChar, 60).Value = "2";
                        contaProdutoCarrinho.Parameters.Add("@idCliente", MySqlDbType.VarChar, 60).Value = "2";
                        
                        selecionaProdutoCarrinho.Parameters.Add("@idProduto", MySqlDbType.VarChar, 60).Value = codigo.ToString();
                        selecionaProdutoCarrinho.Parameters.Add("@idRevendedor", MySqlDbType.VarChar, 60).Value = "2";

                        conexao.Open();
                        contaProdutoCarrinho.CommandType = CommandType.Text;

                        numeroRegistro = (long)contaProdutoCarrinho.ExecuteScalar();
                        contaProdutoCarrinho.Dispose();

                        if (numeroRegistro > 0) {
                            conexao.Close();
                            Toast.MakeText(Application.Context, "Este produto já está em seu carrinho.", ToastLength.Long).Show();

                            conexao.Open();
                            produtoCarrinho = selecionaProdutoCarrinho.ExecuteReader();

                            if (produtoCarrinho.Read()) {
                                byte[] imagem = (byte[])(produtoCarrinho["imagem"]);
                                imagemProduto = BitmapFactory.DecodeByteArray(imagem, 0, imagem.Length);
                                descricao = produtoCarrinho.GetString("descricao").ToString();
                                preco = produtoCarrinho.GetString("preco").ToString();
                                quantidade = produtoCarrinho.GetString("quantidade").ToString();
                                StartActivity(typeof(ExibeProduto));
                                Finish();
                            }

                        } else {
                            conexao.Close();
                            
                            conexao.Open();
                            retorno = verificaProduto.ExecuteReader();
                            
                            if (retorno.Read()) {
                                byte[] imagem = (byte[])(retorno["imagem"]);
                                imagemProduto = BitmapFactory.DecodeByteArray(imagem, 0, imagem.Length);
                                descricao = retorno.GetString("descricao").ToString();
                                preco = retorno.GetString("preco").ToString();
                                quantidade = "1";
                                StartActivity(typeof(ExibeProduto));
                                Finish();
                            } else {
                                Toast.MakeText(Application.Context, "O Produto não consta no banco de dados!", ToastLength.Long).Show();
                                Finish();
                            }
                        }

                        conexao.Close();
                        varBool = true;
                    }
                });
            }
        }

     

        public void Release() {
        }
    }
}