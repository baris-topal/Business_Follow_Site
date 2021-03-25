using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Proje.ToDo.Business.Interfaces
{
    public  interface ICustomLogger
    {
        public void LogError(string meesage);
    }
}
