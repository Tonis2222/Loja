using Aplicacao.DTO;
using Aplicacao.Retorno;
using AutoMapper;
using Dominio;
using Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aplicacao
{
  public class LojaStoneFacade
  {
    private IRepositorioCliente _repositorioCliente;
    private IRepositorioItem _repositorioItem;
    private IRepositorioPedido _repositorioPedido;

    public LojaStoneFacade(IRepositorioCliente repositorioCliente, IRepositorioItem repositorioItem, IRepositorioPedido repositorioPedido)
    {
      _repositorioCliente = repositorioCliente;
      _repositorioItem = repositorioItem;
      _repositorioPedido = repositorioPedido;

    }

    public RetornoCadastrarCliente CadastrarCliente(ClienteDTO clienteDTO)
    {
      if (clienteDTO == null)
        throw new ArgumentNullException("clienteDTO");

      var cliente = Mapper.Map<Cliente>(clienteDTO);

      string mensagemValidacao;
      if (!cliente.EValidoParaCadastrar(out mensagemValidacao))
      {
        return new RetornoCadastrarCliente() { Sucesso = false, Mensagem = mensagemValidacao };
      }

      _repositorioCliente.CadastrarCliente(cliente);

      return new RetornoCadastrarCliente() { Sucesso = true, Id = cliente.Id };
    }

    public RetornoAtualizarCliente AtualizarCliente(ClienteDTO clienteDTO)
    {
      if (clienteDTO == null)
        throw new ArgumentNullException("clienteDTO");

      var cliente = Mapper.Map<Cliente>(clienteDTO);

      string mensagemValidacao;
      if (!cliente.EValidoParaAtualizar(out mensagemValidacao))
      {
        return new RetornoAtualizarCliente() { Sucesso = false, Mensagem = mensagemValidacao };
      }

      _repositorioCliente.AtualizarCliente(cliente);

      return new RetornoAtualizarCliente() { Sucesso = true, Id = cliente.Id };
    }

    public List<ClienteDTO> BuscarClientes()
    {
      var clientes = _repositorioCliente.BuscarClientes();

      var listaRetorno = new List<ClienteDTO>();

      clientes.ForEach(cliente => 
      {
        listaRetorno.Add(Mapper.Map<ClienteDTO>(cliente));
      });

      return listaRetorno;
    }

    public RetornoCadastrarItem CadastrarItem(ItemDTO itemDTO)
    {
      if (itemDTO == null)
        throw new ArgumentNullException("itemDTO");

      var item = Mapper.Map<Item>(itemDTO);

      string mensagemValidacao;
      if (!item.EValidoParaCadastrar(out mensagemValidacao))
      {
        return new RetornoCadastrarItem() { Sucesso = false, Mensagem = mensagemValidacao };
      }

      _repositorioItem.CadastrarItem(item);

      return new RetornoCadastrarItem() { Sucesso = true, Id = item.Id };
    }

    public RetornoAtualizarItem AtualizarItem(ItemDTO itemDTO)
    {
      if (itemDTO == null)
        throw new ArgumentNullException("itemDTO");

      var item = Mapper.Map<Item>(itemDTO);

      string mensagemValidacao;
      if (!item.EValidoParaAtualizar(out mensagemValidacao))
      {
        return new RetornoAtualizarItem() { Sucesso = false, Mensagem = mensagemValidacao };
      }

      _repositorioItem.AtualizarItem(item);

      return new RetornoAtualizarItem() { Sucesso = true, Id = item.Id };
    }

    public List<ItemDTO> BuscarItems()
    {
      var items = _repositorioItem.BuscarItens();

      var listaRetorno = new List<ItemDTO>();

      items.ForEach(item =>
      {
        listaRetorno.Add(Mapper.Map<ItemDTO>(item));
      });

      return listaRetorno;
    }

    //public RetornoCriarPedido CriarPedido(ClienteDTO clienteDTO, List<ItemDTO> ItensDTO)
    //{
    //  if (clienteDTO == null)
    //    throw new ArgumentNullException("clienteDTO");

    //  if (ItensDTO == null || !ItensDTO.Any())
    //    throw new ArgumentNullException("ItensDTO");

    //  var cliente = Mapper.Map<Cliente>(clienteDTO);
    //  var itens = ItensDTO.Select(itemDTO => Mapper.Map<Item>(itemDTO)).ToList();

    //  var pedido = FabricaPedido.CriarPedido(cliente, itens);
      
    //  _repositorioPedido.CriarPedido(pedido);

    //  return new RetornoCriarPedido() { Sucesso = true, Id = pedido.Id };
    //}

    //public RetornoAtualizarPedido DesistirDoPedido(PedidoDTO pedidoDTO)
    //{
    //  if (pedidoDTO == null)
    //    throw new ArgumentNullException("pedidoDTO");

    //  var pedido = Mapper.Map<Pedido>(pedidoDTO);

    //  string mensagemValidacao;
    //  if (!pedido.EValidoParaAtualizar(out mensagemValidacao))
    //  {
    //    return new RetornoAtualizarPedido() { Sucesso = false, Mensagem = mensagemValidacao };
    //  }

    //  _repositorioPedido.AtualizarPedido(pedido);

    //  return new RetornoAtualizarPedido() { Sucesso = true, Id = pedido.Id };
    //}

    //public List<PedidoDTO> BuscarPedidos()
    //{
    //  var pedidos = _repositorioPedido.BuscarPedidos();

    //  var listaRetorno = new List<PedidoDTO>();

    //  pedidos.ForEach(pedido =>
    //  {
    //    listaRetorno.Add(Mapper.Map<PedidoDTO>(pedido));
    //  });

    //  return listaRetorno;
    //}



  }
}
