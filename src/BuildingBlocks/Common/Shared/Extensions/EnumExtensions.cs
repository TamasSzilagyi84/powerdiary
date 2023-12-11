namespace Shared.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public static class EnumExtensions<T> where T : struct, Enum
    {
        public static IList<T> GetValues(Enum value)
        {
            List<T> enumValues = new List<T>();

            foreach (FieldInfo fi in value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                enumValues.Add((T)Enum.Parse(value.GetType(), fi.Name, false));
            }

            return enumValues;
        }

        public static (bool Success, T EnumValue) TryParse(string value)
        {
            T enumValue = default(T);
            bool success = Enum.TryParse(value, true, out enumValue);

            return (success, enumValue);
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T Parse(int value)
        {
            T enumValue = (T)Enum.ToObject(typeof(T), value);

            return enumValue;
        }

        public static IList<string> GetNames(Enum value)
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        public static IList<string> GetDisplayValues(Enum value)
        {
            return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        public static string TryGetDisplayValueFromString(string value)
        {
            bool success = Enum.TryParse(value, true, out T enumValue);

            if (!success)
            {
                return string.Empty;
            }

            return GetDisplayValue(enumValue);
        }

        public static string GetDisplayValue(T value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo == null)
            {
                throw new Exception($"FieldInfo was not accessible for {value}");
            }

            DisplayAttribute[] descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes != null && descriptionAttributes[0]?.ResourceType != null)
            {
                return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);
            }

            if (descriptionAttributes == null)
            {
                return string.Empty;
            }

            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }

        private static string LookupResource(Type resourceManagerProvider, string resourceKey)
        {
            var resourceKeyProperty = resourceManagerProvider.GetProperty(
                resourceKey,
                BindingFlags.Static | BindingFlags.Public,
                null,
                typeof(string),
                Type.EmptyTypes,
                null);
            if (resourceKeyProperty != null)
            {
                return (string)resourceKeyProperty.GetMethod?.Invoke(null, null);
            }

            return resourceKey; // Fallback with the key name
        }
    }
}
