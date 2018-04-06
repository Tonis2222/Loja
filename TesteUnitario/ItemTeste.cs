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
      bool resultadoEsperado = false;
      string mensagemEsperada = "Descrição não informada.";

      Item item = new Item()
      {
        Valor = 1.99M
      };

      string mensagem = string.Empty;
      var resultado = item.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCadastrarItemSemValor()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Valor não informado.";

      Item item = new Item()
      {
        Descricao = "Item Teste 1"
      };

      string mensagem = string.Empty;
      var resultado = item.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void DeveCadastrarComDescricaoEValor()
    {
      bool resultadoEsperado = true;

      Item item = new Item()
      {
        Descricao = "Item Teste 1",
        Valor = 1.99M
      };

      string mensagem = string.Empty;
      var resultado = item.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
    }

    [TestMethod]
    public void NaoPodeAtualizarItemSemDescricao()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Descrição não informada.";

      Item item = new Item()
      {
        Id = 1,
        Valor = 1.99M
      };

      string mensagem = string.Empty;
      var resultado = item.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeAtualizarItemSemValor()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Valor não informado.";

      Item item = new Item()
      {
        Id = 1,
        Descricao = "Item Teste 1"
      };

      string mensagem = string.Empty;
      var resultado = item.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void DeveAtualizarComDescricaoEValor()
    {
      bool resultadoEsperado = true;

      Item item = new Item()
      {
        Id = 1,
        Descricao = "Item Teste 1",
        Valor = 1.99M
      };

      string mensagem = string.Empty;
      var resultado = item.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
    }

    [TestMethod]
    public void NaoPodeCadastrarItemComDescricaoMaiorQue200Caracteres()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Descrição deve ter no máximo 200 caracteres.";

      Item item = new Item()
      {
        Descricao = "abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghija",
        Valor = 1.99M
      };

      string mensagem = string.Empty;
      var resultado = item.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }
  }
}
