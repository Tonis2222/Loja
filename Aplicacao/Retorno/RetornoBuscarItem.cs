using Aplicacao.DTO;

namespace Aplicacao.Retorno
{
  public class RetornoBuscarItem
  {
    public bool Sucesso { get; internal set; }
    public ItemDTO Item { get; internal set; }
    public string Mensagem { get; internal set; }
  }
}