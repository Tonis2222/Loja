using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
  public class FabricaPedido
  {
    public static Pedido CriarPedido(Cliente cliente, List<Item> itens)
    {
      Pedido pedido = new Pedido();
      pedido.Id = Guid.NewGuid();
      pedido.Estado = EstadoPedido.Ativo;
      pedido.Cliente = cliente;
      pedido.Itens = itens;

      return pedido;
    }
  }
}
