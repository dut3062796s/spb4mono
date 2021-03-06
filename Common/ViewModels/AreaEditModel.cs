﻿//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Tunynet.Common;

namespace Spacebuilder.Common
{
    /// <summary>
    /// 创建地区的类
    /// </summary>
    public class AreaEditModel
    {
        private AreaService areaService = new AreaService();

        /// <summary>
        /// 地区编，数据表的主键
        /// </summary>
        [Display(Name = "行政区号")]
        public string AreaCode { get; set; }
        /// <summary>
        /// 地区名
        /// </summary>
        [Display(Name = "地区名称")]
        [StringLength(18, ErrorMessage = "不能超过18个字符")]
        [Required(ErrorMessage = "请输入地区名称")]
        public string AreaName { get; set; }
        /// <summary>
        /// 父地区的地区码
        /// </summary>
        [Display(Name = "父地区")]
        [Required(ErrorMessage = "请选择父地区")]
        public string ParentCode { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        [Display(Name = "邮政编码")]
        [StringLength(8, ErrorMessage = "不能超过8个字符")]
        [RegularExpression(@"[A-Za-z0-9]+", ErrorMessage = "只能输入字母或数字")]
        public string PostCode { get; set; }

        /// <summary>
        /// 转化为Area用于数据库存储
        /// </summary>
        /// <returns>Area</returns>
        public Area AsArea()
        {
            Area area = areaService.Get(this.AreaCode);
            area.Name = this.AreaName;
            area.ParentCode = this.ParentCode ?? string.Empty;
            area.PostCode = this.PostCode ?? string.Empty;

            return area;
        }
    }
    /// <summary>
    /// 地区的扩展方法
    /// </summary>
    public static class AreaExtensions
    {
        /// <summary>
        /// 转换成AreaEditModel
        /// </summary>
        public static AreaEditModel AsEditModel(this Area area)
        {
            return new AreaEditModel
            {
                AreaCode = area.AreaCode,
                ParentCode = area.ParentCode,
                PostCode = area.PostCode,
                AreaName = area.Name
            };
        }


    }
}
