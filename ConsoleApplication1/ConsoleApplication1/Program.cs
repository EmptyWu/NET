using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace ConsoleApplication1
{
    class Program
    {
        static string connstr = @"Server=localhost;Database=SampleDB;User Id=postgres;Password=1qaz@WSX;";
        static void Main(string[] args)
        {
         //執行目前test筆數
            string SQL = @"select * from test";
            Console.WriteLine("日前比數:" + GetAll(SQL).Rows.Count);

            Console.ReadLine();

            //測試新增資料
            //若成功會回傳執行筆數
            SQL = "insert into test(c1 )values ('c2');";
            Console.WriteLine("新增結果:" + ExecNonQuery(SQL));
            Console.ReadLine();
            //刪除修改也是類似的寫法呼叫
        }

        //資料庫 為本機資料庫
        //DataBase:SampeDB
        //TableName:test
        //ColumnNmae:c1

        /// <summary>
        /// 建立取得資料方式
        /// </summary>
        /// <param name="__SQLCommand"></param>
        /// <returns></returns>
        static DataTable GetAll(string _SQLCommand)
        {
            DataTable oDataTable = new DataTable();
            using (NpgsqlConnection conn = new NpgsqlConnection(connstr))
            {
                conn.Open();

                NpgsqlDataAdapter oAdapter = new NpgsqlDataAdapter(_SQLCommand, conn);
                oAdapter.Fill(oDataTable);
                if (oAdapter != null)
                    oAdapter.Dispose();
            }
            return oDataTable;
        }

        /// <summary>
        /// 建立Insert,Update,Delete 方式
        /// </summary>
        /// <param name="_SQLCommand"></param>
        /// <returns></returns>
        static int ExecNonQuery(string _SQLCommand)
        {
            int result = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection(connstr))
            {
                connection.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(_SQLCommand, connection);
                cmd.CommandType = CommandType.Text;

                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                connection.Close();
            }

            return result;
        }

    }
}
