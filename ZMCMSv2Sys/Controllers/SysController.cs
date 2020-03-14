using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.EntityClient;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ZMCMSv2Sys.ViewModels;
using ZMCMSv2Sys.Models;
using OYLib;

namespace ZMCMSv2Sys.Controllers
{
    public class SysController : Controller
    {
        //private HISEntities db_his = new HISEntities();
        private ZMCMSv2_SysEntities db_zmcmsv2_sys = new ZMCMSv2_SysEntities();
        private ZMCMSEntities db_zmcms = new ZMCMSEntities();

        OYClass myClass = new OYClass();

        //public string GetSQLConnectionString(
        //    string dbs,
        //    string dbn,
        //    string userId,
        //    string pw)
        //{
        //    SqlConnectionStringBuilder providerCs = new SqlConnectionStringBuilder();

        //    providerCs.DataSource = dbs;
        //    providerCs.InitialCatalog = String.IsNullOrEmpty(dbn) ? "his0000000000" : dbn;
        //    //providerCs.UserInstance = true;
        //    providerCs.UserID = userId;
        //    providerCs.Password = pw;
        //    providerCs.IntegratedSecurity = false;

        //    var csBuilder = new EntityConnectionStringBuilder
        //    {
        //        Provider = "System.Data.SqlClient",
        //        ProviderConnectionString = providerCs.ToString(),

        //        //<add name="HISEntities" connectionString="metadata=res://*/Areas.HIS.Models.HISModel.csdl|res://*/Areas.HIS.Models.HISModel.ssdl|res://*/Areas.HIS.Models.HISModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=www.tongxin.com.tw,38301;initial catalog=3532040438;persist security info=True;user id=sa;password=23752100;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient"/>
        //        //csBuilder.Metadata = string.Format("res://*/{0}/{1}.csdl|res://{0}/{1}.ssdl|res://{0}/{1}.msl",
        //        Metadata = "res://*/Models.ZMCMSv2Model.csdl|res://*/Models.ZMCMSv2Model.ssdl|res://*/Models.ZMCMSv2Model.msl"
        //    };

        //    return csBuilder.ToString();
        //}

        public ActionResult HIUpload(string id)
        {
            ViewBag.HospID = id;

            return View();
        }


        public JsonResult GetTotfa(string sId)
        {
            HISEntities db_his = new HISEntities(myClass.GetSQLConnectionString(@"23.97.65.134,1933", "his" + sId, @"sa", @"I@ntif@t"));

            var result = (from t in db_his.totfa 
                          where t.t2 == sId
                          orderby t.t3 descending select new { t.id, t.t3 });

            return Json(result, JsonRequestBehavior.AllowGet);
            //return Json(result);
        }

        public JsonResult GetHospital(string HospID)
        {
            if (string.IsNullOrEmpty(HospID))
            {
                // 依使用者權限取得所屬的醫事機構選項
                var result = (from sh in db_zmcms.SysHospital
                              where sh.HospActive == true
                              orderby sh.HospID descending
                              select new { sh.HospID, sh.HospName });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // 依傳入的參數取得醫事機構選項
                var result = (from sh in db_zmcms.SysHospital
                              where sh.HospID == HospID && sh.HospActive == true
                              orderby sh.HospID descending
                              select new { sh.HospRowid, sh.HospID, sh.HospName });
                return Json(result, JsonRequestBehavior.AllowGet);
            }                        
        }

        public ActionResult Get_ServerStatus_By_HospID([DataSourceRequest]DataSourceRequest request, string sHospID)
        {
            DataSourceResult
                result = (from u in db_zmcmsv2_sys.UploadServer
                          let USServerStatus = (u.USServerStatus == "S" ? "待處理" :
                                                u.USServerStatus == "P" ? "處理中" :
                                                u.USServerStatus == "E" ? "已完成" : u.USServerStatus)
                          where u.USHospID == sHospID
                          orderby u.USLoadDateTime 
                          select new 
                          { 
                              u.USRowid,
                              u.USHospRowid,
                              u.USLoadDateTime,
                              u.USLoadFilename,                              
                              USServerStatus,
                              u.USRecordCount
                          }).ToDataSourceResult(request);

            return Json(result);
        }
    }
}