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

        public static string CapitalizeFirstLetter(this string str)
        {
            string[] strings = str.Split("_");
            for (int i = 0; i < strings.Length; i++)
            {
                char[] characters = strings[i].ToCharArray();
                characters[0] = characters[0].ToString().ToUpper()[0];

                strings[i] = String.Join("", characters);
            }
            
            return String.Join("", strings);
        }
    }
}