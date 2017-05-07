using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ConnectionFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ConnectionFactory
    /// 创建标识：yjq 2017/5/7 21:11:42
    /// </summary>
    public sealed class MqConnectionFactory
    {
        private static ConcurrentDictionary<string, IConnection> _connCache = new ConcurrentDictionary<string, IConnection>();

        public static IConnection GetConn(RabbitMqConfig config)
        {
            if (!IsHaveCreate(config))
            {
                lock (_connCache)
                {
                    if (!IsHaveCreate(config))
                    {
                        _connCache[config.ToString()] = new ConnectionFactory
                        {
                            HostName = config.HostName,
                            Password = config.Password,
                            NetworkRecoveryInterval = config.NetworkRecoveryInterval,
                            RequestedHeartbeat = config.RequestedHeartbeat,
                            UserName = config.UserName,
                            VirtualHost = config.VirtualHost
                        }.CreateConnection();
                    }
                }
            }
            return _connCache[config.ToString()];
        }

        public static void Dispose()
        {
            lock (_connCache)
            {
                foreach (var item in _connCache)
                {
                    item.Value?.Close();
                    item.Value?.Dispose();
                }
                _connCache.Clear();
            }
        }

        private static bool IsHaveCreate(RabbitMqConfig config)
        {
            return _connCache.ContainsKey(config.ToString());
        }
    }
}
