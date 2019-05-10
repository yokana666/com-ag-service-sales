using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.Models
{
    public class Rate : BaseModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }

    }
}
