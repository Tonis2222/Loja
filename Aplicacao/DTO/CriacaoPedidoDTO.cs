using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.DTO
{
  public class CriacaoPedidoDTO
  {
    public IdVersaoDTO Cliente { get; set; }
    public List<IdVersaoDTO> Itens { get; set; }
  }
}
