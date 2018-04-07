using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.ServicosDeAplicacao
{
  public interface IServicoMensageria
  {
    void GuardarCopia(Pedido pedido);
  }
}
