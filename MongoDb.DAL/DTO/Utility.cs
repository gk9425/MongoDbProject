using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MongoDb.DAL.DTO
{
    public enum MySearchOptions
    {
        startWith = 1,
        endsWith = 2,
        isContained = 3
    }    
   

    public static class Utility
    {        
        public static Regex SearchExpressionBuilder(string searchParam, MySearchOptions options)
        {
            Regex expression = null;

            switch (options)
            {
                case MySearchOptions.startWith:
                    expression = new Regex("^" + searchParam);
                    break;
                case MySearchOptions.isContained:
                    expression = new Regex("\\s" + searchParam + "\\s");
                    break;
                case MySearchOptions.endsWith:
                    expression = new Regex(searchParam + "$");
                    break;
                default:
                    //isContained is default option
                    expression = new Regex("\\s" + searchParam + "\\s");
                    break;
            }
            return expression;
        }

    }
}
