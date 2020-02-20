using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;

namespace WebRunLocal.Managers
{
    class DynamicLoadDLL
    {
        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress", SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        private IntPtr hModule = IntPtr.Zero;
        private IntPtr farProc = IntPtr.Zero;

        /// <summary>
        /// 获取系统错误信息
        /// </summary>
        /// <param name="errCode"></param>
        /// <returns></returns>
        public static string GetSysErrMsg(int errCode)
        {
            Win32Exception win32exception = new Win32Exception(errCode);
            return win32exception.Message;
        }

        /// <summary>
        /// 装载DLL
        /// </summary>
        /// <param name="lpFileName"></param>
        public void LoadDll(string lpFileName)
        {
            hModule = LoadLibrary(lpFileName);
            if (hModule == IntPtr.Zero)
                throw (new Exception(" 没有找到 :" + lpFileName + "."));
        }

        /// <summary>
        /// 装载DLL
        /// </summary>
        /// <param name="HMODULE"></param>
        public void LoadDll(IntPtr HMODULE)
        {
            if (HMODULE == IntPtr.Zero)
            {
                throw (new Exception(" 所传入的函数库模块的句柄 HMODULE 为空 ."));
            }
            hModule = HMODULE;
        }

        /// <summary>
        /// 获得函数指针
        /// </summary>
        /// <param name="lpProcName"> 调用函数的名称 </param>
        public void LoadFun(string lpProcName)
        {
            if (hModule == IntPtr.Zero)
            {
                throw (new Exception(" 函数库模块的句柄为空 , 请确保已进行 LoadDll 操作 !"));
            }
            // 取得函数指针
            farProc = GetProcAddress(hModule, lpProcName);
            // 若函数指针，则抛出异常
            if (farProc == IntPtr.Zero)
            {
                throw (new Exception(" 没有找到 :" + lpProcName + " 这个函数的入口点 "));
            }
        }

        /// <summary>
        /// 获得函数指针
        /// </summary>
        /// <param name="lpFileName"> 包含需调用函数的 DLL 文件名 </param>
        /// <param name="lpProcName"> 调用函数的名称 </param>
        public void LoadFun(string lpFileName, string lpProcName)
        { // 取得函数库模块的句柄
            hModule = LoadLibrary(lpFileName);
            // 若函数库模块的句柄为空，则抛出异常
            if (hModule == IntPtr.Zero)
            {
                throw (new Exception(" 没有找到 :" + lpFileName + "."));
            }

            // 取得函数指针
            farProc = GetProcAddress(hModule, lpProcName);
            // 若函数指针，则抛出异常
            if (farProc == IntPtr.Zero)
            {
                throw (new Exception(" 没有找到 :" + lpProcName + " 这个函数的入口点 "));
            }
        }

        /// <summary>
        /// 卸载 Dll
        /// </summary>
        public void UnLoadDll()
        {
            FreeLibrary(hModule);
            hModule = IntPtr.Zero;
            farProc = IntPtr.Zero;
        }

