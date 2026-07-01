using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace task07
{
    public static class ReflectionHelper
    {
        public static string PrintTypeInfo(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var result = new StringBuilder();

            result.AppendLine($"=== Информация о классе {type.Name} ===");

            var displayName = type.GetCustomAttribute<DisplayNameAttribute>();
            if (displayName != null)
            {
                result.AppendLine($"Отображаемое имя: {displayName.DisplayName}");
            }

            var version = type.GetCustomAttribute<VersionAttribute>();
            if (version != null)
            {
                result.AppendLine($"Версия: {version.Major}.{version.Minor}");
            }

            result.AppendLine();

            result.AppendLine("Свойства:");
            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var attr = prop.GetCustomAttribute<DisplayNameAttribute>();
                if (attr != null)
                {
                    result.AppendLine($"  {prop.Name} (Отображаемое имя: {attr.DisplayName})");
                }
                else
                {
                    result.AppendLine($"  {prop.Name}");
                }
            }

            result.AppendLine();

            result.AppendLine("Методы:");
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                var attr = method.GetCustomAttribute<DisplayNameAttribute>();
                if (attr != null)
                {
                    result.AppendLine($"  {method.Name} (Отображаемое имя: {attr.DisplayName})");
                }
                else
                {
                    result.AppendLine($"  {method.Name}");
                }
            }

            return result.ToString();
        }
    }
}