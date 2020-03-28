using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ZMCMSv2Sys.ViewModels
{
    #region TOTFA 申報總表 (23.97.65.134/Database:his1234567890)
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
    #endregion

    #region DTLFA 點數申請表 (23.97.65.134/Database:his1234567890)
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
    #endregion

    #region ORDFA 醫令清單表 (23.97.65.134/Database:his1234567890)
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
    #endregion

    #region laboratoryPatient 檢驗表頭資料 (23.97.65.134/Database:his1234567890)
    public class ViewModel_LP
    {
        [Display(Name = "資料序號")]
        public string id { get; set; }

        [Display(Name = "病人資料序號")]
        public string patient_id { get; set; }

        [Display(Name = "病人姓名")]
        public string patient_name { get; set; }

        [Display(Name = "匯人人員資料序號")]
        public string employee_id { get; set; }

        [Display(Name = "開單日(收件日)")]
        public DateTime? laboratory_date { get; set; }

        [Display(Name = "就診日期")]
        public DateTime? clinic_date { get; set; }

        [Display(Name = "檢驗日期")]
        public DateTime? apply_date { get; set; }

        [Display(Name = "報告日期")]
        public DateTime? report_date { get; set; }

        [Display(Name = "檢驗單號")]
        public string sno { get; set; }

        [Display(Name = "檢驗名稱")]
        public string title { get; set; }

        [Display(Name = "檢驗資料來源")]
        public string lab_clinic_name { get; set; }

        [Display(Name = "完成標記")]
        public bool bolisDone { get; set; }

        [Display(Name = "註記")]
        public string comment { get; set; }
    }
    #endregion

    #region laboratoryPatientDetail 檢驗明細資料 (23.97.65.134/Database:his1234567890)
    public class ViewModel_LPD
    {
        [Display(Name = "資料序號")]
        public string id { get; set; }

        [Display(Name = "檢驗表頭資料序號")]
        public string laboratoryPatient_id { get; set; }

        [Display(Name = "檢驗項目資料序號")]
        public string laboratoryItem_id { get; set; }

        #region laboratory_item 檢驗項目資料 (23.97.65.134/Database:antifat)
        [Display(Name = "檢驗項目代碼")]
        public string laboratory_code { get; set; }

        [Display(Name = "檢驗項目名稱")]
        public string laboratory_name { get; set; }

        [Display(Name = "檢驗項目名稱(中文)")]
        public string laboratory_chnName { get; set; }

        [Display(Name = "檢驗值單位")]
        public string laboratory_unit { get; set; }

        [Display(Name = "標準值說明")]
        public string laboratory_standard { get; set; }

        [Display(Name = "健保代碼")]
        public string laboratory_nhi_code { get; set; }
        #endregion

        [Display(Name = "檢驗數值")]
        public string dataValue { get; set; }

        [Display(Name = "檢驗結果")]
        public string result { get; set; }

        [Display(Name = "檢驗單位")]
        public string unit { get; set; }

        [Display(Name = "檢驗標準參考")]
        public string standard { get; set; }

        [Display(Name = "資料順序")]
        public int intSeq { get; set; }

        [Display(Name = "完成標記")]
        public bool bolisDone { get; set; }

        [Display(Name = "藥物備註")]
        public string drugMemo { get; set; }
    }
    #endregion

    #region 資料上傳主機狀態 (oy.asuscomm.com/Database:UploadServer)    
    public class ViewModel_UploadServerStatus
    {
        [Display(Name = "資料序號")]
        public string USRowid { get; set; }

        [Display(Name = "醫事機構資料序號")]
        public string USHospRowid { get; set; }

        [Display(Name = "醫事機構代碼")]
        public string USHospID { get; set; }

        [Display(Name = "上傳檔案名稱")]
        public string USLoadFilename { get; set; }

        [Display(Name = "上傳日期")]
        public DateTime? USLoadDateTime { get; set; }

        [Display(Name = "預訂執行日期時間")]
        public DateTime? USBookingDatetime { get; set; }

        [Display(Name = "完成日期時間")]
        public DateTime? USFinishDateTime { get; set; }

        [Display(Name = "狀態")]
        public string USServerStatus { get; set; }
        // USServerStatus 定義，字母大寫:
        // S: 待處理
        // P: 處理中
        // E: 已完成        

        [Display(Name = "上傳資料總筆數")]
        public int USRecordCount { get; set; }
                
        [Display(Name = "主機執行類別")]
        public string USType { get; set; }
        // USType 定義，字母大寫:
        // H: 申報上傳 (使用者透過介面上載的檔案)
        // L: 檢驗上傳 (使用者透過介面上載的檔案)
        // A: 檢驗上傳 (使用者透過指定時間方式讓主機 Agent程式依時間到期時自動嫁接第三方執行自動匯入)
        // P: 檢驗上傳 (主機 Agent程式依後台管理模式指定時間到期時自動嫁接第三方執行自動匯入)
    }
    #endregion
}