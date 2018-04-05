using Aplicacao.DTO;
using AutoMapper;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao
{
  public class MapperInit
  {
    public static void MapearDTOs()
    {
      Mapper.Initialize(map =>
      {
        map.CreateMap<EnderecoDTO, Endereco>().ReverseMap();
        map.CreateMap<ClienteDTO, Cliente>().ReverseMap();
        map.CreateMap<ItemDTO, Item>().ReverseMap();
        map.CreateMap<PedidoDTO, Pedido>().ReverseMap();
      });
    }
  }
}
