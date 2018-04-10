using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.DTO
{
  public class CriacaoPedidoDTO
  {
    public ClienteDTO Cliente { get; set; }
    public List<ItemDTO> Itens { get; set; }
  }
}
