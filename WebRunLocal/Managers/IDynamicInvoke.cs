using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebRunLocal.Entity;

namespace WebRunLocal.Managers
{
    /// <summary>
    /// 调用第三方程序所使用的接口
    /// </summary>
    interface IDynamicInvoke
    {
        ReturnDTO invokeLocalApp(InputDTO inputDTO, string localAppPath);
    }
}
