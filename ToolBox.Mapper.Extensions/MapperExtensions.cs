﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ToolBox.Mapper.Extensions
{
    public static class MapperExtensions
    {
        public static T MapTo<T>(this object from, params object[] ctorParameters)
            where T : class
        {
            ConstructorInfo ctor = typeof(T).GetConstructor(ctorParameters.Select(p => p.GetType()).ToArray());
            if (ctor == null) throw new ArgumentException("Invalid Constructor Parameters");
            T result = (T)ctor.Invoke(ctorParameters);
            IEnumerable<PropertyInfo> properties = from.GetType().GetProperties()
                .Where(p => p.CanRead && !p.IsDefined(typeof(MapperIgnoreAttribute)));
            foreach (PropertyInfo property in properties)
            {
                try
                {
                    MapperPropertyAttribute propAttribute = property.GetCustomAttribute<MapperPropertyAttribute>();
                    PropertyInfo resultProp = typeof(T).GetRuntimeProperty(
                        propAttribute?.PropertyName ?? property.Name
                    );
                    if (
                        resultProp != null
                        && !resultProp.IsDefined(typeof(MapperIgnoreAttribute))
                        && resultProp.CanWrite
                    )
                    {
                        if (property.PropertyType == resultProp.PropertyType)
                            resultProp.SetValue(result, property.GetValue(from));
                        else if (TypeDescriptor.GetConverter(property.PropertyType).CanConvertTo(resultProp.PropertyType))
                            resultProp.SetValue(result, Convert.ChangeType(property.GetValue(from), resultProp.PropertyType));
                        else if (property.PropertyType == typeof(string))
                        {
                            if (resultProp.PropertyType == typeof(int))
                                resultProp.SetValue(result, int.Parse(property.GetValue(from).ToString()));
                            else if (resultProp.PropertyType == typeof(DateTime))
                                resultProp.SetValue(result, DateTime.Parse(property.GetValue(from).ToString()));
                            else if (resultProp.PropertyType == typeof(bool))
                                resultProp.SetValue(result, bool.Parse(property.GetValue(from).ToString()));
                            else if (resultProp.PropertyType == typeof(double))
                                resultProp.SetValue(result, double.Parse(property.GetValue(from).ToString()));
                            else if (resultProp.PropertyType == typeof(float))
                                resultProp.SetValue(result, float.Parse(property.GetValue(from).ToString()));
                        }
                    }
                }
                catch (Exception) { }
            }
            return result;
        }


        public static T MapToInstance<T>(this object from, T result)
                where T : class
        {
            IEnumerable<PropertyInfo> properties = from.GetType().GetProperties()
                .Where(p => p.CanRead && !p.IsDefined(typeof(MapperIgnoreAttribute)));
            foreach (PropertyInfo property in properties)
            {
                try
                {
                    MapperPropertyAttribute propAttribute = property.GetCustomAttribute<MapperPropertyAttribute>();
                    PropertyInfo resultProp = typeof(T).GetRuntimeProperty(
                        propAttribute?.PropertyName ?? property.Name
                    );
                    if (
                        resultProp != null
                        && !resultProp.IsDefined(typeof(MapperIgnoreAttribute))
                        && resultProp.CanWrite
                    )
                    {
                        if (property.PropertyType == resultProp.PropertyType)
                            resultProp.SetValue(result, property.GetValue(from));
                        else if (TypeDescriptor.GetConverter(property.PropertyType).CanConvertTo(resultProp.PropertyType))
                            resultProp.SetValue(result, Convert.ChangeType(property.GetValue(from), resultProp.PropertyType));
                        else if (property.PropertyType == typeof(string))
                        {
                            if (resultProp.PropertyType == typeof(int))
                                resultProp.SetValue(result, int.Parse(property.GetValue(from).ToString()));
                            else if (resultProp.PropertyType == typeof(DateTime))
                                resultProp.SetValue(result, DateTime.Parse(property.GetValue(from).ToString()));
                            else if (resultProp.PropertyType == typeof(bool))
                                resultProp.SetValue(result, bool.Parse(property.GetValue(from).ToString()));
                            else if (resultProp.PropertyType == typeof(double))
                                resultProp.SetValue(result, double.Parse(property.GetValue(from).ToString()));
                            else if (resultProp.PropertyType == typeof(float))
                                resultProp.SetValue(result, float.Parse(property.GetValue(from).ToString()));
                        }
                    }
                }
                catch (Exception) { }
            }
            return result;
        }
    }
}
