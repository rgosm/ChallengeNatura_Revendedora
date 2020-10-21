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
    class ListViewAdapter : BaseAdapter<Lista>{
        public List<Lista> items;
        private Context context;

        public ListViewAdapter(Context context, List<Lista> items) {
            this.items = items;
            this.context = context;
        }

        public override int Count {
            get { return items.Count; }
        }

        public override long GetItemId(int position) {
            return position;
        }

        public override Lista this[int position] {
            get { return items[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent) {
            View row = convertView;
            if(row == null) {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.lista, null, false);
            }

            TextView txtCodigo = row.FindViewById<TextView>(Resource.Id.txtCodigo);
            txtCodigo.Text = items[position].codigo;
            
            TextView txtDescricao = row.FindViewById<TextView>(Resource.Id.txtDescricao);
            txtDescricao.Text = items[position].descricao;

            TextView txtQuantidade = row.FindViewById<TextView>(Resource.Id.txtQuantidade);
            txtQuantidade.Text = items[position].quantidade;

            TextView txtValorTotal = row.FindViewById<TextView>(Resource.Id.txtValorTotal);
            txtValorTotal.Text = items[position].valorTotal;

            TextView txtCliente = row.FindViewById<TextView>(Resource.Id.txtCliente);
            txtCliente.Text = items[position].cliente;

            return row;
        }
    }
}