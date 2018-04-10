using System.Collections.Generic;
using Aplicacao.DTO;

namespace Aplicacao
{
  public class RetornoBuscarPedidos
  {
    public bool Sucesso { get; internal set; }
    public List<PedidoDTO> Pedidos { get; internal set; }
    public string Mensagem { get; internal set; }
  }
}