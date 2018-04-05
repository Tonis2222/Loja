using Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteUnitario
{

  [TestClass]
  public class ClienteTeste
  {
    [TestMethod]
    public void NaoPodeCadastrarClienteSemNome()
    {
      bool ResultadoEsperado = false;
      string MensagemEsperada = "Nome não informado.";

      Cliente cliente = new Cliente()
      {
        CPF = 12345678910,
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
      Assert.AreEqual(MensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCadastrarClienteSemCPF()
    {
      bool ResultadoEsperado = false;
      string MensagemEsperada = "CPF não informado.";

      Cliente cliente = new Cliente()
      {
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
      Assert.AreEqual(MensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCadastrarClienteSemEndereco()
    {
      bool ResultadoEsperado = false;
      string MensagemEsperada = "Endereço não informado.";

      Cliente cliente = new Cliente()
      {
        CPF = 12345678910,
        Nome = "Cliente Teste",
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
      Assert.AreEqual(MensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCadastrarClienteSemRua()
    {
      bool ResultadoEsperado = false;
      string MensagemEsperada = "Rua não informada.";

      Cliente cliente = new Cliente()
      {
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
      Assert.AreEqual(MensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCadastrarClienteSemNumero()
    {
      bool ResultadoEsperado = false;
      string MensagemEsperada = "Número não informado.";

      Cliente cliente = new Cliente()
      {
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
      Assert.AreEqual(MensagemEsperada, mensagem);
    }

    [TestMethod]
    public void DeveCadastrarClienteComTodosOsDados()
    {
      bool ResultadoEsperado = true;

      Cliente cliente = new Cliente()
      {
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 100,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);

    }

    [TestMethod]
    public void DeveCadastrarClienteSemComplemento()
    {
      bool ResultadoEsperado = true;

      Cliente cliente = new Cliente()
      {
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 100
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);

    }

    [TestMethod]
    public void NaoPodeAtualizarClienteSemID()
    {
      bool ResultadoEsperado = false;
      string MensagemEsperada = "Id não informado.";

      Cliente cliente = new Cliente()
      {
        CPF = 12345678910,
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
      Assert.AreEqual(MensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeAtualizarClienteSemNome()
    {
      bool ResultadoEsperado = false;
      string MensagemEsperada = "Nome não informado.";

      Cliente cliente = new Cliente()
      {
        Id = 1,
        CPF = 12345678910,
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
      Assert.AreEqual(MensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeAtualizarClienteSemCPF()
    {
      bool ResultadoEsperado = false;
      string MensagemEsperada = "CPF não informado.";

      Cliente cliente = new Cliente()
      {
        Id = 1,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
      Assert.AreEqual(MensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeAtualizarClienteSemEndereco()
    {
      bool ResultadoEsperado = false;
      string MensagemEsperada = "Endereço não informado.";

      Cliente cliente = new Cliente()
      {
        Id = 1,
        CPF = 12345678910,
        Nome = "Cliente Teste",
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
      Assert.AreEqual(MensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeAtualizarClienteSemRua()
    {
      bool ResultadoEsperado = false;
      string MensagemEsperada = "Rua não informada.";

      Cliente cliente = new Cliente()
      {
        Id = 1,
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
      Assert.AreEqual(MensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeAtualizarClienteSemNumero()
    {
      bool ResultadoEsperado = false;
      string MensagemEsperada = "Número não informado.";

      Cliente cliente = new Cliente()
      {
        Id = 1,
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);
      Assert.AreEqual(MensagemEsperada, mensagem);
    }

    [TestMethod]
    public void DeveAtualizarClienteComTodosOsDados()
    {
      bool ResultadoEsperado = true;

      Cliente cliente = new Cliente()
      {
        Id = 1,
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 100,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);

    }

    [TestMethod]
    public void DeveAtualizarClienteSemComplemento()
    {
      bool ResultadoEsperado = true;

      Cliente cliente = new Cliente()
      {
        Id = 1,
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 100
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(ResultadoEsperado, resultado);

    }
  }
}
