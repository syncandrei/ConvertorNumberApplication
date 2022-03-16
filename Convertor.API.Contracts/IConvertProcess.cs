using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convertor.API.Contracts
{
    public interface IConvertProcess
    {
        IConvertNumber ConvertProcessNumber(string number);
    }
}
