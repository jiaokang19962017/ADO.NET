using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Demo
{
    class Start
    {
        private static readonly string strConn = ConfigurationManager.ConnectionStrings["strConn"].ToString();
        static void Main(string[] args)
        {
            Console.WriteLine("请输入用户名");
            string username = Console.ReadLine();
            Console.WriteLine("请输入密码:");
            string password = Console.ReadLine();
           /* using (SqlConnection con = new SqlConnection(strConn))
            {
                string sql = "select count(*) from Users where UserId='"+username+"' and Password='"+password+"'";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    Console.WriteLine("登录成功");
                }
                else
                {
                    Console.WriteLine("登录失败");
                }*/
           

            #region 防止sql注入攻击
            using (SqlConnection con = new SqlConnection(strConn))
            {
                string sql = "select count(*) from Users where Userid=@userid and password=@password";
                SqlCommand cmd = new SqlCommand(sql, con);

                //创建一个sqlparmeter对象用来设置参数名称,参数类型,参数长度,参数值
                /* SqlParameter param = new SqlParameter("@userid", SqlDbType.VarChar, 20) { Value=username};
                 SqlParameter param1 = new SqlParameter("@password", SqlDbType.VarChar, 20) { Value = password };
                 cmd.Parameters.Add(param);
                 cmd.Parameters.Add(param1);*/

                //第二种方式
                /*  cmd.Parameters.Add("@userid", SqlDbType.VarChar, 20);
                  cmd.Parameters[0].Value = username;
                  cmd.Parameters.Add("@password", SqlDbType.VarChar, 20);
                  cmd.Parameters[1].Value = password;*/

                //第三种方式
                /* cmd.Parameters.AddWithValue("@userid", username);
                 cmd.Parameters.AddWithValue("@password", password);*/

                //第四种方式
                SqlParameter[] param = new SqlParameter[]
                {
                  //  new SqlParameter("@urseid",SqlDbType.VarChar,20) { Value=username},
                    //new SqlParameter("@password",SqlDbType.VarChar,20) { Value=password},
                    new SqlParameter("@userid",username),
                    new SqlParameter("@password",password),
                };
                cmd.Parameters.AddRange(param);

                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    Console.WriteLine("登录成功");
                }
                else
                {
                    Console.WriteLine("登录失败");
                }
                
            }
            #endregion
        }
        }
}
 