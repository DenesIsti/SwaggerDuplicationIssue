using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SwaggerDuplicationIssueService.Controllers
{
    /// <summary>
    /// A model to use for returning errors
    /// </summary>
    public class ErrorResponseModel
    {
        public ErrorResponseModel()
        {
            Errors = new List<ErrorModel>();
        }

        [JsonConstructor]
        public ErrorResponseModel(List<ErrorModel> errors)
        {
            Errors = errors;
        }

        /// <summary>
        /// Convenience constructor for creating the model with a single error
        /// </summary>
        /// <param name="errorCode">the error code</param>
        /// <param name="errorMessage">the system error message (not a localized friendly message)</param>
        /// <param name="fullException">exception if any</param>
        /// <param name="values">additional values, could be an order number or a machine id or whatever</param>
        public ErrorResponseModel(int errorCode = 0, string errorMessage = null, Exception fullException = null, object values = null)
        {
            Errors = new List<ErrorModel>();
            if (errorCode != 0 || !string.IsNullOrEmpty(errorMessage) || fullException != null || values != null)
            {
                AddError(errorCode, errorMessage, fullException, values);
            }
        }

        /// <summary>
        /// The errors attached to the model
        /// </summary>
        public List<ErrorModel> Errors { get; }

        /// <param name="errorCode">the error code</param>
        /// <param name="errorMessage">the system error message (not a localized friendly message)</param>
        /// <param name="fullException">exception if any</param>
        /// <param name="errorValues">additional values, could be an order number or a machine id or whatever</param>
        public void AddError(int errorCode, string errorMessage, Exception fullException = null, object errorValues = null)
        {
            Errors.Add(new ErrorModel(errorCode, errorMessage, fullException, errorValues));
        }

        // helper for converting an anonymous object to a dictionary
        private static Dictionary<string, object> ToDictionary(object obj)
        {
            if (obj == null)
            {
                return new Dictionary<string, object>();
            }
            else
            {
                try
                {
                    var json = JsonConvert.SerializeObject(obj);
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    return dict;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Error model for a single error
        /// </summary>
        public class ErrorModel
        {
            [JsonConstructor]
            public ErrorModel(int errorCode, string errorMessage, Exception fullException, object errorValues)
            {
                ErrorCode = errorCode;
                FullException = fullException;
                ErrorMessage = errorMessage;
                ErrorValues = ErrorResponseModel.ToDictionary(errorValues);
            }

            public int ErrorCode { get; }

            public Exception FullException { get; }

            /// <summary>
            /// System error message (not intended to be the a localized friendly error message)
            /// </summary>
            public string ErrorMessage { get; }

            /// <summary>
            /// Additional values. This could be values needed for creating a friendly error message e.g. an order number or a machine id
            /// </summary>
            public Dictionary<string, object> ErrorValues { get; }
        }
    }
}
