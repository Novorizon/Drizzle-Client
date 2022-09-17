using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace JsonExtension
{
    public class PropertySerializerContractResolver : DefaultContractResolver
    {
        private readonly Dictionary<Type, HashSet<string>> serializes;
        private readonly Dictionary<Type, HashSet<string>> ignores;

        public PropertySerializerContractResolver()
        {
            serializes = new Dictionary<Type, HashSet<string>>();
            ignores = new Dictionary<Type, HashSet<string>>();
        }

        public void IgnoreProperty(Type type, params string[] jsonPropertyNames)
        {
            if (!ignores.ContainsKey(type))
                ignores[type] = new HashSet<string>();

            foreach (var prop in jsonPropertyNames)
                ignores[type].Add(prop);
        }

        public void ShouldSerializeProperty(Type type, params string[] jsonPropertyNames)
        {
            if (!serializes.ContainsKey(type))
                serializes[type] = new HashSet<string>();

            foreach (var prop in jsonPropertyNames)
                serializes[type].Add(prop);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (IsSerialized(property.DeclaringType, property.PropertyName))
            {
                property.ShouldSerialize = i => true;
                property.Ignored = false;
            }

            if (IsIgnored(property.DeclaringType, property.PropertyName))
            {
                property.ShouldSerialize = i => false;
                property.Ignored = true;
            }


            return property;
        }

        private bool IsSerialized(Type type, string jsonPropertyName)
        {
            if (!serializes.ContainsKey(type))
                return false;

            return serializes[type].Contains(jsonPropertyName);
        }


        private bool IsIgnored(Type type, string jsonPropertyName)
        {
            if (!ignores.ContainsKey(type))
                return false;

            return ignores[type].Contains(jsonPropertyName);
        }

    }
}