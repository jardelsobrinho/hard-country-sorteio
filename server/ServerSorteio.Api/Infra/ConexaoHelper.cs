using ServerSorteio.Api.Domain;
using System.Data;
using System.Data.SQLite;

namespace ServerSorteio.Api.Infra;

public class ConexaoHelper
{
    private static SQLiteConnection? sqliteConnection;
    public static string LocalDatabase = "D:\\Work\\MotorsFestSorteio\\data\\Sorteio.sqlite";
    public ConexaoHelper()
    { }
    private static SQLiteConnection DbConnection()
    {
        sqliteConnection = new SQLiteConnection($"Data Source={LocalDatabase}; Version=3;");
        sqliteConnection.Open();
        return sqliteConnection;
    }
    public static void CriarBancoSQLite()
    {
        try
        {
            if (!File.Exists(LocalDatabase))
            {
                SQLiteConnection.CreateFile($"{LocalDatabase}");

                using var cmd = DbConnection().CreateCommand();
                cmd.CommandText = "CREATE TABLE NumerosSorteados(numero int primary key, vencedor text)";
                cmd.ExecuteNonQuery();
            }
        }
        catch
        {
            throw;
        }
    }

    public static IList<NumeroSorteadoEntity> RetornaNumerosSorteados()
    {
        var listaNumerosSorteados = new List<NumeroSorteadoEntity>();
        DataTable dt = new();
        try
        {
            using var cmd = DbConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM NumerosSorteados ORDER BY numero";
            SQLiteDataAdapter? da = new(cmd.CommandText, DbConnection());
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                var numeroSorteado = new NumeroSorteadoEntity()
                {
                    Numero = Convert.ToInt32(dr["numero"]),
                    Vencedor = dr["vencedor"].ToString() == "S"
                };
                listaNumerosSorteados.Add(numeroSorteado);
            };

            return listaNumerosSorteados;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static bool JaExisteNumeroVencedor()
    {
        DataTable dt = new();
        try
        {
            using var cmd = DbConnection().CreateCommand();
            cmd.CommandText = $"SELECT * FROM NumerosSorteados where vencedor = 'S'";
            SQLiteDataAdapter? da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
            da.Fill(dt);

            return dt.Rows.Count > 0;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static DataTable RetornaNumero(int numero)
    {
        DataTable dt = new();
        try
        {
            using var cmd = DbConnection().CreateCommand();
            cmd.CommandText = $"SELECT * FROM NumerosSorteados where numero = {numero}";
            SQLiteDataAdapter? da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
            da.Fill(dt);
            return dt;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static void AdicionaNumero(int numeroSorteado)
    {
        if (JaExisteNumeroVencedor())
        {
            throw new BadHttpRequestException($"Já existe um número vencedor");
        }

        if (numeroSorteado < 1)
        {
            throw new BadHttpRequestException($"O número deve ser maior ou igual a 1");
        }

        if (numeroSorteado > 300)
        {
            throw new BadHttpRequestException($"O número deve ser menor ou igual a 300");
        }

        var numeroJaExiste = RetornaNumero(numeroSorteado);
        if (numeroJaExiste.Rows.Count > 0)
        {
            throw new BadHttpRequestException($"O número {numeroSorteado} já foi sorteado.");
        }
        try
        {
            using var cmd = DbConnection().CreateCommand();
            cmd.CommandText = "INSERT INTO NumerosSorteados(numero, vencedor) values (@numero, 'N')";
            cmd.Parameters.AddWithValue("@numero", numeroSorteado);
            cmd.ExecuteNonQuery();

            var numeroGanhador = RetornaNumeroGanhador();
            if (numeroGanhador > 0)
            {
                cmd.CommandText = "INSERT INTO NumerosSorteados(numero, vencedor) values (@numero, 'S')";
                cmd.Parameters.AddWithValue("@numero", numeroGanhador);
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static int RetornaNumeroGanhador()
    {
        var numerosSorteados = RetornaNumerosSorteados();
        if (numerosSorteados.Count >= 299)
        {
            for (int numero = 1; numero <= 300; numero++)
            {
                var consulta = numerosSorteados.Where(x => x.Numero == numero).ToList();

                if (consulta.Count == 0)
                {
                    return numero;
                }
            }
        }
        return 0;
    }

    public static void ExcluirNumeroSorteado(int numero)
    {
        if (JaExisteNumeroVencedor())
        {
            throw new BadHttpRequestException($"Já existe um número vencedor");
        }

        var numeroJaExiste = RetornaNumero(numero);
        if (numeroJaExiste.Rows.Count == 0)
        {
            throw new BadHttpRequestException($"O número {numero} não existe.");
        }

        try
        {
            using var cmd = new SQLiteCommand(DbConnection());
            cmd.CommandText = "DELETE FROM NumerosSorteados Where numero=@numero";
            cmd.Parameters.AddWithValue("@numero", numero);
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
