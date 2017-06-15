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
            /* Console.WriteLine("请输入用户名");
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
            /* using (SqlConnection con = new SqlConnection(strConn))
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
            /*  SqlParameter[] param = new SqlParameter[]
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

          } */
            #endregion

            #region 带有参数的sql操作
            /*  using (SqlConnection con = new SqlConnection(strConn))
              {
                  string sql = @"insert into Course(CourseId,
                                                                      CourseName,
                                                                      Credit) 
                                                          values(@Courseid,
                                                                     @CourseName,
                                                                     @Credit)";
                  string couresid = "C006";
                  string coursename = "c#";
                  int credit = 10;
                  SqlParameter[] parm = new SqlParameter[]
                  {
                      new SqlParameter("@CourseId",couresid),
                      new SqlParameter("@CourseName",coursename),
                      new SqlParameter("@Credit",credit)
                  };
                  SqlCommand cmd = new SqlCommand(sql, con);
                  cmd.Parameters.AddRange(parm);
                  con.Open();
                  int count = cmd.ExecuteNonQuery();
                  if (count > 0)
                  {
                      Console.WriteLine("执行成功");
                  }
                  else
                  {
                      Console.WriteLine("执行失败");
                  }
              }*/
            #endregion

            #region 执行带有参数的存储过程(input output)
            /* Console.WriteLine("课程编号:");
             string courseid = Console.ReadLine();
             using (SqlConnection con = new SqlConnection(strConn))
             {
                 SqlCommand cmd = new SqlCommand("PROC_CourseAvg", con);

                 SqlParameter[] parm = new SqlParameter[]
                 {
                     new SqlParameter("@courseid",courseid),
                     //指定该参数为输出参数
                     new SqlParameter("@result",SqlDbType.VarChar,20) { Direction=ParameterDirection.Output}

                 };
                 cmd.Parameters.AddRange(parm);
                 //指定命令对象为储存过程
                 cmd.CommandType = CommandType.StoredProcedure;
                 con.Open();
                cmd.ExecuteNonQuery();
                 //接收输出参数的值
                 string result =Convert.ToString(parm[1].Value);
                 Console.WriteLine("考试结果:"+result);
             }*/
            #endregion

            #region 带有返回值的储存过程
            /* Console.WriteLine("课程编号:");
             string courseid = Console.ReadLine();
             using (SqlConnection con = new SqlConnection(strConn))
             {
                 SqlCommand cmd = new SqlCommand("PROC_ReturnValue", con);
                 SqlParameter[] parm= new SqlParameter[] 
                 {
                     new SqlParameter("@courseid",courseid),
                     new SqlParameter("@result",SqlDbType.VarChar,20) { Direction=ParameterDirection.Output},
                     new SqlParameter("@return",SqlDbType.Float,10) {  Direction=ParameterDirection.ReturnValue}

                 };
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddRange(parm);
                 con.Open();
                 cmd.ExecuteNonQuery();
                 string result = Convert.ToString(parm[1].Value);
                 float score = Convert.ToSingle(parm[2].Value);
                 Console.WriteLine("本次考试:{0},平均分{1}",result,score);
             }*/
            #endregion

            #region 断开连接类
            //1.通过连接字符串查询写语句
            //编写sql语句
          //  string sql = "select * from Students";
            //创建 SqlDataAdapter对象
            //SqlDataAdapter da = new SqlDataAdapter(sql, strConn);
            using (SqlConnection con = new SqlConnection(strConn))
            {
                string strsql = "select * from students";
                
                //SqlDataAdapter是;连接数据源和数据集的中间桥梁,也称为数据适配器
                //其中有个Fill()方法,是用来填充数据集,此时就断开了连接,将结果保存到数据集中
                //DataSet:数据集,也可以理解成脱机数据库,里面可以添加多个DataTable
                //DataSet他是存放于服务器的内存当中

                //创建一个 SqlDataAdapter对象
                SqlDataAdapter da = new SqlDataAdapter(strsql, con);
                SqlDataAdapter da1 = new SqlDataAdapter(strsql, con);
                con.Open();
                //创建一个DataSet的实例
                DataSet ds = new DataSet();
                //da.Fill(ds);
                da.Fill(ds, "StuTable");//将查询结果填充到数据集中(ds),并指定一个虚拟的表StuTable用来存放数据
                DataTable dt = ds.Tables["StuTable"];
                for (int i = 0; i<dt.Rows.Count; i++)//循环每一行
                {
                  //  DataRow dr = dt.Rows[i];//取出每行数据
                    for (int j = 0; j < dt.Columns.Count; j++)//循环每一列
                    {
                        Console.Write(dt.Rows[i][j] +"\t");//取出当前行每一列的数据
                    }
                    Console.Write("\n");
                }
                Console.WriteLine();
            }
           
            #endregion
        }
    }
}
 