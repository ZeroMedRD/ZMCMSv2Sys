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

        // GET: Sys
        public ActionResult HIUpload()
        {
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

        public ActionResult Get_ServerStatus_By_HospID([DataSourceRequest]DataSourceRequest request, string sHospRowid)
        {
            DataSourceResult
                result = (from u in db_zmcmsv2_sys.UploadServer where u.USHospRowid == sHospRowid orderby u.USLoadDateTime select u).ToDataSourceResult(request);

            return Json(result);
        }

    }
}