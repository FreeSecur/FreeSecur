using FreeSecur.API.Core.GeneralExtensions;
using FreeSecur.API.Core.Reflection;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FreeSecur.API.Core.Reflection
{
    public class StringInterpolationService
    {
        private readonly ReflectionService _reflectionService;

        public StringInterpolationService(ReflectionService reflectionService)
        {
            _reflectionService = reflectionService;
        }

        public string Interpolate<T>(string input, T model)
        {
            if (input is null) throw new ArgumentNullException(nameof(input));

            var matchedKeys = input.GetMatchedValues('{', '}');
            var matchKeyValuePairs = matchedKeys.Select(key => GetMatchKeyValuePair(model, key));
            var resultingValue = input;

            foreach(var matchPair in matchKeyValuePairs)
            {
                resultingValue = matchPair.Replace(resultingValue);
            }

            return resultingValue;
        }

        private MatchedPair GetMatchKeyValuePair<T>(T model, string key)
        {
            if (key is null) throw new ArgumentNullException(nameof(key));

            var fullKey = key;
            var parsedKey = fullKey.Substring(1, fullKey.Length - 2);

            var value = _reflectionService.GetValueForKey(parsedKey, model);
            var stringValue = $"{value}";

            return new MatchedPair(fullKey, stringValue);
        }

        private class MatchedPair
        {
            private readonly string _key;
            private readonly string _value;

            public MatchedPair(string key, string value)
            {
                _key = key;
                _value = value;
            }
            
            public string Replace(string input)
            {
                return input.Replace(_key, _value);
            }
        }
    }
}
