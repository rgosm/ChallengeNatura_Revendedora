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
    public static class ComandosSQL { 
        public static string insereDados = "insert into tblCarrinhoCompra(idCarrinhoCompra, idCliente, idRevendedor, idProduto, quantidade, status) values(null, @idCliente, @idRevendedor, @idProduto, @quantidade, 'Enviado')";
        public static string verificaProduto = "SELECT * FROM tblProduto WHERE idProduto = @idProduto";
        public static string exibeListaCarrinho = "SELECT * FROM tblCarrinhoCompra car, tblProduto p, tblCliente cli WHERE cli.idCliente = car.idCliente and car.idProduto = p.idProduto and car.idRevendedor=@idRevendedor and (car.status = 'Enviado' or car.status = 'comprado')";
        public static string contaProdutoCarrinho = "SELECT count(*) FROM tblCarrinhoCompra WHERE idProduto = @idProduto and idRevendedor = @idRevendedor and idCliente = @idCliente";
        public static string selecionaProdutoCarrinho = "SELECT * FROM tblCarrinhoCompra c, tblProduto p WHERE c.idProduto = p.idProduto and c.idRevendedor = @idRevendedor and c.idProduto = @idProduto";
        public static string atualizaProdutoCarrinho = "UPDATE tblCarrinhoCompra SET quantidade = @quantidade WHERE idRevendedor = @idRevendedor and idProduto = @idProduto and idCliente = @idCliente";
        public static string excluiProdutoCarrinho = "DELETE FROM tblCarrinhoCompra WHERE idProduto = @idProduto and idRevendedor = @idRevendedor and idCliente = @idCliente and (status = 'Processando' or status = 'Enviado')";
        public static string excluiTodosProdutoCarrinho = "DELETE FROM tblCarrinhoCompra WHERE idRevendedor = @idRevendedor and (status = 'Processando' or status = 'Enviado')";
        public static string enviaCarrinho = "UPDATE tblCarrinhoCompra SET status = 'Comprado' WHERE idRevendedor = @idRevendedor and (status = 'Processando' or status = 'Enviado')";

    }
}
