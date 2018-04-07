using Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServicoMensageriaRabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteIntegracao
{
  [TestClass]
  public class MensageriaRabbitMQTeste
  {
    [TestMethod]
    public void PublicarCopiaDePedido()
    {
      var servico = new MensageriaRabbitMQ("localhost");

      Cliente cliente = new Cliente()
      {
        Id = 1,
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };
      List<Item> itens = new List<Item>() {
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

      var pedido = FabricaPedido.CriarPedido(cliente, itens);

      servico.GuardarCopia(pedido);

    }

  }
}
