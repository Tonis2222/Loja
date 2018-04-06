using Dominio;
using Dominio.Repositorio;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace InfraMongoDB
{
  public class RepositorioPedidoMongoDB : IRepositorioPedido
  {
    private const string NOME_BASE_PEDIDOS = "basePedidos";
    IMongoClient clienteMongoDB;
    IMongoDatabase basePedido;
    IMongoCollection<Pedido> pedidos;


    public RepositorioPedidoMongoDB(string connectionString)
    {
      this.clienteMongoDB = new MongoClient(connectionString);

      basePedido = clienteMongoDB.GetDatabase(NOME_BASE_PEDIDOS);

      pedidos = basePedido.GetCollection<Pedido>(nameof(Pedido));

      if (pedidos == null)
      {
        basePedido.CreateCollection(nameof(Pedido));
        pedidos = basePedido.GetCollection<Pedido>(nameof(Pedido));
      }
    }

    public Pedido BuscarPedido(Guid id)
    {
      return pedidos.Find(Builders<Pedido>.Filter.Eq(a => a.Id, id)).FirstOrDefault();
    }

    public List<Pedido> BuscarPedidos()
    {
      return pedidos.Find(Builders<Pedido>.Filter.Empty).ToList();
    }

    public List<Pedido> BuscarPedidosPorCliente(Cliente cliente)
    {
      return pedidos.Find(Builders<Pedido>.Filter.Eq(a => a.Cliente.Id, cliente.Id)).ToList();
    }

    public void CriarPedido(Pedido pedido)
    {
      pedidos.InsertOne(pedido);
    }

    public void AtualizarPedido(Pedido pedido)
    {
      pedidos.ReplaceOne(Builders<Pedido>.Filter.Eq(a => a.Id, pedido.Id), pedido);
    }
  }
}
