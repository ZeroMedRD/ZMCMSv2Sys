using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;
//using System.Data.EntityClient;
using System.Data.Entity;
using System.Text;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ZMCMSv2Sys.ViewModels;
using ZMCMSv2Sys.Models;
using ZMLib;

namespace ZMCMSv2Sys.Controllers
{
    public class HIUploadController : Controller
    {
        //private HISEntities db_his = new HISEntities();        
        private ZMCMSv2_SysEntities db_zmcmsv2_sys = new ZMCMSv2_SysEntities();

        ZMClass myClass = new ZMClass();

        public ActionResult Get_Dtlfa_By_Id([DataSourceRequest]DataSourceRequest request, string sHospID, string sTotfaID)
        {
            // 依據使用者所屬的醫事機構代碼切換所屬資料庫            
            HISEntities db_his = new HISEntities(myClass.GetSQLConnectionString(@"23.97.65.134,1933", "his" + sHospID, @"sa", @"I@ntif@t"));

            DataSourceResult result;
            try
            {
                result = (from d in db_his.dtlfa where d.totfa_id == sTotfaID select d).ToDataSourceResult(request);                
            }
            catch
            {
                result = null;
            }

            return Json(result);
        }

        public ActionResult Get_Ordfa_By_Id([DataSourceRequest]DataSourceRequest request, string sHospID, string id)
        {            
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
                    string sDir = Server.MapPath("~/FileCloud") + @"/UploadFile/HITemp";
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
                string sDir = Server.MapPath("~/FileCloud") + @"/UploadFile/HITemp";

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