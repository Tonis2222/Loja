using Aplicacao.DTO;

namespace Aplicacao.Retorno
{
  public class RetornoBuscarPedido
  {
    public bool Sucesso { get; internal set; }
    public PedidoDTO Pedido { get; internal set; }
    public string Mensagem { get; internal set; }
  }
}