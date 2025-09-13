using Exam2025.API.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AYMDating.Blazor.Data.DTO
{
    public class ReturnValue_GenericDTO<T> where T : new()
    {
        public string type { get; set; } = null!;
        public string message { get; set; } = null!;
        public T data { get; set; } = new T();
        public IEnumerable<T> list { get; set; } = null;

    }
}
