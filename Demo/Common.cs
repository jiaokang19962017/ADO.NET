using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace Demo
{
    public static class Common
    {
        /// <summary>
        /// 读取;连接字符串
        /// </summary>
        private static readonly string strConn = ConfigurationManager.ConnectionStrings["strConn"].ToString();

        private static SqlTransaction tran;
        private static SqlConnection conn;
        /// <summary>
        /// 创建连接对象,并打开连接
        /// </summary>
        public static SqlConnection Conn
        {
            get
            {
                if (conn == null)
                {
                    conn = new SqlConnection(strConn);//创建连接对象
                    conn.Open();
                }
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (conn.State == ConnectionState.Broken)
                {
                    conn.Close();
                    conn.Open();
                }
                return conn;
            }
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        public static void BeginTransaction()
        {
            tran = Conn.BeginTransaction();
            Close();

        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public static void CommitTransaction()
        {
            tran.Commit();
            Close();

        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public static void RollBackTranscation()
        {
            tran.Rollback();
            Close();
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public static void Close()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parm">传递的参数</param>
        public static void ExecuteTransaction(string sql, params SqlParameter[] parm)//事务操作方法
        {

                SqlCommand cmd = new SqlCommand(sql, Conn);
                if (parm != null)
                {
                    cmd.Parameters.AddRange(parm);
                }
                cmd.ExecuteNonQuery();
 
        }
    }
}
