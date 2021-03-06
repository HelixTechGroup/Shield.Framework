﻿#region Usings
using System;
using System.Collections.Generic;
using Shield.Framework.Exceptions;
#endregion

namespace Shield.Framework
{
    internal static partial class Throw
    {
        #region Methods
        public static void IfNull<T, TException>(T obj,
                                                 string message = null,
                                                 object[] args = null,
                                                 params KeyValuePair<string, object>[] data)
            where TException : Exception, new()
        {
            if (obj == null)
                throw ExceptionProvider.GenerateException<TException>(message, args, data);
        }

        public static ThrowExceptionSelector IfNull<T>(T obj)
        {
            return new ThrowExceptionSelector(obj == null);
        }

        public static void IfNullOrEmpty<TException>(string obj,
                                                     string message = null,
                                                     object[] args = null,
                                                     params KeyValuePair<string, object>[] data)
            where TException : Exception, new()
        {
            if (string.IsNullOrWhiteSpace(obj))
                throw ExceptionProvider.GenerateException<TException>(message, args, data);
        }

        public static ThrowExceptionSelector IfNullOrEmpty(string obj)
        {
            return new ThrowExceptionSelector(string.IsNullOrWhiteSpace(obj));
        }
        #endregion
    }
}