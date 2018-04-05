using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Repositorio
{
  public interface IRepositorioPedido
  {
    void CriarPedido(Pedido pedido);
    void DesistirDoPedido(Pedido pedido);
    List<Pedido> BuscarPedidos();
    List<Pedido> BuscarPedidosPorCliente(Cliente cliente);

  }
}
