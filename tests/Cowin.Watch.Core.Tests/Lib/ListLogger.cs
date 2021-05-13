using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Cowin.Watch.Core.Tests.Lib
{

    public class NullScope : IDisposable
    {
        public static NullScope Instance { get; } = new NullScope();

        private NullScope() { }

        public void Dispose() { }
    }

    public class ListLogger : ILogger
    {
        public IList<string> Logs;

        public IDisposable BeginScope<TState>(TState state) => NullScope.Instance;

        public bool IsEnabled(LogLevel logLevel) => false;

        public ListLogger()
        {
            this.Logs = new List<string>();
        }

        public void Log<TState>(LogLevel logLevel,
                                EventId eventId,
                                TState state,
                                Exception exception,
                                Func<TState, Exception, string> formatter)
        {
            string message = formatter(state, exception);
            this.Logs.Add(message);
        }

        public static ILogger GetInstance() => new ListLogger();
    }

    public class ListLogger<T> : ILogger<T> where T: class
    {
        private readonly ListLogger baseLogger;
        private readonly string contextType;

        public ListLogger() : base()
        {
            baseLogger = new ListLogger();
            contextType = typeof(T).ToString();
        }

        public IDisposable BeginScope<TState>(TState state) => baseLogger.BeginScope(state);

        public bool IsEnabled(LogLevel logLevel) => baseLogger.IsEnabled(logLevel);

        public new void Log<TState>(LogLevel logLevel,
                                EventId eventId,
                                TState state,
                                Exception exception,
                                Func<TState, Exception, string> formatter)
        {
            Func<TState, Exception, string> formatterWithContext = (state, exception) => $"{contextType}-{formatter(state, exception)}";
            baseLogger.Log<TState>(logLevel, eventId, state, exception, formatterWithContext);
        }
    }
}
