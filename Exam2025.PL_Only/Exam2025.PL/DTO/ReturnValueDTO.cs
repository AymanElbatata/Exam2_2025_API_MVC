using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Exam2025.PL.DTO
{
    public static class EnumHelpers
    {

        //public static T GetEnumObjectByValue<T>(int valueId)
        //{
        //    return (T)Enum.ToObject(typeof(T), valueId);
        //}

    }
    public enum ReturnType : byte
    {
        Success,
        Failed
    }

    public class ReturnValueDTO
    {
        //public ReturnType Type { get; set; }
        public string? type { get; set; } = string.Empty;
        public string? message { get; set; } = string.Empty;
        public object? data { get; set; } = new object();


        public ReturnValueDTO(ReturnType TypeValue, string? message, object? data = null)
        {
            //this.Type = Type;
            type = EnumToString(TypeValue).ToString();
            this.message= message;
            this.data = data ?? new object() ;
        }

        public static string EnumToString(ReturnType val)
        {
            switch (val)
            {
                case ReturnType.Success:
                    return "Success";
                case ReturnType.Failed:
                    return "Failed";
                default:
                    return "Unknown value";
            }
        }

    }
}
