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

     //设置为只读
      private static readonly string strConn = ConfigurationManager.ConnectionStrings["strConn"].ToString();
        static void Main(string[] args)
        {
            #region 应用程序配置
      /*
       从app.config(应用配置文件)中读取连接字符串
       1.需要导入System.Configruation.dll文件
       2.引入System.Configruation命名空间
       3.使用ConfigruationManager.ConnectionString["strConn"].ToString()来读取字符串
       4.通常将连接字符串的变量设置为只读
           */


            #endregion
            //从配置文件app.config中获取连接字符串
            //string strConn = ConfigurationManager.ConnectionStrings["strConn"].ToString();
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
                Console.WriteLine("请输入要更新系别名称");
                string strdeptname = Console.ReadLine();
                Console.WriteLine("请输入新的系别名称");
                string newstrdeptname = Console.ReadLine();
                if (!string.IsNullOrEmpty(strdeptname))//判断非空
                {
                    string strSql = string.Format("update  Department set deptname='{0}' where deptname='{1}' ", newstrdeptname,strdeptname);
                    /*SQL Command构造函数有两个参数(查询文本,连接对象)
                     查询文本:要执行的sql语句(do)
                     连接对象:指定连接哪个数据库(从哪个数据源中操作)
                     总结:sqlcommand 创建一个命令对象,用来执行sql语句*/
                    SqlCommand cmd = new SqlCommand(strSql, con);
                    con.Open();//打开连接,连接最晚打开,最早关闭
                    int count = cmd.ExecuteNonQuery();//执行()新增,修改,删除)语句,返回受影响行数
                    Console.WriteLine("成功修改{0}行记录", count);
                }
                else {
                    Console.WriteLine("为空,重新输入");
                    Console.ReadKey();
                }
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
   