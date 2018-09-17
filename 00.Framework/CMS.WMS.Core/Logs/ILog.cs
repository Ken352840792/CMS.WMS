using Sy.Base;
using System;

namespace Sy.Core
{
    public interface ILog: IScopeDependency
    {
        void Info(string Msg);
        void Trace(string Msg);
        void Debug(string Msg);
        void Warn(string Msg);
        void Error(string Msg);
        void Fatal(string Msg);
        void Info(string Title, string Msg);
        void Error(Exception Ex);
    }
}