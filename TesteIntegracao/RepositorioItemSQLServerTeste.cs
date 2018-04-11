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

      var retorno = repositorio.CadastrarItemAsync(item);
      retorno.Wait();

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

      var retorno = repositorio.CadastrarItemAsync(item);
      retorno.Wait();

      Assert.IsTrue(item.Id > 0);
      var retorno2 = repositorio.BuscarItensAsync();
      retorno2.Wait();

      var itens = retorno2.Result;
      Assert.IsNotNull(itens);
      Assert.IsTrue(itens.Any(a => a.Id == item.Id));
    }

    [TestMethod]
    public void BuscarITemPorId()
    {
      var repositorio = new RepositorioItemSQLServer(connectionString);
      var item = new Item()
      {
        Descricao = "Item de Teste 1",
        Valor = 1.99M
      };

      var retorno = repositorio.CadastrarItemAsync(item);
      retorno.Wait();

      Assert.IsTrue(item.Id > 0);
      var retorno2 = repositorio.BuscarItemAsync(item.Id, null);
      retorno2.Wait();

      var itemBusca = retorno2.Result;
      Assert.IsNotNull(itemBusca);
      Assert.IsTrue(itemBusca.Id == item.Id);
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

      var retorno = repositorio.CadastrarItemAsync(item);
      retorno.Wait();
      Assert.IsTrue(item.Id > 0);

      var retorno2 = repositorio.BuscarItensAsync();
      retorno2.Wait();

      var itens = retorno2.Result;
      Assert.IsNotNull(itens);
      Assert.IsTrue(itens.Any(a => a.Id == item.Id));

      var itemSalvo = itens.First(a => a.Id == item.Id);
      itemSalvo.Valor = valorEsperado;

      var retorno3 = repositorio.AtualizarItemAsync(itemSalvo);
      retorno3.Wait();

      var retorno4 = repositorio.BuscarItensAsync();
      retorno4.Wait();

      itens = retorno4.Result;
      Assert.IsNotNull(itens);
      Assert.IsTrue(itens.Any(a => a.Id == item.Id));
      Assert.AreEqual(valorEsperado, itens.First(a => a.Id == item.Id).Valor);
    }
  }
}
