using System;
using System.Collections.Generic;
using System.Linq;

namespace Ottobo.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        
        public static string ToSnakeCase(this string str)
        {
            return string.Concat(str.Select((character, index) =>
                    index > 0 && char.IsUpper(character)
                        ? "_" + character
                        : character.ToString()))
                .ToLower();
        }
    }
}