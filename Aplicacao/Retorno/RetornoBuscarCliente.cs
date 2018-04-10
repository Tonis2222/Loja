using Aplicacao.DTO;

namespace Aplicacao.Retorno
{
  public class RetornoBuscarCliente
  {
    public ClienteDTO Cliente { get; internal set; }
    public bool Sucesso { get; internal set; }
    public string Mensagem { get; internal set; }
  }
}