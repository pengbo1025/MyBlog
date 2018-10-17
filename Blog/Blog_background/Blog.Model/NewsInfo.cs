/**  
* NewsInfo.cs
*
* 功 能： N/A
* 类 名： NewsInfo
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
	/// NewsInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[DataContract]
	public partial class NewsInfo
	{
		public NewsInfo()
		{
             ID = 0;
             CategoryID = 0;
             Title = "";
             Release_time = DateTime.Now;
             Release_people = "";
             Click = 0;
             IsHost = false;
             State = false;

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
         public int CategoryID { get; set; }

	    /// <summary>
 		/// 
 		/// </summary>
 		[DataMember]
         public string Title { get; set; }

	    /// <summary>
 		/// 
 		/// </summary>
 		[DataMember]
         public DateTime Release_time { get; set; }

	    /// <summary>
 		/// 
 		/// </summary>
 		[DataMember]
         public string Release_people { get; set; }

	    /// <summary>
 		/// 
 		/// </summary>
 		[DataMember]
         public int Click { get; set; }

	    /// <summary>
 		/// 
 		/// </summary>
 		[DataMember]
         public bool IsHost { get; set; }

	    /// <summary>
 		/// 
 		/// </summary>
 		[DataMember]
         public bool State { get; set; }


		#endregion Model

	}
}

