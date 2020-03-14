using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;

namespace ZMCMSv2HIService
{
    public partial class Service1 : ServiceBase
    {
        private Timer MyTimer;
        public string sStartDt, sEndDt;

        public Service1()
        {
            InitializeComponent();

            this.AutoLog = false;
            if (!System.Diagnostics.EventLog.SourceExists("ZMCMSv2HISerivce"))
            {
                System.Diagnostics.EventLog.CreateEventSource("ZMCMSv2HISerivce", "HILog");
            }

            eventLog1.Source = "ZMCMSv2HISerivce";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Start Timer.");

            MyTimer = new Timer();
            MyTimer.Elapsed += new ElapsedEventHandler(MyTimer_Elapsed);
            MyTimer.Interval = 10 * 1000;
            MyTimer.Start();
        }

        protected override void OnStop()
        {
            sEndDt = DateTime.Now.ToString();

            eventLog1.WriteEntry("資料轉檔描述:" + sStartDt + "開始執行轉檔作業，在 " + sEndDt + " 完成轉檔作業 !");

            
            MyTimer = null;
        }

        private void MyTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            MyTimer.Stop();

            sStartDt = DateTime.Now.ToString();

            // 檢查 UploadServer 是否有未完成轉檔的資料存在
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = "oy.asuscomm.com,36913";
            csb.InitialCatalog = "ZMCMSv2_Sys";            
            csb.IntegratedSecurity = false;
            csb.UserID = "ZMSys";
            csb.Password = "z1r0m1d!@#$";

            string connString = csb.ToString();                       
            string queryString = "select us.* from UploadServer us where us.USServerStatus='S' order by us.USLoadDateTime";

            using (SqlConnection connection = new SqlConnection(connString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = queryString;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // 
                        string sUSRowid = reader["USRowid"].ToString();
                        string sUSHospRowid = reader["USHospRowid"].ToString();
                        string sUSLoadFilename = reader["USLoadFilename"].ToString();

                        // 取得該主機的實際位置
                        // 將取得的主機機位置解密
                        // 產生連結資料
                        // 


                        // 執行轉檔動作

                    }
                }
            }
        }
    }
}
