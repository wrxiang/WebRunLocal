using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebRunLocal.Entity;
using WebRunLocal.Utils;

namespace WebRunLocal.Managers
{
    /// <summary>
    /// 动态调用DLL实现类
    /// </summary>
    class DynamicInvokeDLL : IDynamicInvoke
    {

        public ReturnDTO invokeLocalApp(Entity.InputDTO inputDTO, string localAppPath)
        {

            ReturnDTO returnDTO = new ReturnDTO();

            List<ParamDTO> paramDTOLIst = inputDTO.param;
            Type[] parameterTypes = new Type[paramDTOLIst.Count]; // 实参类型
            object[] parameters = new object[paramDTOLIst.Count];//实参
            Type typeReturn = TypeConversionUtil.getTypeByString(inputDTO.returnType); //返回类型
            PublicValue.ModePass[] themode = new PublicValue.ModePass[paramDTOLIst.Count]; //传递方式

            for (int i = 0; i < paramDTOLIst.Count; i++)
            {
                parameterTypes[i] = TypeConversionUtil.getTypeByString(paramDTOLIst[i].type);
                parameters[i] = TypeConversionUtil.getObjByType(paramDTOLIst[i].type, paramDTOLIst[i].value);
                themode[i] = (PublicValue.ModePass)int.Parse(paramDTOLIst[i].mode);
            }

            Directory.SetCurrentDirectory(Path.GetDirectoryName(localAppPath));
            DynamicLoadDLL dld = new DynamicLoadDLL();
            dld.LoadDll(localAppPath);
            dld.LoadFun(inputDTO.method);
            object result = dld.Invoke(parameters, parameterTypes, themode, typeReturn);

            for (int i = 0; i < themode.Length; i++)
            {
                if (themode[i] != PublicValue.ModePass.ByValue)
                {
                    returnDTO.values.Add(parameters[i].ToString());
                }
            }
            returnDTO.result = result;

            return returnDTO;
        }
    }
}
