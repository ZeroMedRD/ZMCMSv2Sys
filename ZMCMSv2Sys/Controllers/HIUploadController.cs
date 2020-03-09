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

namespace ZMCMSv2Sys.Controllers
{
    public class HIUploadController : Controller
    {
        private HISEntities db_his = new HISEntities();        

        public ActionResult Get_Dtlfa_By_Id([DataSourceRequest]DataSourceRequest request, string sId)
        {
            DataSourceResult
                result = (from d in db_his.dtlfa where d.totfa_id == sId select d).ToDataSourceResult(request);
            
            return Json(result);
        }

        public ActionResult Get_Ordfa_By_Id([DataSourceRequest]DataSourceRequest request, string id)
        {
            DataSourceResult
                result = (from o in db_his.ordfa where o.dtlfa_id == id orderby o.p13 select o).ToDataSourceResult(request);

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

                    // 開始匯入         
                    string vstrFormFile = destinationPath;

                    //DataTable _HIDrug = GetDataSourceFromFile(vstrFormFile);

                    //using (var bulkCopy = new SqlBulkCopy(connDRUGSys, SqlBulkCopyOptions.KeepIdentity))
                    //{
                    //    bulkCopy.BulkCopyTimeout = 60000;
                    //    bulkCopy.DestinationTableName = "HIDrug";
                    //    bulkCopy.WriteToServer(_HIDrug);
                    //}
                }
            }

