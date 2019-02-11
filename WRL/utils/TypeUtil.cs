using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WRL.utils
{
    public class TypeUtil
    {
        public static Type getTypeByString(string type)
        {
            switch (type)
            {
                case "bool":
                    return Type.GetType("System.Boolean", true, true);
                case "byte":
                    return Type.GetType("System.Byte", true, true);
                case "char":
                    return Type.GetType("System.Char", true, true);
                case "decimal":
                    return Type.GetType("System.Decimal", true, true);
                case "double":
                    return Type.GetType("System.Double", true, true);
                case "float":
                    return Type.GetType("System.Single", true, true);
                case "int":
                    return Type.GetType("System.Int32", true, true);
                case "uint":
                    return Type.GetType("System.UInt32", true, true);
                case "long":
                    return Type.GetType("System.Int64", true, true);
                case "ulong":
                    return Type.GetType("System.UInt64", true, true);
                case "object":
                    return Type.GetType("System.Object", true, true);
                case "short":
                    return Type.GetType("System.Int16", true, true);
                case "ushort":
                    return Type.GetType("System.UInt16", true, true);
                case "string":
                    return Type.GetType("System.String", true, true);
                case "datetime":
                    return Type.GetType("System.DateTime", true, true);
                case "StringBuilder":
                    return Type.GetType("System.Text.StringBuilder", true, true);
                default:
                    throw new Exception("该类型不存在" + type);
            }
        }



        public static object getObjByType(string type, string value)
        {
            switch (type)
            {
                case "bool":
                    return bool.Parse(value);
                case "byte":
                    return System.Text.Encoding.Default.GetBytes(value);
                case "char":
                    return value.ToCharArray();
                case "decimal":
                    return   decimal.Parse(value); 
                case "double":
                    return double.Parse(value);
                case "float":
                    return float.Parse(value);
                case "int":
                    return int.Parse(value);
                case "uint":
                    return uint.Parse(value);
                case "long":
                    return long.Parse(value);
                case "ulong":
                    return ulong.Parse(value);
                case "object":
                case "short":
                    return short.Parse(value);
                case "ushort":
                    return ushort.Parse(value);
                case "string":
                    return value;
                case "datetime":
                    return Convert.ToDateTime(value);
                case "StringBuilder":
                    StringBuilder s = new StringBuilder(value);
                    return s;
                default:
                    throw new Exception("该类型不存在" + type);
            }
        }
    }
}
