using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio;
using System.Collections.Generic;

namespace TesteUnitario
{
  [TestClass]
  public class FabricaPedidoTeste
  {
    [TestMethod]
    public void CriacaoDeUmNovoPedido()
    {
      var EstadoPedidoEsperado = EstadoPedido.Ativo;

      Cliente clienteEsperado = new Cliente()
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
      List<Item> itensEsperados = new List<Item>() {
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

      var pedido = FabricaPedido.CriarPedido(clienteEsperado, itensEsperados);

      Assert.IsNotNull(pedido, "Pedido nulo");
      Assert.AreEqual(EstadoPedidoEsperado, pedido.Estado, "Estado diferente do esperado");
      Assert.IsNotNull(pedido.Id,"Guid não gerado");
      Assert.AreEqual(clienteEsperado, pedido.Cliente, "Cliente não cadastrado no Pedido");
      Assert.AreEqual(itensEsperados, pedido.Itens, "Itens não cadastrados no Pedido");
      string mensagem;
      Assert.IsTrue(pedido.EValidoCriar(out mensagem));

    }
  }
}
