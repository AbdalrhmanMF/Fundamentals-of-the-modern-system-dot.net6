using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCore
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly IDistributedCache _cache;
        public JsonStringLocalizer(IDistributedCache cashe)
        {
            _cache = cashe;
        }

        private readonly JsonSerializer _serializer = new();
        public LocalizedString this[string name]
        {
            get
            {
                var value = Getstring(name);
                return new LocalizedString(name, value);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var actualVal = this[name];
                return !actualVal.ResourceNotFound
                    ? new LocalizedString(name, string.Format(actualVal.Value, arguments))
                    : actualVal;
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";

            using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader sr = new(stream);
            using JsonTextReader JReader = new JsonTextReader(sr);

            while (JReader.Read())
            {
                if (JReader.TokenType != JsonToken.PropertyName)
                    continue;

                var key = JReader.Value as string;
                JReader.Read();
                var value = _serializer.Deserialize<string>(JReader);

                yield return new LocalizedString(key, value);
            }
        }

        private string Getstring(string key)
        {
            var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            var fullFilePath = Path.GetFullPath(filePath);
            if (File.Exists(fullFilePath))
            {
                //locale_en-Us_welcome
                //locale_ar-EG_welcome
                var chasheKey = $"locale_{Thread.CurrentThread.CurrentCulture.Name}_{key}";
                var casheValue = _cache.GetString(chasheKey);
                if (!string.IsNullOrEmpty(casheValue))
                    return casheValue;

                var result = GetValueFromJson(key, fullFilePath);
                if (!string.IsNullOrEmpty(result))
                    _cache.SetString(chasheKey, result);

                return result;
            }
            return string.Empty;
        }

        private string GetValueFromJson(string PropName, string FilePath)
        {
            if (string.IsNullOrEmpty(PropName) || string.IsNullOrEmpty(FilePath))
                return string.Empty;

            using FileStream stream = new(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader sr = new(stream);
            using JsonTextReader JReader = new JsonTextReader(sr);
            while (JReader.Read())
            {
                if (JReader.TokenType == JsonToken.PropertyName && JReader.Value as string == PropName)
                {
                    JReader.Read();
                    return _serializer.Deserialize<string>(JReader);
                }
            }
            return string.Empty;
        }
    }
}
