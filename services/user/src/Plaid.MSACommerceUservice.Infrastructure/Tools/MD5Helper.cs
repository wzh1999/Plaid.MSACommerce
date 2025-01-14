﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.Uservice.Infrastructure.Tools
{
    /// <summary>
    /// 封装加密MD5帮助类
    /// </summary>
    public static class MD5Helper
    {
        /// <summary>
        /// MD5加密字符串
        /// </summary>
        /// <param name="content">源字符串</param>
        /// <returns>加密后字符串</returns>
        public static string MD5EncodingOnly(string content)
        {
            //创建MD5类的默认实例:MD5CryptoServiceProvider
            var md5 = MD5.Create();
            var bs = Encoding.UTF8.GetBytes(content);
            var hs = md5.ComputeHash(bs);
            var stb = new StringBuilder();
            foreach (var b in hs)
            {
                //以十六进制格式格式化
                stb.Append(b.ToString("x2"));
            }
            return stb.ToString();
        }

        /// <summary>
        /// MD5盐值加密
        /// </summary>
        /// <param name="context">源字符串</param>
        /// <param name="salt">盐值</param>
        /// <returns>加密后字符串</returns>
        public static string MD5EncodingWithSalt(string context, string salt)
        {
            return string.IsNullOrEmpty(salt) ? context : MD5EncodingOnly(context + "{" + salt + "}");
        }
    }
}
