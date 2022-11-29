using System;
using InGame.WebUI.Enum;

namespace InGame.WebUI.Models.Common
{
    public class ServiceResult
    {
        public ServiceResult(ServiceResultType serviceResultType) { ServiceResultType = serviceResultType; }

        public string Message { get; set; }
        public ServiceResultType ServiceResultType { get; set; }
        public int ExceptionCode { get; set; }
        public Exception Exception { get; set; }
        public object Data { get; set; }
    }
}
