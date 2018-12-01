using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EFCore.ConsoleClient
{
    public class JsonConverter<T> : ValueConverter<T, string>
    {
        public JsonConverter(ConverterMappingHints mappingHints = null)
        : base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
        { }
      
        static Expression<Func<T, string>> convertToProviderExpression = v => JsonConvert.SerializeObject(v);
        static Expression<Func<string, T>> convertFromProviderExpression = v => JsonConvert.DeserializeObject<T>(v);

    }
}
