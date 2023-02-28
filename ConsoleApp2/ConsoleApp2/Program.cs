using System;
using System.Data;
using System.Data.OracleClient;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter from tour date 'yy-mm-dd'");
        string dateFrom = Console.ReadLine();
        Console.WriteLine("Please enter from tour date 'yy-mm-dd'");
        string dateTo = Console.ReadLine();
        string connectionString = "Data Source=PORACLE01:1521/Soloplan;User ID=sol;Password=sql;";
        string sql = $"select ATOUR.STARTSOLL, ATOUR.ENDSOLL, ATOUR.TOURNUMMER, SFUHRP.KENNZEICHEN as fritekstet_trailer_no, SPERSO.MATCHCODE as Main_Dispatcher from atour full join SFUHRP on atour.ANHAENGERKZ  = SFUHRP.KENNZEICHEN full join SPerso on atour.PERSONALID = SPerso.nr where atour.ANHAENGERNR IS NULL and SFUHRP.EIGFZG = 1 and STARTSOLL BETWEEN TO_DATE('{dateFrom}','yy-mm-dd') and TO_DATE('{dateTo}','yy-mm-dd') and ATOUR.TOURNUMMER > 0 order by ATOUR.STARTSOLL asc\r\n";

        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            connection.Open();

            using (OracleCommand command = new OracleCommand(sql, connection))
            {
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write("{0}\t", reader.GetName(i));
                    }
                    Console.WriteLine();

                    
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write("{0}\t", reader[i]);
                        }
                        Console.WriteLine();
                    }
                }
            }

            connection.Close();
            Console.WriteLine("That will be all");
            Console.ReadLine();
            
        } 
    }
}
