using Dominio;
using Dominio.Repositorio;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    public async Task<Pedido> BuscarPedidoAsync(Guid id)
    {
      var retorno = await pedidos.FindAsync(Builders<Pedido>.Filter.Eq(a => a.Id, id));
      return retorno.FirstOrDefault();
    }

    public async Task<List<Pedido>> BuscarPedidosAsync()
    {
      var retorno = await pedidos.FindAsync(Builders<Pedido>.Filter.Empty);
      return retorno.ToList();
    }

    public async Task<List<Pedido>> BuscarPedidosPorClienteAsync(Cliente cliente)
    {
      var retorno = await pedidos.FindAsync(Builders<Pedido>.Filter.Eq(a => a.Cliente.Id, cliente.Id));
      return retorno.ToList();
    }

    public async Task CriarPedidoAsync(Pedido pedido)
    {
      await pedidos.InsertOneAsync(pedido);
    }

    public async Task AtualizarPedidoAsync(Pedido pedido)
    {
      await pedidos.ReplaceOneAsync(Builders<Pedido>.Filter.Eq(a => a.Id, pedido.Id), pedido);
    }
  }
}
