﻿using Autofac;
using Surging.Core.CPlatform.DependencyResolution;
using System;

namespace Surging.Core.CPlatform
{
    public class CPlatformContainer
    {
        private readonly IComponentContext _container;

        public IComponentContext Current => _container;

        public CPlatformContainer(IComponentContext container)
        {
            this._container = container;
        }

        public bool IsRegistered<T>()
        {
            return _container.IsRegistered<T>();
        }

        public bool IsRegistered<T>(object serviceKey)
        {
            return _container.IsRegisteredWithKey<T>(serviceKey);
        }

        public  T GetInstances<T>(string name) where T : class
        {
     
            return _container.ResolveKeyed<T>(name);
        }

        public  T GetInstances<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public object GetInstances(Type type)  
        {
            return _container.Resolve(type);
        }

        public T GetInstances<T>(Type type) where T : class
        {
            return _container.Resolve(type) as T;
        }

        public object GetInstances(string name,Type type)  
        {
            // var appConfig = AppConfig.DefaultInstance;
            var objInstance = ServiceResolver.Current.GetService(type, name);
            if (objInstance == null)
            {
                objInstance = string.IsNullOrEmpty(name) ? GetInstances(type) : _container.ResolveKeyed(name, type);
                ServiceResolver.Current.Register(name, objInstance, type);
            }
            return objInstance;
        }
    }
}
