using Dominio;
using InfraSQLServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TesteIntegracao
{
  [TestClass]
  public class RepositorioItemSQLServerTeste

  {
    private string connectionString = @"Server=localhost\SQLEXPRESS;Database=teste;Trusted_Connection=True;";

    [TestMethod]
    public void CadastrarITem()
    {
      var repositorio = new RepositorioItemSQLServer(connectionString);
      var item = new Item()
      {
        Descricao = "Item de Teste 1",
        Valor = 1.99M
      };
      repositorio.CadastrarItem(item);
      Assert.IsTrue(item.Id > 0);
    }

    [TestMethod]
    public void BuscarITem()
    {
      var repositorio = new RepositorioItemSQLServer(connectionString);
      var item = new Item()
      {
        Descricao = "Item de Teste 1",
        Valor = 1.99M
      };
      repositorio.CadastrarItem(item);
      Assert.IsTrue(item.Id > 0);

      var ret = repositorio.BuscarItens();
      Assert.IsNotNull(repositorio);
      Assert.IsTrue(ret.Any(a => a.Id == item.Id));
    }

    [TestMethod]
    public void AtualizarITem()
    {
      var valorEsperado = 5.30M;
      var repositorio = new RepositorioItemSQLServer(connectionString);
      var item = new Item()
      {
        Descricao = "Item de Teste 1",
        Valor = 1.99M
      };
      repositorio.CadastrarItem(item);
      Assert.IsTrue(item.Id > 0);

      var ret = repositorio.BuscarItens();
      Assert.IsNotNull(repositorio);
      Assert.IsTrue(ret.Any(a => a.Id == item.Id));

      var itemSalvo = ret.First(a => a.Id == item.Id);
      itemSalvo.Valor = valorEsperado;

      repositorio.AtualizarItem(itemSalvo);

      ret = repositorio.BuscarItens();
      Assert.IsNotNull(repositorio);
      Assert.IsTrue(ret.Any(a => a.Id == item.Id));
      Assert.AreEqual(valorEsperado, ret.First(a => a.Id == item.Id).Valor);
    }
  }
}
