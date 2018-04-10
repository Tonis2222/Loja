using Dominio;
using InfraMongoDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace TesteIntegracao
{

  [TestClass]
  public class RepositorioPedidoMongoDBTeste
  {
    private const string connectionString = "mongodb://localhost:27017";

    [TestMethod]
    public void CriarPedido()
    {
      Cliente clienteEsperado = new Cliente()
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

      var repositorio = new RepositorioPedidoMongoDB(connectionString);

      var retorno = repositorio.CriarPedidoAsync(pedido);
      retorno.Wait();
    }

    [TestMethod]
    public void BuscarPedido()
    {
      Cliente clienteEsperado = new Cliente()
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

      var repositorio = new RepositorioPedidoMongoDB(connectionString);

      var retorno = repositorio.CriarPedidoAsync(pedido);
      retorno.Wait();

      var retorno2 = repositorio.BuscarPedidoAsync(pedido.Id);
      retorno2.Wait();
      var pedidoSalvo = retorno2.Result;

      Assert.IsNotNull(pedidoSalvo);
      Assert.AreEqual(pedidoSalvo.Id, pedido.Id);
      Assert.IsNotNull(pedidoSalvo.Cliente);
      Assert.AreEqual(pedidoSalvo.Cliente.Id, pedido.Cliente.Id);
      Assert.IsNotNull(pedidoSalvo.Itens);
      foreach (var item in itensEsperados)
      {
        Assert.IsTrue(pedidoSalvo.Itens.Any(a => a.Id == item.Id));
      }
    }

    [TestMethod]
    public void AtualizarPedido()
    {
      EstadoPedido estadoEsperado = EstadoPedido.Cancelado;

      Cliente clienteEsperado = new Cliente()
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

      var repositorio = new RepositorioPedidoMongoDB(connectionString);

      var retorno = repositorio.CriarPedidoAsync(pedido);
      retorno.Wait();
      
      var retorno2 = repositorio.BuscarPedidoAsync(pedido.Id);
      retorno2.Wait();

      var pedidoSalvo = retorno2.Result;

      Assert.IsNotNull(pedidoSalvo);
      Assert.AreEqual(pedidoSalvo.Id, pedido.Id);
      Assert.IsNotNull(pedidoSalvo.Cliente);
      Assert.AreEqual(pedidoSalvo.Cliente.Id, pedido.Cliente.Id);
      Assert.IsNotNull(pedidoSalvo.Itens);
      foreach (var item in itensEsperados)
      {
        Assert.IsTrue(pedidoSalvo.Itens.Any(a => a.Id == item.Id));
      }

      string mensagem;
      var resultadoCancelamento = pedidoSalvo.Cancelar(out mensagem);

      Assert.IsTrue(resultadoCancelamento);
      Assert.IsTrue(string.IsNullOrEmpty(mensagem));

      var retorno3 = repositorio.AtualizarPedidoAsync(pedidoSalvo);
      retorno3.Wait();

      var retorno4 = repositorio.BuscarPedidoAsync(pedido.Id);
      retorno4.Wait();
      pedidoSalvo = retorno4.Result;

      Assert.IsNotNull(pedidoSalvo);
      Assert.AreEqual(pedidoSalvo.Id, pedido.Id);
      Assert.IsNotNull(pedidoSalvo.Cliente);
      Assert.AreEqual(pedidoSalvo.Cliente.Id, pedido.Cliente.Id);
      Assert.IsNotNull(pedidoSalvo.Itens);
      foreach (var item in itensEsperados)
      {
        Assert.IsTrue(pedidoSalvo.Itens.Any(a => a.Id == item.Id));
      }

      Assert.AreEqual(estadoEsperado, pedidoSalvo.Estado);

    }

    [TestMethod]
    public void BuscarPedidosPorCliente()
    {
      Cliente cliente = new Cliente()
      {
        Id = 10,
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };
      List<Pedido> pedidosEsperados = new List<Pedido>();

      for (int i = 0; i < 5; i++)
      {
        var itens = new List<Item>()
        {
          new Item()
          {
            Id = i,
            Descricao = "Item de Teste 1",
            Valor = i
          }
        };

        var pedido = FabricaPedido.CriarPedido(cliente, itens);
        pedidosEsperados.Add(pedido);
      }

      var repositorio = new RepositorioPedidoMongoDB(connectionString);

      pedidosEsperados.ForEach(pedido =>
      {
        var retorno = repositorio.CriarPedidoAsync(pedido);
        retorno.Wait();
      });

      var retorno2 = repositorio.BuscarPedidosPorClienteAsync(cliente);
      retorno2.Wait();

      var pedidosSalvos = retorno2.Result;

      pedidosEsperados.ForEach(pedido =>
      {
        var pedidoSalvo = pedidosSalvos.First(a => a.Id == pedido.Id);

        Assert.IsNotNull(pedidoSalvo);
        Assert.AreEqual(pedidoSalvo.Id, pedido.Id);
        Assert.IsNotNull(pedidoSalvo.Cliente);
        Assert.AreEqual(pedidoSalvo.Cliente.Id, pedido.Cliente.Id);
        Assert.IsNotNull(pedidoSalvo.Itens);
        foreach (var item in pedido.Itens)
        {
          Assert.IsTrue(pedidoSalvo.Itens.Any(a => a.Id == item.Id));
        }
      });
    }

    [TestCleanup]
    public void Limpa()
    {
      var clienteMongoDB = new MongoClient(connectionString);

      clienteMongoDB.DropDatabase("basePedidos");

    }

  }
}
