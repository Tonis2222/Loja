//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Dominio.Repositorio;
//using Moq;
//using Aplicacao;
//using Dominio;
//using Aplicacao.DTO;
//using AutoMapper;
//using Aplicacao.ServicosDeAplicacao;

//namespace TesteIntegracao
//{
//  [TestClass]
//  public class LojaStoneFacadeTesteMockado
//  {
//    private Mock<IRepositorioCliente> mockRepositorioCliente;
//    private Mock<IRepositorioItem> mockRepositorioItem;
//    private Mock<IRepositorioPedido> mockRepositorioPedido;
//    private Mock<IServicoMensageria> mockServicoMensageria;

//    [TestInitialize]
//    public void Inicializar()
//    {
//      Mapper.Reset();
//      MapperInit.MapearDTOs();
//      mockRepositorioCliente = new Mock<IRepositorioCliente>();
//      mockRepositorioItem = new Mock<IRepositorioItem>();
//      mockRepositorioPedido = new Mock<IRepositorioPedido>();
//      mockServicoMensageria = new Mock<IServicoMensageria>();
//    }
    
//    [TestMethod]
//    public void CadastrarCliente()
//    {
//      var facade = new LojaStoneFacade(mockRepositorioCliente.Object, mockRepositorioItem.Object, mockRepositorioPedido.Object, mockServicoMensageria.Object);

//      var clienteDTO = new ClienteDTO()
//      {
//        Nome = "Cliente Teste Mockado 1",
//        CPF = 12345678910,
//        Endereco = new EnderecoDTO()
//        {
//          Rua = "Rua Teste 1",
//          Numero = 3,
//          Complemento = "apartamento 101"
//        }
//      };

//      facade.CadastrarCliente(clienteDTO);

//      mockRepositorioCliente.Verify(a => a.CadastrarCliente(It.IsAny<Cliente>()), Times.Once);
//    }

//    [TestMethod]
//    public void AtualizarCliente()
//    {
//      var facade = new LojaStoneFacade(mockRepositorioCliente.Object, mockRepositorioItem.Object, mockRepositorioPedido.Object, mockServicoMensageria.Object);

//      var clienteDTO = new ClienteDTO()
//      {
//        Id = 1,
//        Nome = "Cliente Teste Mockado 1",
//        CPF = 12345678910,
//        Endereco = new EnderecoDTO()
//        {
//          Rua = "Rua Teste 1",
//          Numero = 3,
//          Complemento = "apartamento 101"
//        }
//      };

//      facade.AtualizarCliente(clienteDTO);

//      mockRepositorioCliente.Verify(a => a.AtualizarCliente(It.IsAny<Cliente>()), Times.Once);
//    }







//  }
//}
