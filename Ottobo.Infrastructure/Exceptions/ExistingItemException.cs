using System;

namespace Ottobo.Infrastructure.Exceptions
{
    public class ExistingItemException:Exception
    {
        
        public ExistingItemException(string message):base(message)
        {
           
        }
    }
}