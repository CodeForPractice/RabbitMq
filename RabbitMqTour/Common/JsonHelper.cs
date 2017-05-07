using Newtonsoft.Json;

namespace Common
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：JsonHelper.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：JsonHelper
    /// 创建标识：yjq 2017/5/7 20:45:24
    /// </summary>
    public static class JsonHelper
    {
        public static string ToJson<T>(this T info)
        {
            return JsonConvert.SerializeObject(info);
        }

        public static T ToObj<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}