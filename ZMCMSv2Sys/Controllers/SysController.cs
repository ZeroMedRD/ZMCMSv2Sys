using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ZMCMSv2Sys.ViewModels;
using ZMCMSv2Sys.Models;

namespace ZMCMSv2Sys.Controllers
{
    public class SysController : Controller
    {
        private HISEntities db_his = new HISEntities();
        private ZMCMSv2_SysEntities db_zmcmsv2_sys = new ZMCMSv2_SysEntities();
        private ZMCMSEntities db_zmcms = new ZMCMSEntities();

        public ActionResult HIUpload(string id)
        {
            ViewBag.HospID = id;

            return View();
        }


        public JsonResult GetTotfa(string sId)
        {
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

        public ActionResult Get_ServerStatus_By_HospID([DataSourceRequest]DataSourceRequest request, string HospRowid)
        {
            DataSourceResult
                result = (from u in db_zmcmsv2_sys.UploadServer
                          let USServerStatus = (u.USServerStatus == "S" ? "待處理" :
                                                u.USServerStatus == "P" ? "處理中" :
                                                u.USServerStatus == "E" ? "已完成" : u.USServerStatus)
                          where u.USHospRowid == HospRowid orderby u.USLoadDateTime 
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