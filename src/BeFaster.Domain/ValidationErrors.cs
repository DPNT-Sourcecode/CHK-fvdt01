
namespace BeFaster.Domain
{
    public static class ValidationErrors
    {
        public static Error Input1IsRequired = new Error(1001, "Input1 is required");
        public static Error Input2IsRequired = new Error(1002, "Input2 is required");
    }
}
