using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;


namespace Sy.Util
{
    /// <summary>
    ///     通用类型扩展方法类
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     把对象类型转化为指定类型，转化失败时返回该类型默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <returns> 转化后的指定类型的对象，转化失败返回类型的默认值 </returns>
        public static T CastTo<T>(this object value)
        {
            object result;
            Type type = typeof(T);
            try
            {
                if (type.IsEnum)
                {
                    result = Enum.Parse(type, value.ToString());
                }
                else if (type == typeof(Guid))
                {
                    result = Guid.Parse(value.ToString());
                }
                else
                {
                    result = Convert.ChangeType(value, type);
                }
            }
            catch
            {
                result = default(T);
            }

            return (T)result;
        }

        /// <summary>
        ///     把对象类型转化为指定类型，转化失败时返回指定的默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <param name="defaultValue"> 转化失败返回的指定默认值 </param>
        /// <returns> 转化后的指定类型对象，转化失败时返回指定的默认值 </returns>
        public static T CastTo<T>(this object value, T defaultValue)
        {
            object result;
            Type type = typeof(T);
            try
            {
                result = type.IsEnum ? Enum.Parse(type, value.ToString()) : Convert.ChangeType(value, type);
            }
            catch
            {
                result = defaultValue;
            }
            return (T)result;
        }

        /// <summary>
        /// 将制定的对象转换为目标对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ConvertTo<T>(this object value) where T : new()
        {
            T result = new T();
            var properties = value.GetType().GetProperties();
            var targetProperties = result.GetType().GetProperties();
            try
            {
                foreach (var p in targetProperties)
                {
                    var defaultModel = properties.Where(a => a.Name == p.Name).FirstOrDefault();
                    if (defaultModel != null)
                    {
                        p.SetValue(result, defaultModel.GetValue(value));
                    }
                }
            }
            catch
            {
                result = default(T);
            }

            return result;

        }

        public static string ToTime(this TimeSpan value)
        {
            string Hours = (value.Days * 24 + value.Hours).ToString();
            string Minutes = value.Minutes.ToString();
            string Seconds = value.Seconds.ToString();
            if (Hours.Length <= 1)
            {
                Hours = "0" + Hours;
            }
            if (Minutes.Length <= 1)
            {
                Minutes = "0" + Minutes;
            }
            if (Seconds.Length <= 1)
            {
                Seconds = "0" + Seconds;
            }
            return string.Format("{0}:{1}:{2}", Hours, Minutes, Seconds);
        }


        /// <summary>
        /// 将对象序列化为JSON字符串，不支持存在循环引用的对象
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="value">动态类型对象</param>
        /// <returns>JSON字符串</returns>
        public static string ToJsonString<T>(this T value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static T ToObject<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }


        /// <summary>
        /// 将对象[主要是匿名对象]转换为dynamic
        /// </summary>
        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            Type type = value.GetType();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
            foreach (PropertyDescriptor property in properties)
            {
                var val = property.GetValue(value);
                if (property.PropertyType.FullName.StartsWith("<>f__AnonymousType"))
                {
                    dynamic dval = val.ToDynamic();
                    expando.Add(property.Name, dval);
                }
                else
                {
                    expando.Add(property.Name, val);
                }
            }
            return expando as ExpandoObject;
        }

    }
}