using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpFaker
{
    class Program
    {
        static void Main(string[] args)
        {
            ReturnHandler test = WriteIt;
            HttpHelper.HttpPost("http://dev.emailuo.com/api/v1/Feedback", new Test { userId = 3,content="该消息来自一体机-测试Api",contact="18321950875" }, test);
        }
        static void WriteIt(string res)
        {
            Console.WriteLine(res);
        }
    }

    public class Test
    {
        public int userId { get; set; }
        public string content{get;set;}
        public string contact { get; set; }
    }
}
