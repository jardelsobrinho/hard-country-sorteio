using System.Data.SQLite;
using System.Data;

namespace ServerSorterio.Api.Infra;

public class ConexaoHelper
{
    private static SQLiteConnection? sqliteConnection;
    private static readonly string LocalDatabase = "D:\\Work\\MotorsFestSorteio\\data\\Sorteio.sqlite";
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
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS Sorteio(numero int)";
                cmd.ExecuteNonQuery();
            }
        }
        catch
        {
            throw;
        }
    }
    
    public static DataTable RetornaNumerosSorteados()
    {
        SQLiteDataAdapter? da = null;
        DataTable dt = new();
        try
        {
            using var cmd = DbConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM Sorteio";
            da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
            da.Fill(dt);
            return dt;
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
            cmd.CommandText = $"SELECT * FROM Sorteio where numero = {numero}";
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
            cmd.CommandText = "INSERT INTO Sorteio(numero) values (@numero)";
            cmd.Parameters.AddWithValue("@numero", numeroSorteado);
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static void ExcluirNumeroSorteado(int numero)
    {
        var numeroJaExiste = RetornaNumero(numero);
        if (numeroJaExiste.Rows.Count == 0)
        {
            throw new BadHttpRequestException($"O número {numero} não existe.");
        }

        try
        {
            using var cmd = new SQLiteCommand(DbConnection());
            cmd.CommandText = "DELETE FROM Sorteio Where numero=@numero";
            cmd.Parameters.AddWithValue("@numero", numero);
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
