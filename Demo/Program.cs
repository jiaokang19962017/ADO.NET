using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Demo
{
    class Program
    {

   
      
        static void Main(string[] args)
        {
            #region 应用程序配置
      /*
       从app.config(应用配置文件)中读取连接字符串
       1.需要导入System.Configruation.dll文件
       2.引入System.Configruation命名空间
       3.使用ConfigruationManager.ConnectionString["strConn"].ToString()来读取字符串
           */


            #endregion
            //从配置文件app.config中获取连接字符串
            string strConn = ConfigurationManager.ConnectionStrings["strConn"].ToString();
            #region 连接字符串
            //获取连接字符串方式
            /*1.Data Soucre:服务器名称
             * Initial Catalog:数据库名称
             *Integrated Security=SSPI:采用windows身份认证模式(集成安全)
             2.SQL server身份验证
             需要指定用户名和密码
             */

            //创建连接对象
            //打开指定的数据源
            #endregion
            //手动书写连接字符串
       
            //string strConn = "Data Source=HP201-1;Initial Catalog=Student;Integrated Security=SSPI;";----------------windows认证连接方式
            //string strConn1 = "Data Source=(localhost);Inital Catalog=Student;User ID=sa;Password=''123";--------------sql身份认证方式
            
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
