using Microsoft.Extensions.Logging;
using System;
using Volo.Abp;

namespace NecnatAbp.Exceptions
{
    public class BusinessRuleException : BusinessException
    {
        public BusinessRuleException(string businessRule,
            string? details = null,
            Exception? innerException = null,
            LogLevel logLevel = LogLevel.Warning)
            : base(
                  businessRule.Substring(0, businessRule.IndexOf(' ')),
                  businessRule.Substring(businessRule.IndexOf(' ') + 1),
                  details,
                  innerException,
                  logLevel)
        { }
    }
}
