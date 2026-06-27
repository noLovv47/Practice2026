using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace task05
{
    public class ClassAnalyzer
    {
        private readonly Type _type;

        public ClassAnalyzer(Type type)
        {
            _type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public IEnumerable<string> GetPublicMethods()
        {
            return _type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                        .Select(m => m.Name)
                        .ToList();
        }

        public IEnumerable<string> GetMethodParams(string methodName)
        {
            if (string.IsNullOrEmpty(methodName))
                return new List<string>();

            var method = _type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);

            if (method == null)
                return new List<string>();

            var result = new List<string>();

            result.Add($"Return: {method.ReturnType.Name}");

            var parameters = method.GetParameters();
            if (parameters.Any())
            {
                result.AddRange(parameters.Select(p => $"{p.Name}: {p.ParameterType.Name}"));
            }

            return result;
        }

        public IEnumerable<string> GetAllFields()
        {
            return _type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                        .Select(f => f.Name)
                        .ToList();
        }

        public IEnumerable<string> GetProperties()
        {
            return _type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Select(p => p.Name)
                        .ToList();
        }

        public bool HasAttribute<T>() where T : Attribute
        {
            return _type.GetCustomAttributes(typeof(T), false).Any();
        }
    }
}