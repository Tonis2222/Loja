using Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteUnitario
{

  [TestClass]
  public class ItemTeste
  {
    [TestMethod]
    public void NaoPodeCadastrarItemSemDescricao()
    {
      bool ResultadoEsperado = false;
      string MensagemEsperada = "Descrição não informada.";

      Item item = new Item()
      {
        Valor = 1.99M
      };

      string mensagem = string.Empty;
      var resultado = item.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
      Assert.AreEqual(MensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCadastrarItemSemValor()
    {
      bool ResultadoEsperado = false;
      string MensagemEsperada = "Valor não informado.";

      Item item = new Item()
      {
        Descricao = "Item Teste 1"
      };

      string mensagem = string.Empty;
      var resultado = item.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
      Assert.AreEqual(MensagemEsperada, mensagem);
    }

    [TestMethod]
    public void DeveCadastrarComDescricaoEValor()
    {
      bool ResultadoEsperado = true;

      Item item = new Item()
      {
        Descricao = "Item Teste 1",
        Valor = 1.99M
      };

      string mensagem = string.Empty;
      var resultado = item.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
    }

    [TestMethod]
    public void NaoPodeAtualizarItemSemDescricao()
    {
      bool ResultadoEsperado = false;
      string MensagemEsperada = "Descrição não informada.";

      Item item = new Item()
      {
        Id = 1,
        Valor = 1.99M
      };

      string mensagem = string.Empty;
      var resultado = item.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
      Assert.AreEqual(MensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeAtualizarItemSemValor()
    {
      bool ResultadoEsperado = false;
      string MensagemEsperada = "Valor não informado.";

      Item item = new Item()
      {
        Id = 1,
        Descricao = "Item Teste 1"
      };

      string mensagem = string.Empty;
      var resultado = item.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
      Assert.AreEqual(MensagemEsperada, mensagem);
    }

    [TestMethod]
    public void DeveAtualizarComDescricaoEValor()
    {
      bool ResultadoEsperado = true;

      Item item = new Item()
      {
        Id = 1,
        Descricao = "Item Teste 1",
        Valor = 1.99M
      };

      string mensagem = string.Empty;
      var resultado = item.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
    }
  }
}
