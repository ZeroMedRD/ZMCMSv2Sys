using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using HIUConsole.Model;

namespace HIUConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ZMCMSv2_SysEntities db_zmcmsv2 = new ZMCMSv2_SysEntities();
            ZMCMSEntities db_zmcms = new ZMCMSEntities();

            var tb_us = (from us in db_zmcmsv2.UploadServer orderby us.USLoadDateTime where us.USServerStatus=="S" 
                         select new { us.USRowid, us.USHospRowid, us.USLoadFilename });

            foreach (var r in tb_us)
            {
                //Console.WriteLine(r.USRowid.ToString());
                //Console.WriteLine(r.USHospRowid.ToString());
                //Console.WriteLine(r.USLoadFilename.ToString());
                //Console.ReadLine();

                // 取得醫事機構代碼
                var tb_sh = (from sh in db_zmcms.SysHospital where sh.HospRowid == r.USHospRowid.ToString()
                             select new { sh.HospID }).First();

                Console.WriteLine(tb_sh.HospID.ToString());
                Console.ReadLine();
            }


            // 檢查 UploadServer 是否有未完成轉檔的資料存在
            //SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            //csb.DataSource = "oy.asuscomm.com,36913";
            //csb.InitialCatalog = "ZMCMSv2_Sys";
            //csb.IntegratedSecurity = false;
            //csb.UserID = "ZMSys";
            //csb.Password = "z1r0m1d!@#$";

            //string connString = csb.ToString();
            //string queryString = "select us.* from UploadServer us where us.USServerStatus='S' order by us.USLoadDateTime";

            //using (SqlConnection connection = new SqlConnection(connString))
            //using (SqlCommand command = connection.CreateCommand())
            //{
            //    command.CommandText = queryString;

            //    connection.Open();

            //    using (SqlDataReader reader = command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            // 
            //            string sUSRowid = reader["USRowid"].ToString();
            //            string sUSHospRowid = reader["USHospRowid"].ToString();
            //            string sUSLoadFilename = reader["USLoadFilename"].ToString();

            //            // 取得該主機的實際位置

            //            // 將取得的主機機位置解密
            //            // 產生連結資料
            //            // 


            //            // 執行轉檔動作
            //            Console.WriteLine(reader["USRowid"].ToString());
            //            Console.ReadLine();
            //        }
            //    }
            //}
        }
    }
}
