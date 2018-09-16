using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Util
{
    public static class RandomHelper
    {
        //构造随机数种子
        public static int GetRandomSeed()
        {
            byte[] bytes = new byte[5];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        //生成随机数
        public static string rnd()
        {
            Random ran = new Random(GetRandomSeed());
            int cnt = ran.Next(0, 99999); 
            return DateTime.Now.ToString("SOPyyyyMMdd") + cnt.ToString().PadLeft(5, '0');
        }
    }
}
