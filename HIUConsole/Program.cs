using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using HIUConsole.Model;
using OYLib;

namespace HIUConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ZMCMSv2_SysEntities db_zmcmsv2 = new ZMCMSv2_SysEntities();
          
            OYClass myClass = new OYClass();

            var tb_us = (from us in db_zmcmsv2.UploadServer orderby us.USLoadDateTime where us.USServerStatus=="S" 
                         select new { us.USRowid, us.USHospRowid, us.USHospID, us.USLoadFilename });

            foreach (var r in tb_us)
            {
                //Console.WriteLine(r.USRowid.ToString());
                //Console.WriteLine(r.USHospRowid.ToString());
                //Console.WriteLine(r.USHospID.ToString());
                //Console.WriteLine(r.USLoadFilename.ToString());
                //Console.ReadLine();

                // 取得醫事機構代碼
                //var tb_sh = (from sh in db_zmcms.SysHospital where sh.HospRowid == r.USHospRowid.ToString()
                //             select new { sh.HospID }).First();

                //Console.WriteLine(tb_sh.HospID.ToString());
                //Console.ReadLine();
                // 開始
                // 讀取 XML
                XmlDocument doc = new XmlDocument();
                doc.Load(@"D:\ZeroMedRD\ZMCMSv2Sys\ZMCMSv2Sys\FileCloud\UploadFile\HITemp\" + r.USLoadFilename.ToString());
                XmlNodeList NodeList = doc.SelectNodes("outpatient");

                string sText = String.Empty;
                sText = "<" + doc.DocumentElement.Name + ">";
                foreach (XmlNode node in NodeList)
                {
                    sText += node.InnerXml;
                }
                sText += "</" + doc.DocumentElement.Name + ">";

                if (String.IsNullOrEmpty(sText) == false)
                {                    
                    string sCommand = @"if exists(select * from sys.tables where name='XMLwithOpenXML') 
                                             drop table XMLwithOpenXML;
                                          CREATE TABLE XMLwithOpenXML (XMLData xml null);
                                          insert into XMLwithOpenXML (XMLData) values (@XMLData);";

					using (var context = new HISEntities())
                    {
                        var commandText = sCommand;
                        var name = new SqlParameter("@XMLData", sText);
                        context.Database.ExecuteSqlCommand(commandText, name);
                    }

					string sInsertHITotfa = sqlText();
					using (DbContext ctx = new DbContext("Server=127.0.0.1,1933; Integrated Security=true; Database=his" + r.USHospID.ToString() + "; user id=sa;password=1qaz@WSX;"))
					{
						ctx.Database.CommandTimeout = 600;						
						ctx.Database.ExecuteSqlCommand(sInsertHITotfa);
					}
                    

                    //string sConn = ConfigurationManager.ConnectionStrings["Test"].ToString();
                    //SqlConnection conn = new SqlConnection(sConn);

                    //DatabaseProviderFactory factory_XML = new DatabaseProviderFactory();

                    //Database db_InsertXML = factory_XML.Create("conn3HFC");
                    //DbCommand cmd_InsertXML = db_InsertXML.GetSqlStringCommand(sInsertXML);

                    //cmd_InsertXML.Parameters.Clear();
                    //db_InsertXML.AddInParameter(cmd_InsertXML, "@XMLData", DbType.String, sText);
                    //db_InsertXML.AddInParameter(cmd_InsertXML, "@LoadedDateTime", DbType.DateTime, DateTime.Now);

                    //db_InsertXML.ExecuteNonQuery(cmd_InsertXML);
                }

                //HisEntities db_his = new HisEntities(myClass.GetSQLConnectionString(@"127.0.0.1,1933", "antifat", @"sa", @"I@ntif@t"));



            }

			string sqlText()
			{
				string sSqlCommandText =
					@"DECLARE @xmlData xml; select @xmlData=XMLData from [antifat].[dbo].[XMLwithOpenXML];
						select newid() as id,
								Nd.value('(t1)[1]','nvarchar(10)') as t1,
								Nd.value('(t2)[1]','nvarchar(10)') as t2,
								Nd.value('(t3)[1]','nvarchar(10)') as t3,
								Nd.value('(t4)[1]','nvarchar(10)') as t4,
								Nd.value('(t5)[1]','nvarchar(10)') as t5,
								Nd.value('(t6)[1]','nvarchar(10)') as t6,
								Nd.value('(t7)[1]','nvarchar(10)') as t7,
								Nd.value('(t8)[1]','nvarchar(10)') as t8,
								Nd.value('(t9)[1]','nvarchar(10)') as t9,
								Nd.value('(t10)[1]','nvarchar(10)') as t10,
								Nd.value('(t11)[1]','nvarchar(10)') as t11,
								Nd.value('(t12)[1]','nvarchar(10)') as t12,
								Nd.value('(t13)[1]','nvarchar(10)') as t13,
								Nd.value('(t14)[1]','nvarchar(10)') as t14,
								Nd.value('(t15)[1]','nvarchar(10)') as t15,
								Nd.value('(t16)[1]','nvarchar(10)') as t16,
								Nd.value('(t17)[1]','nvarchar(10)') as t17,
								Nd.value('(t18)[1]','nvarchar(10)') as t18,
								Nd.value('(t19)[1]','nvarchar(10)') as t19,
								Nd.value('(t20)[1]','nvarchar(10)') as t20,
								Nd.value('(t21)[1]','nvarchar(10)') as t21,
								Nd.value('(t22)[1]','nvarchar(10)') as t22,
								Nd.value('(t23)[1]','nvarchar(10)') as t23,
								Nd.value('(t24)[1]','nvarchar(10)') as t24,
								Nd.value('(t25)[1]','nvarchar(10)') as t25,
								Nd.value('(t26)[1]','nvarchar(10)') as t26,
								Nd.value('(t27)[1]','nvarchar(10)') as t27,
								Nd.value('(t28)[1]','nvarchar(10)') as t28,
								Nd.value('(t29)[1]','nvarchar(10)') as t29,
								Nd.value('(t30)[1]','nvarchar(10)') as t30,
								Nd.value('(t31)[1]','nvarchar(10)') as t31,
								Nd.value('(t32)[1]','nvarchar(10)') as t32,
								Nd.value('(t33)[1]','nvarchar(10)') as t33,
								Nd.value('(t34)[1]','nvarchar(10)') as t34,
								Nd.value('(t35)[1]','nvarchar(10)') as t35,
								Nd.value('(t36)[1]','nvarchar(10)') as t36,
								Nd.value('(t37)[1]','nvarchar(10)') as t37,
								Nd.value('(t38)[1]','nvarchar(10)') as t38,
								Nd.value('(t39)[1]','nvarchar(10)') as t39,
								Nd.value('(t40)[1]','nvarchar(10)') as t40
							into #totfa from @xmlData.nodes('/outpatient/tdata') AS A(Nd)

						-- 將 totfa 產生的 id 抛給變數 @totfa_id 並依此與 dtlfa 為PK
						declare @totfa_id nvarchar(128);
						declare @t3 nvarchar(10);
						select @totfa_id = id, @t3=t3 from #totfa;

						--drop table #dtlfa 
						select newid() as id,@totfa_id as totfa_id,
							Nd.value('(./dhead/d1)[1]', 'nvarchar(10)') as d1,
							Nd.value('(./dhead/d2)[1]', 'nvarchar(10)') as d2,
							Nd.value('(./dbody/d3)[1]', 'nvarchar(10)') as d3,
							Nd.value('(./dbody/d4)[1]', 'nvarchar(10)') as d4,
							Nd.value('(./dbody/d5)[1]', 'nvarchar(10)') as d5,	   
							Nd.value('(./dbody/d6)[1]', 'nvarchar(10)') as d6,	   
							Nd.value('(./dbody/d7)[1]', 'nvarchar(10)') as d7,	   
							Nd.value('(./dbody/d8)[1]', 'nvarchar(10)') as d8,	   
							Nd.value('(./dbody/d9)[1]', 'nvarchar(10)') as d9,	   
							Nd.value('(./dbody/d10)[1]', 'nvarchar(10)') as d10,	   
							Nd.value('(./dbody/d11)[1]', 'nvarchar(10)') as d11,	   
							Nd.value('(./dbody/d12)[1]', 'nvarchar(10)') as d12,
							Nd.value('(./dbody/d13)[1]', 'nvarchar(10)') as d13,
							Nd.value('(./dbody/d14)[1]', 'nvarchar(10)') as d14,
							Nd.value('(./dbody/d15)[1]', 'nvarchar(10)') as d15,
							Nd.value('(./dbody/d16)[1]', 'nvarchar(10)') as d16,
							Nd.value('(./dbody/d17)[1]', 'nvarchar(10)') as d17,
							Nd.value('(./dbody/d18)[1]', 'nvarchar(10)') as d18,
							Nd.value('(./dbody/d19)[1]', 'nvarchar(10)') as d19,
							Nd.value('(./dbody/d20)[1]', 'nvarchar(10)') as d20,
							Nd.value('(./dbody/d21)[1]', 'nvarchar(10)') as d21,
							Nd.value('(./dbody/d22)[1]', 'nvarchar(10)') as d22,
							Nd.value('(./dbody/d23)[1]', 'nvarchar(10)') as d23,
							Nd.value('(./dbody/d24)[1]', 'nvarchar(10)') as d24,
							Nd.value('(./dbody/d25)[1]', 'nvarchar(10)') as d25,
							Nd.value('(./dbody/d26)[1]', 'nvarchar(10)') as d26,
							Nd.value('(./dbody/d27)[1]', 'nvarchar(10)') as d27,
							Nd.value('(./dbody/d28)[1]', 'nvarchar(10)') as d28,
							Nd.value('(./dbody/d29)[1]', 'nvarchar(10)') as d29,
							Nd.value('(./dbody/d30)[1]', 'nvarchar(10)') as d30,
							Nd.value('(./dbody/d31)[1]', 'nvarchar(10)') as d31,
							Nd.value('(./dbody/d32)[1]', 'nvarchar(10)') as d32,
							Nd.value('(./dbody/d33)[1]', 'nvarchar(10)') as d33,
							Nd.value('(./dbody/d34)[1]', 'nvarchar(10)') as d34,
							Nd.value('(./dbody/d35)[1]', 'nvarchar(10)') as d35,
							Nd.value('(./dbody/d36)[1]', 'nvarchar(10)') as d36,
							Nd.value('(./dbody/d37)[1]', 'nvarchar(10)') as d37,
							Nd.value('(./dbody/d38)[1]', 'nvarchar(10)') as d38,
							Nd.value('(./dbody/d39)[1]', 'nvarchar(10)') as d39,
							Nd.value('(./dbody/d40)[1]', 'nvarchar(10)') as d40,
							Nd.value('(./dbody/d41)[1]', 'nvarchar(10)') as d41,
							Nd.value('(./dbody/d42)[1]', 'nvarchar(10)') as d42,
							Nd.value('(./dbody/d43)[1]', 'nvarchar(10)') as d43,
							Nd.value('(./dbody/d44)[1]', 'nvarchar(10)') as d44,
							Nd.value('(./dbody/d45)[1]', 'nvarchar(10)') as d45,
							Nd.value('(./dbody/d46)[1]', 'nvarchar(11)') as d46,
							Nd.value('(./dbody/d47)[1]', 'nvarchar(11)') as d47,
							Nd.value('(./dbody/d48)[1]', 'nvarchar(10)') as d48,
							Nd.value('(./dbody/d49)[1]', 'nvarchar(20)') as d49
						into #dtlfa from [antifat].[dbo].[XMLwithOpenXML]
						CROSS APPLY 
						@xmlData.nodes('/outpatient/ddata') AS A(Nd)
  
					-- ORDFA 
					select @totfa_id as totfa_id,
							Nd.value('(dhead/d1)[1]','nvarchar(10)') AS d1,
							Nd.value('(dhead/d2)[1]','nvarchar(10)') AS d2,
							Nd.value('(dbody/d3)[1]','nvarchar(10)') AS d3,
							Edg.value('(p1)[1]','nvarchar(10)') AS p1, 
							Edg.value('(p2)[1]','nvarchar(10)') AS p2,
							Edg.value('(p3)[1]','nvarchar(10)') AS p3,
							Edg.value('(p4)[1]','nvarchar(12)') AS p4,
							Edg.value('(p5)[1]','nvarchar(10)') AS p5,
							Edg.value('(p6)[1]','nvarchar(10)') AS p6,
							Edg.value('(p7)[1]','nvarchar(10)') AS p7,
							Edg.value('(p8)[1]','nvarchar(10)') AS p8,
							Edg.value('(p9)[1]','nvarchar(10)') AS p9,
							Edg.value('(p10)[1]','nvarchar(10)') AS p10,
							Edg.value('(p11)[1]','nvarchar(10)') AS p11,
							Edg.value('(p12)[1]','nvarchar(10)') AS p12,
							Edg.value('(p13)[1]','nvarchar(10)') AS p13,
							Edg.value('(p14)[1]','nvarchar(11)') AS p14,
							Edg.value('(p15)[1]','nvarchar(11)') AS p15,
							Edg.value('(p16)[1]','nvarchar(10)') AS p16,
							Edg.value('(p17)[1]','nvarchar(10)') AS p17,
							Edg.value('(p18)[1]','nvarchar(10)') AS p18,
							Edg.value('(p19)[1]','nvarchar(10)') AS p19,
							Edg.value('(p20)[1]','nvarchar(10)') AS p20	
					into #ordfa from @xmlData.nodes('/outpatient/ddata') AS A(Nd)
					CROSS APPLY 
					A.Nd.nodes('dbody/pdata') AS B(Edg) 

					-- 先把資料做清除動作,由下往上刪
					-- 刪除 ordfa
					delete ordfa where dtlfa_id in (select d.id from dtlfa d join (select id from totfa where t3=@t3) t on d.totfa_id=t.id)
		
					-- 刪除 dtlfa
					delete dtlfa where totfa_id in (select id from totfa where t3=@t3)
	
					-- 刪除 totfa
					delete totfa where t3=@t3

					-- 把資料寫入到 totfa
					insert into totfa select * from #totfa
					insert into dtlfa select * from #dtlfa

					-- 整理 #ordfa 資料並寫入到 ordfa
					insert into ordfa select newid() as id,d.id as dtlfa_id,convert(int,o.p13) as intSeq,
											o.p1,o.p2,o.p3,o.p4,o.p5,o.p6,o.p7,o.p8,o.p9,o.p10,
											o.p11,o.p12,o.p13,o.p14,o.p15,o.p16,o.p17,o.p18,o.p19,o.p20 
					from #ordfa o left join #dtlfa d on o.totfa_id=d.totfa_id and o.d1=d.d1 and o.d2=d.d2 and o.d3=d.d3";

				return sSqlCommandText;
			}
		}
    }
}
