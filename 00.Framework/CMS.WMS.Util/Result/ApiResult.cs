using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Util
{
    /// <summary>
    /// 输出参数
    /// </summary>
    [DataContract(Namespace = "http://serialize")]
    [KnownType(typeof(ApiResult))]
    public class ApiResult
    {

        public ApiResult() { }

        public ApiResult(int Status, string ErrorCode, object Data)
        {
            this.Status = Status;
            this.ErrorCode = ErrorCode;
            this.Data = Data;
        }


        public ApiResult(int Status, object Data)
        {
            this.Status = Status;
            this.Data = Data;
        }

        /// <summary>
        /// 状态值（0.失败1.成功）
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [DataMember]
        public string ErrorCode { get; set; }
        /// <summary>
        /// 附加值
        /// </summary>
        [DataMember]
        public object Data { get; set; }
 
    }
}
