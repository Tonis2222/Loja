using Dominio;
using Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace InfraSQLServer
{
  public class RepositorioItemSQLServer : IRepositorioItem
  {
    private const string queryInsertItem = @"
insert into Item (descricao, valor)
values(@descricao, @valor)
select @id = @@identity";

    private const string queryUpdatetItem = @"
update Item set descricao = @descricao, valor = @valor where id = @id";

    private const string queryBuscaItens = @"
select id, descricao, valor from Item";

    private string connectionString;
    public RepositorioItemSQLServer(string connectionString)
    {
      this.connectionString = connectionString;

    }

    public void CadastrarItem(Item item)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();

        using (SqlCommand command = new SqlCommand(queryInsertItem, connection))
        {
          command.Parameters.Add(new SqlParameter("@descricao", item.Descricao));
          command.Parameters.Add(new SqlParameter("@valor", item.Valor));
          var paramId = new SqlParameter("@id", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };
          command.Parameters.Add(paramId);
          command.ExecuteNonQuery();
          item.Id = Convert.ToInt32(paramId.Value);
        }
      }
    }

    public void AtualizarItem(Item item)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();

        using (SqlCommand command = new SqlCommand(queryUpdatetItem, connection))
        {
          command.Parameters.Add(new SqlParameter("@descricao", item.Descricao));
          command.Parameters.Add(new SqlParameter("@valor", item.Valor));          
          command.Parameters.Add(new SqlParameter("@id", item.Id));
          var numeroLinhasAfetadas = command.ExecuteNonQuery();
        }
      }
    }

    public List<Item> BuscarItens()
    {
      List<Item> listaRetorno = new List<Item>();

      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();

        using (SqlCommand command = new SqlCommand(queryBuscaItens, connection))
        {
          var rd = command.ExecuteReader();

          while (rd.Read())
          {
            listaRetorno.Add(new Item()
            {
              Id = Convert.ToInt32(rd["id"]),
              Descricao = Convert.ToString(rd["descricao"]),
              Valor = Convert.ToDecimal(rd["valor"])
            });
          }
        }
      }
      return listaRetorno;
    }

  }
}
