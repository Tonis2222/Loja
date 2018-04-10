using System.Collections.Generic;
using Aplicacao.DTO;

namespace Aplicacao.Retorno
{
  public class RetornoBuscarClientes
  {
    public List<ClienteDTO> Clientes { get; internal set; }
    public bool Sucesso { get; internal set; }
    public string Mensagem { get; internal set; }
  }
}