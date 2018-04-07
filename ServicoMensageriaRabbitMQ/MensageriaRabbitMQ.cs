using Aplicacao.ServicosDeAplicacao;
using Dominio;
using RabbitMQ.Client;
using System;
using System.Text;

namespace ServicoMensageriaRabbitMQ
{
  public class MensageriaRabbitMQ : IServicoMensageria
  {
    ConnectionFactory fabricaConexoes;
    public MensageriaRabbitMQ(string hostName)
    {
      fabricaConexoes = new ConnectionFactory() { HostName = hostName };
    }

    public void GuardarCopia(Pedido pedido)
    {

      var pedidoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(pedido);

      using (var connection = fabricaConexoes.CreateConnection())
      {
        using (var channel = connection.CreateModel())
        {
          channel.QueueDeclare(queue: "FilaPedido");

          var conteudoMensagem = Encoding.UTF8.GetBytes(pedidoSerializado);

          channel.BasicPublish(exchange: "",
                               routingKey: "Pedido",
                               body: conteudoMensagem);
        }
      }
    }
  }
}
