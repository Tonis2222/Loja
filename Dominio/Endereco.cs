using System;

namespace Dominio
{
  public class Endereco
  {
    private const int TAMANHO_MAXIMO_RUA = 200;
    private const int TAMANHO_MAXIMO_COMPLEMENTO = 200;

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

      if (Rua.Length > TAMANHO_MAXIMO_RUA)
      {
        mensagemValidacao = $"Rua deve ter no máximo {TAMANHO_MAXIMO_RUA} caracteres.";
        return false;
      }

      if (Numero < 1)
      {
        mensagemValidacao = "Número não informado.";
        return false;
      }

      if (!string.IsNullOrEmpty(Complemento) && Complemento.Length > TAMANHO_MAXIMO_COMPLEMENTO)
      {
        mensagemValidacao = $"Complemento deve ter no máximo {TAMANHO_MAXIMO_COMPLEMENTO} caracteres.";
        return false;
      }

      mensagemValidacao = string.Empty;
      return true;
    }
  }
}