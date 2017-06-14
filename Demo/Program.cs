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
        static void Main1(string[] args)
        {
            #region 插入数据


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

            /*  SqlConnection con = new SqlConnection(strConn);
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
            /* SqlCommand cmd = new SqlCommand(strSql, con);
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
     }*/
            #endregion

            #region 修改语句
            /*   //创建一个连接对象
               SqlConnection con = new SqlConnection(strConn);
               //编写sql语句
               string sql = "update";
               //创建命令对象
               SqlCommand cmd = new SqlCommand(sql, con);
               //打开连接
               con.Open();
               //发送语句
               int count = cmd.ExecuteNonQuery();
               if (count > 0)
               {
                   Console.WriteLine("执行成功");
               }
               else if (count == 0)
               {
                   Console.WriteLine("执行失败");
               }*/
            #endregion

            #region 返回单个值(ExecuteScalar)
            /*  SqlConnection con = new SqlConnection(strConn);
              string sql = "select count(*) from Students";
              SqlCommand cmd = new SqlCommand(sql, con);
              con.Open();
             object count= cmd.ExecuteScalar();//返回单个结果
              int result = (int)count;//转换为int类型
              con.Close();
              Console.WriteLine(result);*/
            #endregion

            #region usring{}释放对象
            /*usring的用法
             1.usring用于引入命名空间
             2.usring{}用于释放对象
             3.usring{}代表除了作用域之后,会默认调用当前对象的dispose()方法,将ConnectionString设置为空
             dispose是对于对象自身而言的,close()是对于数据库本身而言的*/

            /*使用usring表示在除了作用域之后,自动调用dispose方法,释放对象,usring只能用在实现了IDisposeable接口的类上
             * 括号里定义的con只在usring{}这对括号内有效,除了后就没用了
             * 
             * usring和try-catch finally区别
             * 共同:都可以释放资源
             * 不同:usring在超出返回后主动释放资源,try-catch finally需要写释放代码
             * 总结:如果不需要捕获异常,可以使用usring,否则用try-catch finally
             
             */
            /*  using (SqlConnection con = new SqlConnection(strConn))
              {

                  con.Open();
                  con.Close(); 
              }*/
            #endregion

            #region close()和dispose()的区别
            /*  SqlConnection con = new SqlConnection(strConn);
              con.Open();
              con.Close();*/
            #endregion


            #region usring{}案例
            /*   using (SqlConnection con =new SqlConnection(strConn))
               {
                   string sql = "select stuname from students where stuid='s1001'";
                   using (SqlCommand cmd = new SqlCommand(sql,con))
                   {
                       con.Open();
                       string strname = Convert.ToString(cmd.ExecuteScalar());
                       Console.WriteLine(strname);
                   }

               }*/
            #endregion


            #region ExecuteDataReader()返回多条记录(多行多列)
            /*    using (SqlConnection con = new SqlConnection(strConn))
                {
                    string sql = @"select stuid,stuname,stuage,deptname
                                        from students stu inner join department dep 
                                        on stu.deptid=dep.deptid";
                    using (SqlCommand cmd = new SqlCommand(sql,con))
                    {
                        con.Open();
                      SqlDataReader dr=  cmd.ExecuteReader();//查询多行多列的结果,返回的是sqlDatareader的对象
                          /*1.cmd.ExecuteReader()查询的是数据库服务器中的数据,先保存到内存中
                      * 2.创建一个sqldatareadr对象dr来接收内存中的数据
                      * 3.读取dr中的数据,采用只读,只进的方式来读取数据,并且是一条一条的读取
                      * 4.读取之前,先判断dr中有没有数据,使用hasrow这个属性来判断是否存在数据,如果有数据,返回true
                      * 5.使用while循环来读取,具体实现采用dr.read()这个方法来读取,如果存在数据,则返回true,继续执行下一条信息,否则返回false,结束
                      * 6.使用for循环将每一列每一行的数据读取出来
                                */
            /* if (dr.HasRows)
             {
                 while (dr.Read())
                 {
                    /* for (int i = 0; i <dr.FieldCount; i++)
                     {
                         Console.Write(dr[i].ToString() + "\t");
                         Console.WriteLine();
                     }*/

            /*
          }
      }
  }*/

            #endregion

            #region 泛型集合接收数据
            List<Student> lstudent = new List<Student>();//定义一个泛型集合,来存放从dr中获取的数据
             
              using (SqlConnection con = new SqlConnection(strConn))
               {
                   string sql = @"select stuid,stuname,stusex,stuage,deptid
                                       from students";
                   using (SqlCommand cmd = new SqlCommand(sql, con))
                   {
                       con.Open();
                       SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //通过列名
                            /*  Student stu = new Student();
                              stu.StuId = dr["stuid"].ToString();
                              stu.StuName = dr[1].ToString();
                              stu.StuSex = Convert.ToBoolean(dr["stusex"]);
                              stu.StuAge = Convert.ToInt32(dr["stuage"]);
                              stu.DeptId = Convert.ToInt32(dr["deptid"]);*/

                            //通过索引
                            /* Student stu = new Student();
                             stu.StuId = dr[0].ToString();
                             stu.StuName = dr[1].ToString();
                             stu.StuSex = Convert.ToBoolean(dr[2]);
                             stu.StuAge = Convert.ToInt32(dr[3]);
                             stu.DeptId = Convert.ToInt32(dr[4]);*/

                            Student stu = new Student();
                            stu.StuId = dr.GetString(0);
                            stu.StuName = dr.GetString(1);
                            stu.StuSex = dr.GetBoolean(2);
                            stu.StuAge = dr.GetInt32(3);
                            stu.DeptId = dr.GetInt32(4);
                            lstudent.Add(stu);//将student对象添加到  List<Student> 中
                        }
                        dr.Close();
                    }
                    //循环遍历
                    foreach (Student stu in lstudent)
                    {
                        Console.WriteLine(stu.StuId+"\t"+stu.StuName+"\t"+(stu.StuSex==true?"男":"女")+"\t"+stu.StuAge+"\t"+stu.DeptId);
                    }
                   }
               }
            #endregion
            #region 练习
            /*using (SqlConnection con = new SqlConnection(strConn))
            {
                string sql = @"select
                                                    stu.stuid,
                                                    stuname,
                                                    stusex,
                                                    deptname,
                                                    examscore,
                                                    coursename
                                      from       Students stu inner join Score sco on stu.stuid=sco.StuId inner join Department dep
                                                    on stu.DeptId=dep.DeptId inner join Course cou on cou.CourseId=sco.CourseId";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
               SqlDataReader sr= cmd.ExecuteReader();
                if (sr.HasRows)
                {
                    while (sr.Read())
                    {
                        for (int i = 0; i < sr.FieldCount; i++)
                        {
                            Console.Write(sr[i]+"\t");
                          
                        }
                              Console.WriteLine();
                    }
                }
                con.Close();
            }*/
            #endregion
        }
    }
}
  
   
/*总结:
 ExecuteNonQuery()执行增删改操作,返回受影响行数
 ExecuteScalar()用于执行查询单个结果,一行一列,返回是object类型
 
    executeReader()特点
    1.只读,只进,只能通过sqldatareader()对象dr读取数据
    不能修改数据,只能向前读取数据,不能向后,也不能跳跃
    2.使用sqldatareader对象dr时,必须保证连接打开状态,使用dr后,必须关闭dr*/
     
     