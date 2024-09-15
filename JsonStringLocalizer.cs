using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace api
{
    public class JsonStringLocalizer() : IStringLocalizer
    {
        private readonly JsonSerializer _serializer = new();
        public LocalizedString this[string name]
        {
            get
            {
                return new(name, GetString(name));
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var value = this[name];
                return !value.ResourceNotFound ? new LocalizedString(name, string.Format(value, arguments)) : value;
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            var fullPath = Path.GetFullPath(filePath);
            using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader reader = new(stream);
            using JsonTextReader jsonTextReader = new(reader);

            while (jsonTextReader.Read())
            {
                if (jsonTextReader.TokenType != JsonToken.PropertyName)
                    continue;
                var key = jsonTextReader.Value as string;
                jsonTextReader.Read();

                var value = _serializer.Deserialize<string>(jsonTextReader);
                yield return new LocalizedString(key, value);
            }

        }

        private string GetString(string key)
        {
            var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            var fullPath = Path.GetFullPath(filePath);
            if (File.Exists(fullPath))
                return GetValueFromJson(key, fullPath);
            return string.Empty;
        }


        private string GetValueFromJson(string propertyName, string filePath)
        {
            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }
            using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader reader = new(stream);
            using JsonTextReader jsonTextReader = new(reader);

            while (jsonTextReader.Read())
            {
                if (jsonTextReader.TokenType == JsonToken.PropertyName && jsonTextReader.Value as string == propertyName)
                {
                    jsonTextReader.Read();
                    return _serializer.Deserialize<string>(jsonTextReader) ?? string.Empty;
                }
            }

            return string.Empty;
        }
    }
}