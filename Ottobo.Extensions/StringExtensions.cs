using System;
using System.Collections.Generic;

namespace Ottobo.Extensions
{
    public static class StringExtensions
    {
        public static string ConvertToSnakeCase(this string value)
        {
            List<char> snakeCaseList = new List<char>();
            int n = value.Length; 
            string str1 = ""; 
      
            for (int i = 0; i < n; i++) 
            {
                if (Char.IsUpper(value[i]))
                {
                    snakeCaseList.Add('_');
                }
                
                snakeCaseList.Add(Char.ToLower(value[i]));
                    
              
            }

            return new string(snakeCaseList.ToArray());
        }
    }
}