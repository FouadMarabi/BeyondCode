namespace SOLID._5_Dependency_Inversion_Principle__DIP_
{
    namespace Problem
    {
        public class FileLogger
        {
            public void LogMessage(string aStackTrace)
            {
                //code to log stack trace into a file.
            }
        }
        public class ExceptionLogger
        {
            public void LogIntoFile(Exception aException)
            {
                FileLogger objFileLogger = new FileLogger();
                objFileLogger.LogMessage(GetUserReadableMessage(aException));
            }
            private string GetUserReadableMessage(Exception ex)
            {
                return string.Empty;
            }
        }

        public class DataExporter
        {
            public void ExportDataFromFile()
            {
                try
                {
                    throw new Exception("Something went wrong.");
                }
                catch (Exception ex)
                {
                    new ExceptionLogger().LogIntoFile(ex);
                }
            }
        }

        namespace NewRequirement_AddDBLogger_BadSolution
        {
            public class DbLogger
            {
                public void LogMessage(string aMessage)
                {
                    //Code to write message in the database.
                }
            }
            public class FileLogger
            {
                public void LogMessage(string aStackTrace)
                {
                    //code to log stack trace into a file.
                }
            }
            public class ExceptionLogger
            {
                public void LogIntoFile(Exception aException)
                {
                    FileLogger objFileLogger = new FileLogger();
                    objFileLogger.LogMessage(GetUserReadableMessage(aException));
                }
                public void LogIntoDataBase(Exception aException)
                {
                    DbLogger objDbLogger = new DbLogger();
                    objDbLogger.LogMessage(GetUserReadableMessage(aException));
                }
                private string GetUserReadableMessage(Exception ex)
                {
                    return string.Empty;
                }
            }
            public class DataExporter
            {
                public void ExportDataFromFile()
                {
                    try
                    {
                        //code to export data from files to database.
                    }
                    catch (IOException ex)
                    {
                        new ExceptionLogger().LogIntoDataBase(ex);
                    }
                    catch (Exception ex)
                    {
                        new ExceptionLogger().LogIntoFile(ex);
                    }
                }
            }

        }
    }

    namespace Solution
    {
        public interface ILogger
        {
            public Type LoggerType { get; }

            void LogMessage(string aString);
        }

        public class DbLogger : ILogger
        {
            public Type LoggerType
            {
                get { return typeof(IOException); }
            }

            public void LogMessage(string aMessage)
            {
                //Code to write message in database.
            }
        }
        public class FileLogger : ILogger
        {
            public Type LoggerType
            {
                get { return typeof(Exception); }
            }

            public void LogMessage(string aStackTrace)
            {
                //code to log stack trace into a file.
            }
        }

        public class ExceptionLogger // High Level Module
        {
            private ILogger _logger;
            public ExceptionLogger(ILogger aLogger)
            {
                this._logger = aLogger;
            }
            public void LogException(Exception aException)
            {
                string strMessage = GetUserReadableMessage(aException);
                this._logger.LogMessage(strMessage);
            }
            private string GetUserReadableMessage(Exception aException)
            {
                return string.Empty;
            }
        }

        public class DataExporter
        {
            public void ExportDataFromFile()
            {
                ExceptionLogger _exceptionLogger;
                try
                {
                    //code to export data from files to database.
                }
                catch (IOException ex)
                {
                    _exceptionLogger = new ExceptionLogger(new DbLogger());
                    _exceptionLogger.LogException(ex);
                }
                catch (Exception ex)
                {
                    _exceptionLogger = new ExceptionLogger(new FileLogger());
                    _exceptionLogger.LogException(ex);
                }
            }
        }

        namespace NewRequirement_EventLogger
        {
            public class EventLogger : ILogger
            {
                public Type LoggerType
                {
                    get { return typeof(InvalidProgramException); }
                }

                public void LogMessage(string aMessage)
                {
                    //Code to write a message in system's event viewer.
                }
            }
            public class DataExporter
            {
                public void ExportDataFromFile()
                {
                    ExceptionLogger _exceptionLogger;
                    try
                    {
                        //code to export data from files to database.
                    }
                    catch (IOException ex)
                    {
                        _exceptionLogger = new ExceptionLogger(new DbLogger());
                        _exceptionLogger.LogException(ex);
                    }
                    catch (InvalidProgramException ex)
                    {
                        _exceptionLogger = new ExceptionLogger(new EventLogger());
                        _exceptionLogger.LogException(ex);
                    }
                    catch (Exception ex)
                    {
                        _exceptionLogger = new ExceptionLogger(new FileLogger());
                        _exceptionLogger.LogException(ex);
                    }
                }
            }

            namespace FactoryPattern_ToImproveCatchBlocks
            {

                public class DataExporter
                {
                    private ILoggerFactory _factory;
                    public DataExporter(ILoggerFactory factory)
                    {
                        _factory = factory;
                    }
                    public void ExportDataFromFile()
                    {
                        try
                        {
                            //code to export data from files to database.
                        }

                        catch (Exception ex)
                        {
                            new ExceptionLogger(_factory.GetLogger(ex)).LogException(ex);
                        }
                    }
                }

                public interface ILoggerFactory
                {
                    ILogger GetLogger(Exception ex);
                }
                public class LoggerFactoryV1 : ILoggerFactory
                {
                    private readonly Dictionary<Type, ILogger> _loggers;
                    public LoggerFactoryV1()
                    {
                        _loggers = new Dictionary<Type, ILogger>
                            {
                                { typeof(IOException), new DbLogger() },
                                { typeof(Exception), new FileLogger() },
                                {typeof(InvalidProgramException), new EventLogger() }
                            };
                    }
                    public ILogger GetLogger(Exception ex)
                    {
                        return _loggers.GetValueOrDefault(ex.GetType(), new FileLogger());
                    }
                }


                namespace IoC_Container
                {
                    // In real world example you will add the logger types in IoC container
                    public class LoggerFactoryV2 : ILoggerFactory
                    {
                        private readonly IEnumerable<ILogger> _loggers;
                        public LoggerFactoryV2(IEnumerable<ILogger> loggers)
                        {
                            _loggers = loggers;
                        }

                        public ILogger GetLogger(Exception ex)
                        {
                            var logger = _loggers.FirstOrDefault(l => l.GetType() == ex.GetType());

                            if (logger is null)
                            {
                                // Default logger
                                logger = new FileLogger();
                            }

                            return logger;
                        }
                    }
                }
            }

        }
    }
}
