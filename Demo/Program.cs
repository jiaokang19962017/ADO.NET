using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 连接字符串

            #endregion
            //获取连接字符串方式
            /*1.Data Soucre:服务器名称
             * Initial Catalog:数据库名称
             *Integrated Security=SSPI:采用windows身份认证模式(集成安全)
             2.SQL server身份验证
             需要指定用户名和密码

             */
            string strConn = "Data Source=HP201-1;Initial Catalog=Student;Integrated Security=SSPI;";

            string strConn1 = "Data Source=(localhost);Inital Catalog=Student;User ID=sa;Password=''123";
            
            //创建连接对象
            //打开指定的数据源
            SqlConnection con = new SqlConnection(strConn);
            try
            {
                con.Open();//打开连接
         
                Console.WriteLine("打开连接");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();//关闭连接
            }
        }
    }
}
