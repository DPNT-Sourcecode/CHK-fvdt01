﻿
namespace BeFaster.Domain
{
    public static class ValidationErrors
    {
        public static Error Param1IsRequired = new Error(1001, "Param1 is required");
        public static Error Param2IsRequired = new Error(1002, "Param2 is required");
        public static Error Param1IsInvalid = new Error(1003, "Param1 must be a positive number between 0 and 100");
        public static Error Param2IsInvalid = new Error(1004, "Param2 must be a positive number between 0 and 100");

        public static Error MessageIsRequired = new Error(1005, "Message is required");
        public static Error SKUSIsRequired = new Error(1006, "SKUS are required");
        public static Error SKUSIsInvalid = new Error(1007, "SKUS is invalid");
    }
}

