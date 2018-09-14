using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace Sy.Util
{
    /// <summary>
    /// 目录程序集查找器
    /// </summary>
    public class DirectoryAssemblyFinder
    {
        private static readonly IDictionary<string, Assembly[]> AssembliesesDict = new Dictionary<string, Assembly[]>();
        private readonly string _path;

        /// <summary>
        /// 初始化一个<see cref="DirectoryAssemblyFinder"/>类型的新实例
        /// </summary>
        public DirectoryAssemblyFinder()
            : this(GetBinPath())
        { }

        /// <summary>
        /// 初始化一个<see cref="DirectoryAssemblyFinder"/>类型的新实例
        /// </summary>
        public DirectoryAssemblyFinder(string path)
        {
            _path = path;
        }

        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public Assembly[] Find(Func<Assembly, bool> predicate)
        {
            return FindAll().Where(predicate).ToArray();
        }

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <returns></returns>
        public Assembly[] FindAll()
        {
            if (AssembliesesDict.ContainsKey(_path))
            {
                return AssembliesesDict[_path];
            }
            string[] files = Directory.GetFiles(_path, "Sy.*.dll", SearchOption.TopDirectoryOnly)
                .Concat(Directory.GetFiles(_path, "Sy.*.exe", SearchOption.TopDirectoryOnly))
                .ToArray();
            Assembly[] assemblies = files.Select(Assembly.LoadFrom).Distinct().ToArray();
            AssembliesesDict.Add(_path, assemblies);
            return assemblies;
        }

        private static string GetBinPath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (path.Contains("\\bin"))
            {
                return path;
            }
            else
            {
                return path == Environment.CurrentDirectory + "\\" ? path : Path.Combine(path, "bin");
            }

        }
    }
}
