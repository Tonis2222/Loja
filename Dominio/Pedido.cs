using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
  public class Pedido
  {
    public Guid Id { get; set; }
    public Cliente Cliente { get; set; }
    public List<Item> Itens { get; set; }
    public EstadoPedido Estado { get; set; }
    
    public bool EValidoParaCriar(out string mensagem)
    {
      if (Id == null || Id == Guid.Empty)
      {
        mensagem = "Identificador não informado.";
        return false;
      }

      if (Estado != EstadoPedido.Ativo)
      {
        mensagem = "O Pedido deve ser criado ativo.";
        return false;
      }

      if (Cliente == null)
      {
        mensagem = "Cliente não informado.";
        return false;
      }

      if (Itens == null || !Itens.Any())
      {
        mensagem = "Nenhum item informado.";
        return false;
      }

      mensagem = string.Empty;
      return true;
    }
    public bool EValidoParaCancelar(out string mensagem)
    {
      if (Estado != EstadoPedido.Ativo)
      {
        mensagem = "O Pedido deve estar ativo.";
        return false;
      }

      mensagem = string.Empty;
      return true;
    }

    public bool Cancelar(out string mensagem)
    {
      if (EValidoParaCancelar(out mensagem))
      {
        Estado = EstadoPedido.Cancelado;
        return true;
      }
      else
      {
        return false;
      }
    }

  }
}
