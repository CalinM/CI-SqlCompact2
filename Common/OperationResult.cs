using Common;
using System;
using System.Text;

namespace Common
{
    public class OperationResult
    {
        public bool Success { get; set; } = true;

        public string CustomErrorMessage { get; set; }

        private Exception _exception;
        public Exception Exception
        {
            get { return _exception; }
            set
            {
                if (value != null)
                {
                    _exception = value;
                    CustomErrorMessage = GetErrorMessage(value);
                }
            }
        }

        public object AdditionalDataReturn { get; set; }

        public static string GetErrorMessage(Exception ex, bool includeStackTrace = false)
        {
            var msg = new StringBuilder();
            BuildErrorMessage(ex, ref msg);

            if (includeStackTrace)
            {
                msg.Append("\n");
                msg.Append(ex.StackTrace);
            }

            return msg.ToString();
        }

        private static void BuildErrorMessage(Exception ex, ref StringBuilder msg)
        {
            if (ex == null) return;

            if (msg.IndexOf(ex.Message) == -1)
            {
                msg.Append(ex.Message);
                msg.Append("\n");
            }

            if (ex.InnerException != null)
            {
                BuildErrorMessage(ex.InnerException, ref msg);
            }
        }

        public OperationResult FailWithMessage(string message)
        {
            Success = false;
            CustomErrorMessage = message;

            return this;
        }

        public OperationResult FailWithMessage(Exception ex)
        {
            Success = false;
            Exception = ex;
            CustomErrorMessage = GetErrorMessage(ex);

            return this;
        }
    }

    public class TechnicalDetailsImportError
    {
        public string FilePath { get; set; }

        public string ErrorMesage { get; set; }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}", FilePath, Environment.NewLine, ErrorMesage);
        }
    }
}
