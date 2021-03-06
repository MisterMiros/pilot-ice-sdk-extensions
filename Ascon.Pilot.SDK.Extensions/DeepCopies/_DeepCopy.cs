﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.ObjectModel;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public abstract class DeepCopy<I>
    {
        protected DeepCopy(I original)
        {
            Type thisType = GetType();
            foreach (var property in typeof(I).GetProperties())
            {
                var thisProp = thisType.GetProperty(property.Name);
                thisProp.SetValue(this, property.GetValue(original, null), null);
            }
        }

        public static bool IsCopy(I original)
        {
            return original == null || original is DeepCopy<I>;
        }

        protected static ReadOnlyCollection<T> CopyCollection<T>(ReadOnlyCollection<T> original) where T : struct
        {
            return original == null ? null : new ReadOnlyCollection<T>(original);
        }

        protected static string CopyString(string s)
        {
            return s == null ? null : string.Copy(s);
        }
    }

    public static class DeepCopyFactory
    {
        static readonly IDictionary<Type, MethodBase> _copyCreators;
        static DeepCopyFactory()
        {
            var methods = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .SelectMany(type => type.GetMethods(BindingFlags.Static | BindingFlags.Public))
                    .Where(method => method.GetParameters().Count() == 1 && method.IsDefined(typeof(DeepCopyCreator), false));
            _copyCreators = new Dictionary<Type, MethodBase>();
            foreach (var method in methods)
            {
                Type type = (method.GetCustomAttributes(typeof(DeepCopyCreator), false).Single() as DeepCopyCreator).Type;
                _copyCreators[type] = method;
            }
        }

        public static I CreateCopy<I>(I original)
            where I : class
        { 
            Type type = typeof(I);
            if (!_copyCreators.ContainsKey(type)) { return original; }
            return _copyCreators[type]?.Invoke(null, new object[] { original }) as I;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    internal class DeepCopyCreator : Attribute
    {
        readonly Type _type;

        public Type Type
        {
            get { return _type; }
        }

        public DeepCopyCreator(Type type) : base()
        {
            _type = type;
        }
    }
}
