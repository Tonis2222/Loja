using System;

namespace Dominio
{
  public class Endereco
  {
    public string Rua { get; set; }
    public int Numero { get; set; }
    public string Complemento { get; set; }

    public bool EValidoParaCadastrar(out string mensagemValidacao)
    {
      if (string.IsNullOrWhiteSpace(Rua))
      {
        mensagemValidacao = "Rua não informada.";
        return false;
      }

      if (Numero < 1)
      {
        mensagemValidacao = "Número não informado.";
        return false;
      }
      
      mensagemValidacao = string.Empty;
      return true;
    }
  }
}