            //Return an empty string to signify success.
            return Content("");
        }

        public ActionResult Async_Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            //if (fileNames != null)
            //{
            //    foreach (var fullName in fileNames)
            //    {
            //        var fileName = Path.GetFileName(fullName);
            //        string sDir = Server.MapPath("~/FileCloud") + @"/UploadFile/" + Session["UserRowid"].ToString().Trim();
            //        var physicalPath = Path.Combine(sDir + "/", fileName);

            //        if (System.IO.File.Exists(physicalPath))
            //        {
            //            System.IO.File.Delete(physicalPath);
            //        }
            //    }
            //}

            if (Session["targetNewFileName"] != null)
            {
                string sDir = Server.MapPath("~/FileCloud") + @"/UploadFile/HITemp";

                var deleteFile = Path.Combine(sDir + "/", Session["targetNewFileName"].ToString());
                if (System.IO.File.Exists(deleteFile))
                {
                    System.IO.File.Delete(deleteFile);
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        private DataTable GetDataSourceFromFile(string fileName)
        {
            DataTable dt = new DataTable("HIDrug");

            dt.Columns.Add("HIDrugRowid");
            dt.Columns.Add("HIDrugNewMark");
            dt.Columns.Add("HIDrugOralIngotNote");
            dt.Columns.Add("HIDrugSingleComplexNote");
            dt.Columns.Add("HIDrugCode");
            dt.Columns.Add("HIDrugCost");
            dt.Columns.Add("HIDrugDate");
            dt.Columns.Add("HIDrugCloseDate");
            dt.Columns.Add("HIDrugEngName");
            dt.Columns.Add("HIDrugSpecQty");
            dt.Columns.Add("HIDrugSpecUnit");
            dt.Columns.Add("HIDrugIngName");
            dt.Columns.Add("HIDrugIngQty");
            dt.Columns.Add("HIDrugIngUnit");
            dt.Columns.Add("HIDrugDosage");
            dt.Columns.Add("HIDrugReserved01");
            dt.Columns.Add("HIDrugDealer");
            dt.Columns.Add("HIDrugReserved02");
            dt.Columns.Add("HIDrugClass");
            dt.Columns.Add("HIDrugQualityClass");
            dt.Columns.Add("HIDrugChtName");
            dt.Columns.Add("HIDrugGroupName");
            dt.Columns.Add("HIDrugIngName01");
            dt.Columns.Add("HIDrugIngQty01");
            dt.Columns.Add("HIDrugIngUnit01");
            dt.Columns.Add("HIDrugIngName02");
            dt.Columns.Add("HIDrugIngQty02");
            dt.Columns.Add("HIDrugIngUnit02");
            dt.Columns.Add("HIDrugIngName03");
            dt.Columns.Add("HIDrugIngQty03");
            dt.Columns.Add("HIDrugIngUnit03");
            dt.Columns.Add("HIDrugIngName04");
            dt.Columns.Add("HIDrugIngQty04");
            dt.Columns.Add("HIDrugIngUnit04");
            dt.Columns.Add("HIDrugIngName05");
            dt.Columns.Add("HIDrugIngQty05");
            dt.Columns.Add("HIDrugIngUnit05");
            dt.Columns.Add("HIDrugManufacture");
            dt.Columns.Add("HIDrugATCCode");
            dt.Columns.Add("HIDrugNoProuct");

            // reading rest of the data
            using (FileStream fs = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                //http://msdn.microsoft.com/zh-tw/library/ms143457(v=vs.80).aspx
                //using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding(950), true))
                //{
                string[] lines = System.IO.File.ReadAllLines(fileName, Encoding.GetEncoding(950));

                foreach (string line in lines)
                {
                    byte[] linestr = Encoding.Default.GetBytes(line);

                    DataRow dr = dt.NewRow();

                    dr[0] = Guid.NewGuid().ToString();
                    dr[1] = Encoding.Default.GetString(linestr, 0, 2);      // New_mark
                    dr[2] = Encoding.Default.GetString(linestr, 3, 10);     // 口服錠註記
                    dr[3] = Encoding.Default.GetString(linestr, 14, 2);     // 單/複方註記
                    dr[4] = Encoding.Default.GetString(linestr, 17, 10);    // 藥品代碼
                    dr[5] = Encoding.Default.GetString(linestr, 28, 9);     // 藥價參考金額
                    dr[6] = Encoding.Default.GetString(linestr, 38, 7);     // 藥價參考日期
                    dr[7] = Encoding.Default.GetString(linestr, 46, 7);     // 藥價參考截止日期
                    dr[8] = Encoding.Default.GetString(linestr, 54, 120);   // 藥品英文名稱
                    dr[9] = Encoding.Default.GetString(linestr, 175, 7);    // 藥品規格量
                    dr[10] = Encoding.Default.GetString(linestr, 183, 52);  // 藥品規格單位
                    dr[11] = Encoding.Default.GetString(linestr, 236, 56);  // 成份名稱
                    dr[12] = Encoding.Default.GetString(linestr, 293, 12);  // 成份含量
                    dr[13] = Encoding.Default.GetString(linestr, 306, 51);  // 成份含量單位
                    dr[14] = Encoding.Default.GetString(linestr, 358, 86);  // 藥品劑型
                    dr[15] = Encoding.Default.GetString(linestr, 445, 158); // 空白 
                    dr[16] = Encoding.Default.GetString(linestr, 604, 20);  // 藥商名稱
                    dr[17] = Encoding.Default.GetString(linestr, 625, 141); // 空白
                    dr[18] = Encoding.Default.GetString(linestr, 767, 1);   // 藥品分類
                    dr[19] = Encoding.Default.GetString(linestr, 769, 1);   // 品質分類碼
                    dr[20] = Encoding.Default.GetString(linestr, 771, 128); // 藥品中文名稱
                    dr[21] = Encoding.Default.GetString(linestr, 900, 300); // 分類分組名稱
                    dr[22] = Encoding.Default.GetString(linestr, 1200, 56); // （複方一）成份名稱
                    dr[23] = Encoding.Default.GetString(linestr, 1258, 11); // （複方一）藥品成份含量
                    dr[24] = Encoding.Default.GetString(linestr, 1270, 51); // （複方一）藥品成份含量單位
                    dr[25] = Encoding.Default.GetString(linestr, 1322, 56); // （複方二）成份名稱
                    dr[26] = Encoding.Default.GetString(linestr, 1379, 11); // （複方二）藥品成份含量
                    dr[27] = Encoding.Default.GetString(linestr, 1391, 51); // （複方二）藥品成份含量單位
                    dr[28] = Encoding.Default.GetString(linestr, 1443, 56); // （複方三）成份名稱
                    dr[29] = Encoding.Default.GetString(linestr, 1500, 11); // （複方三）藥品成份含量
                    dr[30] = Encoding.Default.GetString(linestr, 1512, 51); // （複方三）藥品成份含量單位
                    dr[31] = Encoding.Default.GetString(linestr, 1564, 56); // （複方四）成份名稱
                    dr[32] = Encoding.Default.GetString(linestr, 1621, 11); // （複方四）藥品成份含量
                    dr[33] = Encoding.Default.GetString(linestr, 1633, 51); // （複方四）藥品成份含量單位
                    dr[34] = Encoding.Default.GetString(linestr, 1685, 56); // （複方五）成份名稱
                    dr[35] = Encoding.Default.GetString(linestr, 1742, 11); // （複方五）藥品成份含量
                    dr[36] = Encoding.Default.GetString(linestr, 1754, 51); // （複方五）藥品成份含量單位
                    dr[37] = Encoding.Default.GetString(linestr, 1806, 42); // 製造廠商
                    dr[38] = Encoding.Default.GetString(linestr, 1849, 8);  // ACT CODE
                    dr[39] = Encoding.Default.GetString(linestr, 1858, 1);  // 未生產或未輸入達五年
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }
    }
}