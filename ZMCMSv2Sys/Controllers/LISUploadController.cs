using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
//using System.Data.EntityClient;
using System.Data.Entity;
using System.Text;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ZMCMSv2Sys.ViewModels;
using ZMCMSv2Sys.Models;
using Newtonsoft.Json;
using ZMLib;

namespace ZMCMSv2Sys.Controllers
{
    public class LISUploadController : Controller
    {
        //private HISEntities db_his = new HISEntities();        
        private ZMCMSv2_SysEntities db_zmcmsv2_sys = new ZMCMSv2_SysEntities();

        ZMClass myClass = new ZMClass();

        public string dbs, ic, userid, password;

        private void GetLink()
        {
            // 依據使用者所屬的醫事機構代碼切換所屬資料庫            
            // dbs = [SQL Server IP,Port number]
            // InitialCatalog = Database Name
            // UserID = Login SQL Server User id
            // Password = Login SQL Server Password
            // 1. 從 Web.config 取得上面四個參數並且以 Json 方式傳遞
            string connectionParameters = myClass.GetParamFromWebConfig();

            // 2. 將取得的 Json 資料反序列化, 做反序列化需先定義 EFLink 類,它放在 DAO_SQLink.cs 裡
            EFLink eflink = JsonConvert.DeserializeObject<EFLink>(connectionParameters);

            // 3. 將反序列化後的資料解密並依屬性的值放在變數裡
            dbs = eflink.Server;
            ic = eflink.InitialCatalog;
            userid = eflink.UserID;
            password = eflink.Password;
        }

        public ActionResult GetLP([DataSourceRequest]DataSourceRequest request, string sHospID, string startDate, string endDate)
        {
            GetLink();

            // 4. 將解密後的值帶入至連結資料庫參數
            // 範例:HISEntities db_his = new HISEntities(myClass.GetSQLConnectionString(@"23.97.65.134,1933", "his" + sHospID, @"sa", @"I@ntif@t"));
            HISEntities db_his = new HISEntities(myClass.GetSQLConnectionString(dbs, "his" + sHospID, userid, password));
            
            DataSourceResult result = null;
            DateTime sdt = DateTime.Now;
            DateTime edt = DateTime.Now;

            // 處理起始日期
            if (String.IsNullOrEmpty(startDate) == false)
            {
                DateTime sd = DateTime.Parse(startDate);
                sdt = DateTime.Parse(sd.Year.ToString().PadLeft(4, '0') + "-" + sd.Month.ToString().PadLeft(2, '0') + "-" + sd.Day.ToString().PadLeft(2, '0'));
            }

            // 處理終止日期
            if (String.IsNullOrEmpty(endDate) == false)
            {
                DateTime ed = DateTime.Parse(endDate);
                edt = DateTime.Parse(ed.Year.ToString().PadLeft(4, '0') + "-" + ed.Month.ToString().PadLeft(2, '0') + "-" + ed.Day.ToString().PadLeft(2, '0'));
            }

            // 取得資料庫內的資料
            result = (from d in db_his.laboratoryPatient
                              join p in db_his.patient on d.patient_id equals p.id 
                              where d.laboratory_date >= sdt && d.laboratory_date <= edt
                      select new {
                                  id = d.id,
                                  sno = d.sno,
                                  laboratory_date = d.laboratory_date,
                                  apply_date = d.apply_date,
                                  report_date = d.report_date,
                                  patient_id = p.id,
                                  patient_name = p.strName }).ToDataSourceResult(request);            

            return Json(result);
        }

        public ActionResult GetLPD([DataSourceRequest]DataSourceRequest request, string sHospID, string id)
        {
            // 資料庫連線取的方式同 GetLink(),請參考上述說明
            GetLink();

            HISEntities db_his = new HISEntities(myClass.GetSQLConnectionString(@"23.97.65.134,1933", "his" + sHospID, @"sa", @"I@ntif@t"));
            
            DataSourceResult result;
            try
            {
                result = (from o in db_his.ordfa where o.dtlfa_id == id orderby o.p13 select o).ToDataSourceResult(request);
            }
            catch
            {
                result = null;
            }

            return Json(result);
        }
       
        public ActionResult Async_Save(IEnumerable<HttpPostedFileBase> annex, string HospID)
        {
            string connDRUGSys = ConfigurationManager.ConnectionStrings["connSysDB"].ConnectionString;

            foreach (var file in annex)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    string sDir = Server.MapPath("~/FileCloud") + @"/UploadFile/LISTemp";
                    //string sDir = @"\FileCloud\UploadFile\" + Session["UserRowid"].ToString().Trim();
                    if (Directory.Exists(sDir) == false)
                    {
                        DirectoryInfo di = Directory.CreateDirectory(sDir);
                    }

                    Session["targetNewFileName"] = HospID + "_" + Guid.NewGuid().ToString() + "_" + fileName;

                    var destinationPath = Path.Combine(sDir + "/", Session["targetNewFileName"].ToString());
                    file.SaveAs(destinationPath);

                    // 把上傳的紀錄寫至 UploadServer
                    var us = new UploadServer()
                    {
                        USRowid = Guid.NewGuid().ToString(),
                        USHospRowid = "HIU",
                        USHospID = HospID,
                        USLoadFilename = Session["targetNewFileName"].ToString(),
                        USLoadDateTime = DateTime.Now,
                        USServerStatus = "S",
                        USRecordCount = 0
                    };

                    db_zmcmsv2_sys.UploadServer.Add(us);
                    db_zmcmsv2_sys.SaveChanges();
                }
            }

            //Return an empty string to signify success.
            return Content("");
        }

        public ActionResult Async_Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"
            if (Session["targetNewFileName"] != null)
            {
                string sDir = Server.MapPath("~/FileCloud") + @"/UploadFile/LISTemp";

                var deleteFile = Path.Combine(sDir + "/", Session["targetNewFileName"].ToString());
                if (System.IO.File.Exists(deleteFile))
                {
                    System.IO.File.Delete(deleteFile);
                }

                var us = (from u in db_zmcmsv2_sys.UploadServer where u.USLoadFilename == Session["targetNewFileName"].ToString() select u).FirstOrDefault();
                db_zmcmsv2_sys.UploadServer.Remove(us);
                db_zmcmsv2_sys.SaveChanges();
            }

            // Return an empty string to signify success
            return Content("");
        }
    }
}