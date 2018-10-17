/**  
* News_ContentInfo.cs
*
* 功 能： N/A
* 类 名： News_ContentInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/2/1 14:35:14   N/A    初版
*
* Copyright (c)  Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Runtime.Serialization;
namespace Blog.Model
{
	/// <summary>
	/// News_ContentInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[DataContract]
	public partial class News_ContentInfo
	{
		public News_ContentInfo()
		{
             ID = 0;
             NewsID = 0;
             N_Content = "";
             attributes = "";
             weight = 0;
             State = false;
             Headings = false;

        }
		#region Model

	    /// <summary>
 		/// 
 		/// </summary>
 		[DataMember]
         public int ID { get; set; }

	    /// <summary>
 		/// 
 		/// </summary>
 		[DataMember]
         public int NewsID { get; set; }

	    /// <summary>
 		/// 
 		/// </summary>
 		[DataMember]
         public string N_Content { get; set; }

	    /// <summary>
 		/// 
 		/// </summary>
 		[DataMember]
         public string attributes { get; set; }

	    /// <summary>
 		/// 
 		/// </summary>
 		[DataMember]
         public int weight { get; set; }

	    /// <summary>
 		/// 
 		/// </summary>
 		[DataMember]
         public bool State { get; set; }

	    /// <summary>
 		/// 
 		/// </summary>
 		[DataMember]
         public bool Headings { get; set; }


		#endregion Model

	}
}

