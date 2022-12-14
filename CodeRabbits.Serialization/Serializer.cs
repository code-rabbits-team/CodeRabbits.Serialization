// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace CodeRabbits.Serialization;

public static class Serializer
{
    public static string SerializeCsv<T>(IEnumerable<T> datas)
    {
        return SerializeCsv(datas, Environment.NewLine);
    }

    public static string SerializeCsv<T>(IEnumerable<T> datas, string newLine)
    {
        string token = ",";
        StringBuilder buffer = new();
        StringBuilder AppendLine(string? value)
        {
            buffer.Append(value);
            return buffer.Append(newLine);
        };

        AppendLine(string.Join(token, GetSerializePropertyNames<T>()));
        var d = datas.Select(data => GetSerializePropertyValues(data));

        foreach (var data in datas)
        {
            AppendLine(string.Join(token, GetSerializePropertyValues(data).Select(d => d.ToString())));
        }

        return buffer.ToString();
    }

    private static IEnumerable<object> GetSerializePropertyValues<T>(T data) => from property in typeof(T).GetProperties()
                                                                                where !Attribute.IsDefined(property, typeof(IgnoreDataMemberAttribute))
                                                                                select property.GetValue(data);
    private static IEnumerable<string> GetSerializePropertyNames<T>()
    {
        return GetSerializePropertyNames(typeof(T));
    }

    private static IEnumerable<string> GetSerializePropertyNames(Type type) => from property in type.GetProperties()
                                                                               where !Attribute.IsDefined(property, typeof(IgnoreDataMemberAttribute))
                                                                               select property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? property.Name;
}
