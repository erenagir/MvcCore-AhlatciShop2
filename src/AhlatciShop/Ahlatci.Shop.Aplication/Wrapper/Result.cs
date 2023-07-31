using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Wrapper
{
    public class Result<T> where T : new()
    {
        public T Data { get; set; }
        public bool Success { get; set; }=true;
        public List<string> Errors { get; set; }=new List<string>();
    }
}
