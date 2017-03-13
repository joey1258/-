﻿using System;

namespace SimpleContainer.Container
{
    /// <summary>
    /// 无参数构造函数委托
    /// </summary>
    public delegate object Constructor();

    /// <summary>
    /// 有参数构造函数委托
    /// </summary>
    public delegate object ParamsConstructor(object[] parameters);

    /// <summary>
    /// 回调一个无参数方法
    /// </summary>
    public delegate void ParameterlessMethod(object instance);

    /// <summary>
    /// 回调一个有参数方法
    /// </summary>
    public delegate void ParamsMethod(object instance, object[] parameters);

    /// <summary>
    /// 为一个属性或字段回调一个设值注入方法
    /// </summary>
    public delegate void Setter(object instance, object value);

    /// <summary>
    /// Setter 信息类
    /// </summary>
    public class SetterInfo : ParameterInfo
    {
        /// <summary>
        /// Setter 方法
        /// </summary>
        public Setter setter;

        #region constructor

        public SetterInfo(Type type, string name, object id, Setter setter) : base(type, name, id)
        {
            this.setter = setter;
        }

        #endregion
    }

    /// <summary>
    /// 参数信息类
    /// </summary>
    public class ParameterInfo
    {
        /// <summary>
        /// Setter 类型
        /// </summary>
        public Type type;

        /// <summary>
        /// id
        /// </summary>
        public object id;

        /// <summary>
        /// name
        /// </summary>
        public string name;

        #region constructor

        public ParameterInfo(Type type, string name, object id)
        {
            this.type = type;
            this.name = name;
            this.id = id;
        }

        #endregion
    }

    /// <summary>
    /// Method 信息类
    /// </summary>
    public class MethodInfo
    {
        /// <summary>
        /// name
        /// </summary>
        public string name;

        /// <summary>
        /// 无参数方法
        /// </summary>
        public ParameterlessMethod method;

        /// <summary>
        /// 有参数方法
        /// </summary>
        public ParamsMethod paramsMethod;

        /// <summary>
        /// 参数信息类
        /// </summary>
        public ParameterInfo[] parameters;

        #region constructor

        public MethodInfo(string name, ParameterInfo[] parameters)
        {
            this.name = name;
            this.parameters = parameters;
        }

        #endregion
    }
}