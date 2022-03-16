using Convertor.API.Contracts;

namespace Convertor.API.Services.Services
{
    public class ConvertorService : IConvertorService
    {
        private readonly IConvertProcess _convert;

        public ConvertorService(IConvertProcess convert)
        {
            _convert = convert;
        }

        public IConvertNumber GetNumberToWords(string number)
        {
            return _convert.ConvertProcessNumber(number);
        }       
    }
}
