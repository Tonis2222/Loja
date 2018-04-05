using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
  public class Pedido
  {
    public Guid Id { get; set; }
    public Cliente Cliente { get; set; }
    public List<Item> Itens { get; set; }
    public EstadoPedido Estado { get; set; }
  }
}
