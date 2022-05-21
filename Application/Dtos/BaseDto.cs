using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class BaseDto<T>
    {
        public BaseDto(bool isSuccess, List<string> messages, T data)
        {
            IsSuccess = isSuccess;
            Messages = messages;
            Data = data;
        }
        public bool IsSuccess { get; private set; }
        public List<string> Messages { get; private set; }
        public T Data { get; private set; }
    }
    public class BaseDto
    {
        public BaseDto(bool isSuccess, List<string> messages)
        {
            IsSuccess = isSuccess;
            Messages = messages;
        }
        public bool IsSuccess { get; private set; }
        public List<string> Messages { get; private set; }
    }
}
