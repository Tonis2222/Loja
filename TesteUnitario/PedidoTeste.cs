using Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteUnitario
{
  [TestClass]
  public class PedidoTeste
  {
    private Cliente clienteParaTeste = new Cliente()
    {
      CPF = 12345678910,
      Nome = "Cliente Teste",
      Endereco = new Endereco()
      {
        Rua = "Rua Teste",
        Numero = 3,
        Complemento = "101"
      }
    };
    private List<Item> itensParaTeste = new List<Item>() {
      new Item()
      {
        Id = 1,
        Descricao = "Item de Teste 1",
        Valor = 1.99M
      },
      new Item()
      {
        Id = 2,
        Descricao = "Item de Teste 2",
        Valor = 1000
      }};

    [TestMethod]
    public void NaoPodeCriarPedidoCancelado()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "O Pedido deve ser criado ativo.";

      var pedido = FabricaPedido.CriarPedido(clienteParaTeste, itensParaTeste);
      pedido.Estado = EstadoPedido.Cancelado;

      string mensagem;
      var resultado = pedido.EValidoParaCriar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCriarPedidoSemId()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Identificador não informado.";

      var pedido = FabricaPedido.CriarPedido(clienteParaTeste, itensParaTeste);
      pedido.Id = Guid.Empty;

      string mensagem;
      var resultado = pedido.EValidoParaCriar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCriarPedidoSemCliente()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Cliente não informado.";

      var pedido = FabricaPedido.CriarPedido(clienteParaTeste, itensParaTeste);
      pedido.Cliente = null;

      string mensagem;
      var resultado = pedido.EValidoParaCriar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCriarPedidoSemItens()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Nenhum item informado.";

      var pedido = FabricaPedido.CriarPedido(clienteParaTeste, itensParaTeste);
      pedido.Itens = new List<Item>();

      string mensagem;
      var resultado = pedido.EValidoParaCriar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCancelarPedidoCancelado()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "O Pedido deve estar ativo.";

      var pedido = FabricaPedido.CriarPedido(clienteParaTeste, itensParaTeste);
      pedido.Estado = EstadoPedido.Cancelado;

      string mensagem;
      var resultado = pedido.EValidoParaCancelar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void DeveCancelarPedidoAtivo()
    {
      bool resultadoEsperado = true;
      
      var pedido = FabricaPedido.CriarPedido(clienteParaTeste, itensParaTeste);
      
      string mensagem;
      var resultado = pedido.EValidoParaCancelar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
    }
  }
}
