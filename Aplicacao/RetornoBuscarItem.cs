using Aplicacao.DTO;

namespace Aplicacao
{
  public class RetornoBuscarItem
  {
    public bool Sucesso { get; internal set; }
    public ItemDTO Item { get; internal set; }
    public string Mensagem { get; internal set; }
  }
}