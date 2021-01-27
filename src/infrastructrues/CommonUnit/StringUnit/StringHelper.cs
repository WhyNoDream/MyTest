using Newtonsoft.Json;
using OfficeOpenXml.Packaging.Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonUnit.StringUnit
{
    public static class StringHelper
    {
        public static string TrimStart(this string source, string trim, StringComparison stringComparison = StringComparison.Ordinal)
        {
            if (source == null)
            {
                return null;
            }

            string s = source;
            while (s.StartsWith(trim, stringComparison))
            {
                s = s.Substring(trim.Length);
            }

            return s;
        }

        /// <summary>
        /// 分割逗号的字符串为List<string>
        /// </summary>
        /// <param name="csvList"></param>
        /// <param name="nullOrWhitespaceInputReturnsNull">nullorwhitespace字符串是否返回空对象</param>
        /// <returns></returns>
        public static List<string> SplitCsv(this string csvList, bool nullOrWhitespaceInputReturnsNull = false)
        {
            if (string.IsNullOrWhiteSpace(csvList))
                return nullOrWhitespaceInputReturnsNull ? null : new List<string>();

            return csvList
                .TrimEnd(',')
                .Split(',')
                .AsEnumerable<string>()
                .Select(s => s.Trim())
                .ToList();
        }

        public static bool IsNullOrWhitespace(this string s)
        {
            return String.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// 获取域名
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDomainName(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            try
            {
                var reslut = value.Replace("http://", "").Replace("https://", "").Split('/')[0];
                return reslut;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 实体转JSON字符窜
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetJsonToString(object model)
        {
            var jSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var json = JsonConvert.SerializeObject(model, Formatting.None, jSetting);
            return json;
        }

        /// <summary>
        /// Json字符窜转为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetJsonToObject<T>(string value)
        {
            var jSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var model = JsonConvert.DeserializeObject<T>(value, jSetting);
            return model;
        }

        //zip压缩字节
        //1.创建压缩的数据流 
        //2.设定compressStream为存放被压缩的文件流,并设定为压缩模式
        //3.将需要压缩的字节写到被压缩的文件流
        public static byte[] CompressBytes(byte[] bytes)
        {
            using (MemoryStream compressStream = new MemoryStream())
            {
                using (var zipStream = new System.IO.Compression.GZipStream(compressStream, System.IO.Compression.CompressionMode.Compress))
                    zipStream.Write(bytes, 0, bytes.Length);
                return compressStream.ToArray();
            }
        }
        //zip解压缩字节
        //1.创建被压缩的数据流
        //2.创建zipStream对象，并传入解压的文件流
        //3.创建目标流
        //4.zipStream拷贝到目标流
        //5.返回目标流输出字节
        public static byte[] Decompress(byte[] bytes)
        {
            using (var compressStream = new MemoryStream(bytes))
            {
                using (var zipStream = new System.IO.Compression.GZipStream(compressStream, System.IO.Compression.CompressionMode.Decompress))
                {
                    using (var resultStream = new MemoryStream())
                    {
                        zipStream.CopyTo(resultStream);
                        return resultStream.ToArray();
                    }
                }
            }
        }



        //zlip压缩字节
        //1.创建压缩的数据流 
        //2.设定compressStream为存放被压缩的文件流,并设定为压缩模式
        //3.将需要压缩的字节写到被压缩的文件流
        public static byte[] CompressZlipBytes(byte[] bytes)
        {
            using (MemoryStream compressStream = new MemoryStream())
            {
                using (var zipStream = new ZlibStream(compressStream, CompressionMode.Compress))
                    zipStream.Write(bytes, 0, bytes.Length);
                return compressStream.ToArray();
            }
        }

        /// <summary>
        /// 解压zlip压缩二进制
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] DeconpressZlip(byte[] bytes)
        {
            using (var compressStream = new MemoryStream(bytes))
            {
                using (var zipStream = new ZlibStream(compressStream, CompressionMode.Decompress))
                {
                    using (var resultStream = new MemoryStream())
                    {
                        zipStream.CopyTo(resultStream);
                        return resultStream.ToArray();
                    }
                }
            }

        }


    }
}
