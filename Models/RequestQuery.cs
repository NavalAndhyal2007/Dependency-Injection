using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIWebApiTutorial.Models
{
    public class RequestQuery
    {
        public int? MinSal { get; set; } = 10000;
        public int? MaxSal { get; set; } = 100000;

        public int PageSize { get; set; } = 100;

        public int Page { get; set; } = 1;

    }
}
