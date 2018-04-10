using System;

namespace Aplicacao.Retorno
{
  public class RetornoCriarPedido
  {
    public bool Sucesso { get; internal set; }
    public Guid Id { get; internal set; }
    public string Mensagem { get; internal set; }
  }
}