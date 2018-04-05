using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.DTO
{
  public class PedidoDTO
  {
    public Guid Id { get; set; }
    public ClienteDTO Cliente { get; set; }
    public List<ItemDTO> Itens { get; set; }
    public EstadoPedidoDTO Estado { get; set; }
  }
}
