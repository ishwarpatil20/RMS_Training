using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Common.ExceptionHandling
{
     public class RaveHRException : ApplicationException
    {

        #region Private

        private string _layerName;
        private string _className;
        private string _methodName;
        private string _stepName;
        private string _messageData;
        private int _eventID;
        private string _stackTrace;

        #endregion  Private Members

        #region Constructors

        #region ApplicationException Constructors

        /// <summary>
        /// Initializes a new instance of the RaveHRException
        /// </summary>
        public RaveHRException()
            : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the RaveHRException
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        public RaveHRException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the RaveHRException
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        /// <param name="innerException">The exception that is cause of current exception.</param>
        public RaveHRException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the RaveHRException
        /// </summary>
        /// <param name="info">The object that holds the serialized object data</param>
        /// <param name="context">The contextual information about the source or destination</param>
        public RaveHRException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }

        #endregion ApplicationException Constructors


        /// <summary>
        /// Initializes a new instance of the RaveHRException
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="innerException">"The exception that is the cause of the current exception"</param>
        /// <param name="className">The class name where the error occurred</param>
        /// <param name="methodName">The method name where the error occurred</param>
        /// <param name="stepName">The step name where the error occurred</param>
        /// <param name="data">Data relevant when the error occured</param>
        public RaveHRException(string message, Exception innerException, string className, string methodName, string stepName, string messageData)
            : base(message, innerException)
        {
            _className = className;
            _methodName = methodName;
            _stepName = stepName;
            _messageData = messageData;
        }

        /// <summary>
        /// Initializes a new instance of the RaveHRException
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="innerException">"The exception that is the cause of the current exception"</param>
        /// <param name="className">The class name where the error occurred</param>
        /// <param name="methodName">The method name where the error occurred</param>
        /// <param name="stepName">The step name where the error occurred</param>
        //public RaveHRException(string message, Exception innerException, string className, string methodName, string stepName)
        //    : this(message, innerException, className, methodName, stepName, "") { }

        /// <summary>
        /// Initializes a new instance of the RaveHRException
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="innerException">"The exception that is the cause of the current exception"</param>
        /// <param name="className">The class name where the error occurred</param>
        /// <param name="methodName">The method name where the error occurred</param>
        public RaveHRException(string message, Exception innerException, string className, string methodName)
            : this(message, innerException, className, methodName, "", "") { }

        /// <summary>
        /// Initializes a new instance of the RaveHRException
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="className">The class name where the error occurred</param>
        /// <param name="methodName">The method name where the error occurred</param>
        /// <param name="stepName">The step name where the error occurred</param>
        public RaveHRException(string message, string className, string methodName, string stepName)
            : this(message, null, className, methodName, stepName, "") { }

        /// <summary>
        /// Initializes a new instance of the RaveHRException
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="className">The class name where the error occurred</param>
        /// <param name="methodName">The method name where the error occurred</param>
        public RaveHRException(string message, string className, string methodName)
            : this(message, null, className, methodName, "", "") { }

        /// <summary>
        /// Initializes a new instance of the RaveHRException
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="innerException">"The exception that is the cause of the current exception"</param>
        /// <param name="className">The class name where the error occurred</param>
        /// <param name="methodName">The method name where the error occurred</param>
        /// <param name="stepName">The step name where the error occurred</param>
        /// <param name="data">Data relevant when the error occured</param>
        public RaveHRException(string message, Exception innerException, string source, string className, string methodName, int eventID)
            : base(message, innerException)
        {
            _layerName = source;
            _className = className;
            _methodName = methodName;
            _eventID = eventID;
            _stackTrace = innerException.StackTrace;
        }

        #endregion Constructors

        #region Public

        /// <summary>
        /// The name of the layer in which the exception was thrown
        /// </summary>
        public string LayerName
        {
            get { return _layerName; }
            set { _layerName = value; }
        }

        /// <summary>
        /// The name of the class in which the exception was thrown
        /// </summary>
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }

        /// <summary>
        /// The name of the method in which the exception was thrown
        /// </summary>
        public string MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }

        /// <summary>
        /// The step at which the exception was thrown
        /// </summary>
        public string StepName
        {
            get { return _stepName; }
            set { _stepName = value; }
        }

        /// <summary>
        /// Data relevant when the exception was thrown
        /// </summary>
        public string MessageData
        {
            get { return _messageData; }
            set { _messageData = value; }
        }

        /// <summary>
        /// eventID when the exception was thrown
        /// </summary>
        public int EventID
        {
            get { return _eventID; }
            set { _eventID = value; }
        }

        /// <summary>
        /// Stack Trace
        /// </summary>
        public string ErrorStackTrace
        {
            get { return _stackTrace; }
            set { _stackTrace = value; }
        }

        #endregion Public


    }
}
