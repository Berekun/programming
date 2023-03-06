using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testModel
{
    public class model
    {
        public static string GetNumberList(int a)
        {

            string list = "";
            for(int i = 0; i <= a; i++)
            {
                list += i + ",";
            }
        
            return list;
        
        }
    }
}
