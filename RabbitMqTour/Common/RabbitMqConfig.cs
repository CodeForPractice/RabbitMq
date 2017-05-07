using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RabbitMqConfig.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：RabbitMqConfig
    /// 创建标识：yjq 2017/5/7 21:09:41
    /// </summary>
    public sealed class RabbitMqConfig
    {

        public string HostName = "localhost";
        public string Password = "yjq";
        public string UserName = "yjq";
        public string VirtualHost = "Test";
        public ushort RequestedHeartbeat = 60;
        public TimeSpan NetworkRecoveryInterval = TimeSpan.FromMinutes(1);

        public override string ToString()
        {
            return $"{HostName}:{VirtualHost}:{UserName}:{Password}";
        }
    }
}
