using System;
using System.Text;

namespace Common
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ByteExtesion.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ByteExtesion
    /// 创建标识：yjq 2017/5/7 20:48:55
    /// </summary>
    public static class ByteExtesion
    {
        public static Byte[] ToBytes(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static string ToStr(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}