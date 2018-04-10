using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorio
{
  public interface IRepositorioPedido
  {
    Task CriarPedidoAsync(Pedido pedido);
    Task AtualizarPedidoAsync(Pedido pedido);
    Task<Pedido> BuscarPedidoAsync(Guid id);
    Task<List<Pedido>> BuscarPedidosAsync();
    Task<List<Pedido>> BuscarPedidosPorClienteAsync(Cliente cliente);
  }
}
