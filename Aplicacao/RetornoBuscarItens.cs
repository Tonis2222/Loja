using System.Collections.Generic;
using Aplicacao.DTO;

namespace Aplicacao
{
  public class RetornoBuscarItens
  {
    public List<ItemDTO> Itens { get; internal set; }
    public bool Sucesso { get; internal set; }
    public string Mensagem { get; internal set; }
  }
}