using Dominio;
using Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

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

    private const string queryBuscaItemPoId = queryBuscaItens + @" where id = @id";

    private string connectionString;
    public RepositorioItemSQLServer(string connectionString)
    {
      this.connectionString = connectionString;

    }

    public async Task CadastrarItemAsync(Item item)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        await connection.OpenAsync();

        using (SqlCommand command = new SqlCommand(queryInsertItem, connection))
        {
          command.Parameters.Add(new SqlParameter("@descricao", item.Descricao));
          command.Parameters.Add(new SqlParameter("@valor", item.Valor));
          var paramId = new SqlParameter("@id", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };
          command.Parameters.Add(paramId);
          await command.ExecuteNonQueryAsync();
          item.Id = Convert.ToInt32(paramId.Value);
        }
      }
    }

    public async Task AtualizarItemAsync(Item item)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        await connection.OpenAsync();

        using (SqlCommand command = new SqlCommand(queryUpdatetItem, connection))
        {
          command.Parameters.Add(new SqlParameter("@descricao", item.Descricao));
          command.Parameters.Add(new SqlParameter("@valor", item.Valor));
          command.Parameters.Add(new SqlParameter("@id", item.Id));
          var numeroLinhasAfetadas = await command.ExecuteNonQueryAsync();
        }
      }
    }

    public async Task<List<Item>> BuscarItensAsync()
    {
      List<Item> listaRetorno = new List<Item>();

      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        await connection.OpenAsync();

        using (SqlCommand command = new SqlCommand(queryBuscaItens, connection))
        {
          var rd = await command.ExecuteReaderAsync();

          while (await rd.ReadAsync())
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

    public async Task<Item> BuscarItemAsync(int id)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        await connection.OpenAsync();

        using (SqlCommand command = new SqlCommand(queryBuscaItemPoId, connection))
        {
          command.Parameters.Add(new SqlParameter("@id", id));

          var rd = await command.ExecuteReaderAsync();

          if (await rd.ReadAsync())
          {
            return new Item()
            {
              Id = Convert.ToInt32(rd["id"]),
              Descricao = Convert.ToString(rd["descricao"]),
              Valor = Convert.ToDecimal(rd["valor"])
            };
          }
          else
          {
            return null;
          }
        }
      }
    }
  }
}
