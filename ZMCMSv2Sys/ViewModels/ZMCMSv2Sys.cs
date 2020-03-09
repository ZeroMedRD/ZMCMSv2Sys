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

    public class ViewModel_HIDtlfa
    {
        [Display(Name = "資料序號")]
        public string id { get; set; }

        [Display(Name = "總表資料序號")]
        public string totfa_id { get; set; }

        [Display(Name = "案件分類(d1)")]
        public string d1 { get; set; }

        [Display(Name = "流水編號(d2)")]
        public string d2 { get; set; }

        [Display(Name = "就醫科別(d8)")]
        public string d8 { get; set; }

        [Display(Name = "就醫日期(d9)")]
        public string d9 { get; set; }

        [Display(Name = "治療結束日期(d10)")]
        public string d10 { get; set; }       
    }

    public class ViewModel_HIOrdfa
    {
        [Display(Name = "資料序號")]
        public string id { get; set; }

        [Display(Name = "點數清單資料序號")]
        public string dtlfa_id { get; set; }

        [Display(Name = "醫令序")]
        public int intSeq { get; set; }

        [Display(Name = "藥品給藥日份(p1)")]
        public string p1 { get; set; }

        [Display(Name = "醫令調劑方式(p2)")]
        public string p2 { get; set; }

        [Display(Name = "醫令類別(p3)")]
        public string p3 { get; set; }

        [Display(Name = "藥品(項目)代號(p4)")]
        public string p4 { get; set; }

        [Display(Name = "藥品用量(p5)")]
        public string p5 { get; set; }

        [Display(Name = "診療之部位(p6)")]
        public string p6 { get; set; }

        [Display(Name = "藥品使用頻率(p7)")]
        public string p7 { get; set; }

        [Display(Name = "醫令序(p13)")]
        public string p13 { get; set; }
    }

    public class ViewModel_UploadServerStatus
    {
        [Display(Name = "資料序號")]
        public string USRowid { get; set; }

        [Display(Name = "醫事機構序號")]
        public string USHospRowid { get; set; }

        [Display(Name = "上傳檔案名稱")]
        public string USLoadFilename { get; set; }

        [Display(Name = "檔案上傳日期")]
        public DateTime? USLoadDateTime { get; set; }

        [Display(Name = "處理狀態")]
        public string USServerStatus { get; set; }

        [Display(Name = "上傳資料總筆數")]
        public int USRecordCount { get; set; }
    }
}