        /// <summary>
        /// 调用所设定的函数
        /// </summary>
        /// <param name="ObjArray_Parameter"> 实参 </param>
        /// <param name="TypeArray_ParameterType"> 实参类型 </param>
        /// <param name="ModePassArray_Parameter"> 实参传送方式 </param>
        /// <param name="Type_Return"> 返回类型 </param>
        /// <returns> 返回所调用函数的 object</returns>
        public object Invoke(object[] ObjArray_Parameter, Type[] TypeArray_ParameterType, WebRunLocal.Entity.PublicValue.ModePass[] ModePassArray_Parameter, Type Type_Return)
        {
            // 下面 3 个 if 是进行安全检查 , 若不能通过 , 则抛出异常
            if (hModule == IntPtr.Zero)
                throw (new Exception(" 函数库模块的句柄为空 , 请确保已进行 LoadDll 操作 !"));
            if (farProc == IntPtr.Zero)
                throw (new Exception(" 函数指针为空 , 请确保已进行 LoadFun 操作 !"));
            if (ObjArray_Parameter.Length != ModePassArray_Parameter.Length)
                throw (new Exception(" 参数个数及其传递方式的个数不匹配 ."));
            // 下面是创建 MyAssemblyName 对象并设置其 Name 属性
            AssemblyName MyAssemblyName = new AssemblyName();
            MyAssemblyName.Name = "InvokeFun";
            // 生成单模块配件
            AssemblyBuilder MyAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(MyAssemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder MyModuleBuilder = MyAssemblyBuilder.DefineDynamicModule("InvokeDll");
            // 定义要调用的方法 , 方法名为“ MyFun ”，返回类型是“ Type_Return ”参数类型是“ TypeArray_ParameterType ”
            MethodBuilder MyMethodBuilder = MyModuleBuilder.DefineGlobalMethod("MyFun", MethodAttributes.Public | MethodAttributes.Static, Type_Return, TypeArray_ParameterType);
            // 获取一个 ILGenerator ，用于发送所需的 IL
            ILGenerator IL = MyMethodBuilder.GetILGenerator();
            int i;
            for (i = 0; i < ObjArray_Parameter.Length; i++)
            {// 用循环将参数依次压入堆栈
                switch (ModePassArray_Parameter[i])
                {
                    case WebRunLocal.Entity.PublicValue.ModePass.ByValue:
                        IL.Emit(OpCodes.Ldarg, i);
                        break;
                    case WebRunLocal.Entity.PublicValue.ModePass.ByRef:
                        IL.Emit(OpCodes.Ldarga, i);
                        break;
                    case WebRunLocal.Entity.PublicValue.ModePass.ByReturn:
                        IL.Emit(OpCodes.Ldarg, i);
                        break;
                    default:
                        throw (new Exception(" 第 " + (i + 1).ToString() + " 个参数没有给定正确的传递方式 ."));
                }
            }
            if (IntPtr.Size == 4)
            {// 判断处理器类型
                IL.Emit(OpCodes.Ldc_I4, farProc.ToInt32());
            }
            else if (IntPtr.Size == 8)
            {
                IL.Emit(OpCodes.Ldc_I8, farProc.ToInt64());
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
            IL.EmitCalli(OpCodes.Calli, CallingConvention.StdCall, Type_Return, TypeArray_ParameterType);
            IL.Emit(OpCodes.Ret); // 返回值
            MyModuleBuilder.CreateGlobalFunctions();
            // 取得方法信息
            MethodInfo MyMethodInfo = MyModuleBuilder.GetMethod("MyFun");
            return MyMethodInfo.Invoke(null, ObjArray_Parameter);// 调用方法，并返回其值
        }


        /// <summary>
        /// 调用所设定的函数
        /// </summary>
        /// <param name="IntPtr_Function"> 函数指针 </param>
        /// <param name="ObjArray_Parameter"> 实参 </param>
        /// <param name="TypeArray_ParameterType"> 实参类型 </param>
        /// <param name="ModePassArray_Parameter"> 实参传送方式 </param>
        /// <param name="Type_Return"> 返回类型 </param>
        /// <returns> 返回所调用函数的 object</returns>
        public object Invoke(IntPtr IntPtr_Function, object[] ObjArray_Parameter, Type[] TypeArray_ParameterType, WebRunLocal.Entity.PublicValue.ModePass[] ModePassArray_Parameter, Type Type_Return)
        {
            // 下面 2 个 if 是进行安全检查 , 若不能通过 , 则抛出异常
            if (hModule == IntPtr.Zero)
            {
                throw (new Exception(" 函数库模块的句柄为空 , 请确保已进行 LoadDll 操作 !"));
            }

            if (IntPtr_Function == IntPtr.Zero)
            {
                throw (new Exception(" 函数指针 IntPtr_Function 为空 !"));
            }

            farProc = IntPtr_Function;
            return Invoke(ObjArray_Parameter, TypeArray_ParameterType, ModePassArray_Parameter, Type_Return);
        }

    }
}
