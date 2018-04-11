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
update Item set descricao = @descricao, valor = @valor where id = @id and versao = @versao";

    private const string queryBuscaItens = @"
select id, descricao, valor, versao from Item";

    private const string queryBuscaItemPoId = queryBuscaItens + @" where id = @id and versao = isnull(convert(binary(8),@versao), versao)";

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

    public async Task<bool> AtualizarItemAsync(Item item)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        await connection.OpenAsync();

        using (SqlCommand command = new SqlCommand(queryUpdatetItem, connection))
        {
          command.Parameters.Add(new SqlParameter("@descricao", item.Descricao));
          command.Parameters.Add(new SqlParameter("@valor", item.Valor));
          command.Parameters.Add(new SqlParameter("@id", item.Id));
          command.Parameters.Add(new SqlParameter("@versao", item.Versao));
          var numeroLinhasAfetadas = await command.ExecuteNonQueryAsync();
          return numeroLinhasAfetadas > 0;
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
            listaRetorno.Add(LerItem(rd));
          }
        }
      }
      return listaRetorno;
    }

    private static Item LerItem(SqlDataReader rd)
    {
      return new Item()
      {
        Id = Convert.ToInt32(rd["id"]),
        Descricao = Convert.ToString(rd["descricao"]),
        Valor = Convert.ToDecimal(rd["valor"]),
        Versao = (byte[])rd["versao"]
      };
    }

    public async Task<Item> BuscarItemAsync(int id, byte[] versao)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        await connection.OpenAsync();

        using (SqlCommand command = new SqlCommand(queryBuscaItemPoId, connection))
        {
          command.Parameters.Add(new SqlParameter("@id", id));

          if (versao != null)
            command.Parameters.Add(new SqlParameter("@versao", versao) { SqlDbType = System.Data.SqlDbType.VarBinary });
          else
            command.Parameters.Add(new SqlParameter("@versao", DBNull.Value));

          var rd = await command.ExecuteReaderAsync();

          if (await rd.ReadAsync())
          {
            return LerItem(rd);
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
