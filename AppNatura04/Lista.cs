using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AppNaturaRevendedora {
    public class Lista {
        public string descricao { get; set; }
        public string codigo { get; set; }
        public string quantidade { get; set; }
        public string valorTotal { get; set; }
        public string somaValorTotal { get; set; }
        public string cliente { get; set; }
    }
}