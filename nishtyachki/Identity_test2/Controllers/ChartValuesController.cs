using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdminApp.Controllers
{    
    [RoutePrefix("api/chartValues")]
    public class ChartValuesController : ApiController
    {
        private class TempData
        {
            public string[] labels;
            public int[] numbers;
        }

        // GET api/<controller>
        [Route("{count}")]
        [AllowAnonymous]
        public object Get(int count)
        {
            TempData data = new TempData();

            data.numbers = new int[count];
            data.labels = new string[count];

            for (int i = 0; i < data.numbers.Length; i++)
            {
                data.numbers[i] = data.numbers.Length - i;
                data.labels[i] = "Abc" + i;
            }

            return data;
        }
    }
}