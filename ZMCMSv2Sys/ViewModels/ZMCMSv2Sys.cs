using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ZMCMSv2Sys.ViewModels
{
    public class ViewModel_HITotfa
    {
        [Display(Name = "資料序號")]
        public string id { get; set; }

        [Display(Name = "資料格式")]
        public string t1 { get; set; }

        [Display(Name = "服務機構代號")]
        public string t2 { get; set; }

        [Display(Name = "費用年月")]
        public string t3 { get; set; }

        [Display(Name = "申報方式")]
        public string t4 { get; set; }

        [Display(Name = "申報類別")]
        public string t5 { get; set; }

        [Display(Name = "申報日期")]
        public string t6 { get; set; }

        [Display(Name = "申請件數總計")]
        public string t37 { get; set; }

        [Display(Name = "申請點數總計")]
        public string t38 { get; set; }

        [Display(Name = "部分負擔件數總計")]
        public string t39 { get; set; }

        [Display(Name = "部分負擔點數總計")]
        public string t40 { get; set; }
    }

    public class ViewModel_SchemaBody
    {
        [Display(Name = "資料序號")]
        public string SBRowid { get; set; }

        [Display(Name = "資料表資料序號")]
        public string SHRowid { get; set; }
              
        [Display(Name = "欄位名稱")]
        public string SBFieldName { get; set; }

        [Display(Name = "欄位型態")]
        public string SBFieldtype { get; set; }
                
        [Display(Name = "欄位長度")]
        public string SBFieldLength { get; set; }

        // 影片型態可分 youtube 網址連結或上傳的檔名
        [Display(Name = "欄位小數長度")]
        public string SBFieldScales { get; set; }

        [Display(Name = "欄位說明")]
        public string SBFieldDesc { get; set; }

        [Display(Name = "欄位詳細說明")]
        public string SBFieldDetailDesc { get; set; }

        [Display(Name = "匯入順序")]
        public int SBImportSeq { get; set; }
    }
}