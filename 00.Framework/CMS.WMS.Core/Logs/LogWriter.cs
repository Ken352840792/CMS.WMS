using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Core
{
    public class LogWriter: ILog
    {
        //工厂创建一个解释器
        static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public void Debug(string Msg)
        {
            logger.Debug(Msg);
        }

        public void Error(string Msg)
        {
            logger.Error(Msg);
        }

        public void Error(Exception ex)
        {
            logger.Error(ex, ex.Message + "--" + ex.Source + "--" + ex.StackTrace);
        }

        public void Error(string Title, Exception ex)
        {
            logger.Error(ex, Title);
        }

        public void Fatal(string Msg)
        {
            logger.Fatal(Msg);
        }

        public void Info(string Msg)
        {
            logger.Info(Msg);
        }

        public void Trace(string Msg)
        {
            logger.Trace(Msg);
        }

        public void Warn(string Msg)
        {
            logger.Warn(Msg);
        }

        public void Info(string Title, string Msg)
        {
            logger.Info(Msg);
        }


        public void WriteLog(string Msg, Exception ex)
        {
            logger.Info(ex, Msg);
        }
    }
}
