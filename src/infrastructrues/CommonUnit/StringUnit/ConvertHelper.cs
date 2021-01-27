using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace CommonUnit.StringUnit
{

    /// <summary>
    /// 类型转化辅助类
    /// </summary>
    public static class ConvertHelper
    {

        #region 判断浮点数相等
        /// <summary>
        /// 判断浮点数相等
        /// </summary>
        /// <param name="numberA"></param>
        /// <param name="numberB"></param>
        /// <returns></returns>
        public static bool FloatEqualTo(this decimal numberA, decimal numberB)
        {
            if (Math.Abs(numberA - numberB).CompareTo(0.0001.ToDecimal()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool FloatEqualTo(this float numberA, float numberB)
        {
            if (Math.Abs(numberA - numberB).CompareTo(0.0001.ToDecimal()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool FloatEqualTo(this double numberA, double numberB)
        {
            if (Math.Abs(numberA - numberB).CompareTo(0.0001.ToDecimal()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 判断对象是否为空
        #region (Object类型)判断对象是否为空，为空返回true
        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty(this object data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }
        #endregion
        #region (泛型)判断对象是否为空，为空返回true
        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <typeparam name="T">要验证的对象的类型</typeparam>
        /// <param name="data">要验证的对象</param>        
        public static bool IsNullOrEmpty<T>(this T data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data is DBNull)
            {
                return true;
            }

            //不为空
            return false;
        }
        #endregion
        #endregion

        #region 类型转化
        /// <summary>
        /// Object转化为Int32类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int32 ToInt32(this object obj)
        {
            if (obj.IsNullOrEmpty())
                return 0;
            else
                return Convert.ToInt32(obj);
        }
        /// <summary>
        /// Object转化为DateTime类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj)
        {
            if (obj.IsNullOrEmpty())
                return DateTime.MinValue;
            else
                return Convert.ToDateTime(obj);
        }
        /// <summary>
        /// Object转化为单精度浮点类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static float ToFloat(this object obj)
        {
            if (obj.IsNullOrEmpty())
                return 0;
            else
                return Convert.ToSingle(obj);

        }
        /// <summary>
        /// Object转化为双精度浮点类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToDouble(this object obj)
        {
            if (obj.IsNullOrEmpty())
                return 0;
            else
                return Convert.ToDouble(obj);
        }
        /// <summary>
        /// Object转化为Bool类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Boolean ToBoolean(this object obj)
        {
            if (obj.IsNullOrEmpty())
                return false;
            else
                return Convert.ToBoolean(obj);

        }
        /// <summary>
        /// Object转化为String类型（强制转化,如果为空则返回Empty)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToStr(this object obj)
        {
            if (obj.IsNullOrEmpty())
                return string.Empty;
            else
                return Convert.ToString(obj);
        }
        /// <summary>
        /// Object转化为decimal类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj)
        {
            if (obj.IsNullOrEmpty())
                return 0;
            else
                return Convert.ToDecimal(obj);

        }
        /// <summary>
        /// 转化为Guid
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string obj)
        {
            return obj.IsNullOrEmpty() ? new Guid() : new Guid(obj);
        }

        #endregion
        #region 使用指定字符集将string转换成byte[]
        /// <summary>
        /// 使用指定字符集将string转换成byte[]
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }
        #endregion

        #region 使用指定字符集将byte[]转换成string
        /// <summary>
        /// 使用指定字符集将byte[]转换成string
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        /// <param name="encoding">字符编码</param>
        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }
        #endregion


        /// <summary>
        /// 人民币大写
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string CmycurD(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string str3 = "";    //从原num值中取出的值 
            string str4 = "";    //数字的字符串形式 
            string str5 = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int j;    //num的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数 
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式 
            j = str4.Length;      //找出最高位 
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3);      //转换为数字 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;
        }
        /// <summary>
        /// Object转化为Int32类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int64 ToInt64(this object obj)
        {
            if (obj.IsNullOrEmpty())
                return 0;
            else
                return Convert.ToInt64(obj);
        }

        public static T ConvertTo<T>(object obj)
        {
            Type t = typeof(T);
            return (T)ConvertToObject(obj, t);
        }

        #region 将一个对象转换为指定类型
        /// <summary>
        /// 将一个对象转换为指定类型
        /// </summary>
        /// <param name="obj">待转换的对象</param>
        /// <param name="type">目标类型</param>
        /// <returns>转换后的对象</returns>
        public static object ConvertToObject(object obj, Type type)
        {
            if (type == null) return obj;
            if (obj == null) return type.IsValueType ? Activator.CreateInstance(type) : null;

            Type underlyingType = Nullable.GetUnderlyingType(type);
            if (type.IsAssignableFrom(obj.GetType())) // 如果待转换对象的类型与目标类型兼容，则无需转换
            {
                return obj;
            }
            else if ((underlyingType ?? type).IsEnum) // 如果待转换的对象的基类型为枚举
            {
                if (underlyingType != null && string.IsNullOrEmpty(obj.ToString())) // 如果目标类型为可空枚举，并且待转换对象为null 则直接返回null值
                {
                    return null;
                }
                else
                {
                    return Enum.Parse(underlyingType ?? type, obj.ToString());
                }
            }
            else if (typeof(IConvertible).IsAssignableFrom(underlyingType ?? type)) // 如果目标类型的基类型实现了IConvertible，则直接转换
            {
                try
                {
                    return Convert.ChangeType(obj, underlyingType ?? type, null);
                }
                catch
                {
                    return underlyingType == null ? Activator.CreateInstance(type) : null;
                }
            }
            else
            {
                TypeConverter converter = TypeDescriptor.GetConverter(type);
                if (converter.CanConvertFrom(obj.GetType()))
                {
                    return converter.ConvertFrom(obj);
                }
                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                if (constructor != null)
                {
                    object o = constructor.Invoke(null);
                    PropertyInfo[] propertys = type.GetProperties();
                    Type oldType = obj.GetType();
                    foreach (PropertyInfo property in propertys)
                    {
                        PropertyInfo p = oldType.GetProperty(property.Name);
                        if (property.CanWrite && p != null && p.CanRead)
                        {
                            property.SetValue(o, ConvertToObject(p.GetValue(obj, null), property.PropertyType), null);
                        }
                    }
                    return o;
                }
            }
            return obj;
        }
        #endregion
    }
}
