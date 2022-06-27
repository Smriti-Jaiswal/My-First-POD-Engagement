using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace api.Helper
{
    public class ErrorResult
    {
        public string Message { get; set; }

        public ErrorResult()
        {
        }

        public  ErrorResult(ModelStateDictionary.ValueEnumerable modelState)
        {
            // This will take the error message coming directly from modelState
            foreach (var value in modelState)
            {
                if (value.Errors.Count > 0)
                {
                    Message = value.Errors.FirstOrDefault().ErrorMessage;
                    break;
                }
            }
        }
    }
}