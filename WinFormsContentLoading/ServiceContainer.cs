#region File Description
//-----------------------------------------------------------------------------
// ServiceContainer.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
#endregion

namespace WinFormsContentLoading
{
    /// <summary>
    /// ServiceContainer クラスは IServiceProvider インターフェイスを実装します。
    /// これは、異なるコンポーネント間で共有サービスを渡すために使用されます。たとえば、
    /// ContentManager はこれを使用して、IGraphicsDeviceService 実装を取得します。
    /// </summary>
    public class ServiceContainer : IServiceProvider
    {
        // マップ(キーとデータ)
        // a = array["trident"];    // 連想配列。associated array
        Dictionary<Type, object> services = new Dictionary<Type, object>();


        /// <summary>
        /// コレクションに新しいサービスを追加します。
        /// </summary>
        public void AddService<T>(T service)
        {
            // マップに追加する。
            services.Add(typeof(T), service);
        }


        /// <summary>
        /// 指定のサービスを取得します。
        /// </summary>
        public object GetService(Type serviceType)
        {
            object service;

            // キーを指定してデータを取り出す。
            services.TryGetValue(serviceType, out service);

            return service;
        }
    }
}
