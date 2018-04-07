using Dominio;
using InfraSQLServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TesteIntegracao
{
  [TestClass]
  public class RepositorioClienteSQLServerTeste
  {
    private string connectionString = @"Server=localhost\SQLEXPRESS;Database=teste;Trusted_Connection=True;";

    [TestMethod]
    public void Cadastrarcliente()
    {
      var repositorio = new RepositorioClienteSQLServer(connectionString);
      Cliente cliente = new Cliente()
      {
        CPF = new Random().Next(0, int.MaxValue),
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };
      repositorio.CadastrarCliente(cliente);
      Assert.IsTrue(cliente.Id > 0);
    }

    [TestMethod]
    public void Buscarcliente()
    {
      var repositorio = new RepositorioClienteSQLServer(connectionString);
      Cliente cliente = new Cliente()
      {
        CPF = new Random().Next(0, int.MaxValue),
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };
      repositorio.CadastrarCliente(cliente);
      Assert.IsTrue(cliente.Id > 0);

      var ret = repositorio.BuscarClientes();
      Assert.IsNotNull(repositorio);
      Assert.IsTrue(ret.Any(a => a.Id == cliente.Id));
    }

    [TestMethod]
    public void Atualizarcliente()
    {
      var nomeEsperado = "Cliente Teste Atualizado";
      var repositorio = new RepositorioClienteSQLServer(connectionString);
      Cliente cliente = new Cliente()
      {
        CPF = new Random().Next(0, int.MaxValue),
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };
      repositorio.CadastrarCliente(cliente);
      Assert.IsTrue(cliente.Id > 0);

      var ret = repositorio.BuscarClientes();
      Assert.IsNotNull(repositorio);
      Assert.IsTrue(ret.Any(a => a.Id == cliente.Id));

      var clienteSalvo = ret.First(a => a.Id == cliente.Id);
      clienteSalvo.Nome = nomeEsperado;

      repositorio.AtualizarCliente(clienteSalvo);

      ret = repositorio.BuscarClientes();
      Assert.IsNotNull(repositorio);
      Assert.IsTrue(ret.Any(a => a.Id == cliente.Id));
      Assert.AreEqual(nomeEsperado, ret.First(a => a.Id == cliente.Id).Nome);
    }

  }
}
