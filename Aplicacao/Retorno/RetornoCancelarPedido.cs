using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.Retorno
{
  public class RetornoCancelarPedido
  {
    public Guid Id { get; internal set; }
    public bool Sucesso { get; internal set; }
    public string Mensagem { get; internal set; }
    
  }
}
