namespace Sy.Core
{
    public interface IConfig
    {
        string ConnectionStrings(string defaultName= "default");
        string GetAppSetting(string Key);
    }
}