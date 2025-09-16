using Exam2025.PL.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Exam2025.DTO
{
    public class ReturnValue_GenericDTO<T> where T : new()
    {
        public string type { get; set; } = null!;
        public string message { get; set; } = null!;
        public T data { get; set; } = new T();
        public List<T> list { get; set; } = new List<T>();

    }
}
