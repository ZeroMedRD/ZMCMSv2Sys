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

        // GET: Sys
        public ActionResult HIUpload(string id)
        {
            ViewBag.HospRowid = id;

            return View();
        }

        public JsonResult GetTotfa(string HospID)
        {
            var result = (from t in db_his.totfa 
                          where t.t2 == HospID 
                          orderby t.t3 descending select new { t.id, t.t3 });

            return Json(result, JsonRequestBehavior.AllowGet);
            //return Json(result);
        }

        public JsonResult GetHospital(string HospRowid)
        {
            //if (String.IsNullOrEmpty(HospRowid))
            //{

            //}

            var result = (from sh in db_zmcms.SysHospital
                          where sh.HospRowid == HospRowid
                          orderby sh.HospID descending
                          select new { sh.HospRowid, sh.HospID, sh.HospName });

            return Json(result, JsonRequestBehavior.AllowGet);            
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