using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace NetReflection
{
    public static class ReflectionExtends
    {
        public static dynamic DictionaryToObject(this IDictionary<string, object> dict)
        {
            AssemblyName aName = new AssemblyName("dynamicObjectAssembly");
            AssemblyBuilder ab = AppDomain.CurrentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.RunAndSave);

            ModuleBuilder mb = ab.DefineDynamicModule(aName.Name);
            TypeBuilder tb = mb.DefineType("dynamicObject", TypeAttributes.Public);

            Type objType = Type.GetType("System.Object");
            ConstructorInfo objCtor = objType.GetConstructor(new Type[0]);

            ConstructorBuilder ctor = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);

            ILGenerator ctorIL = ctor.GetILGenerator();
            ctorIL.Emit(OpCodes.Ldarg_0);
            ctorIL.Emit(OpCodes.Call, objCtor);
            ctorIL.Emit(OpCodes.Ret);

            FieldBuilder field;
            PropertyBuilder property;
            foreach (var item in dict)
            {

                field = tb.DefineField("_" + item.Key, item.Value.GetType(), FieldAttributes.Private);
                property = tb.DefineProperty(item.Key, PropertyAttributes.HasDefault, item.Value.GetType(), null);

                MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

                MethodBuilder fieldGet = tb.DefineMethod("get_" + item.Key, getSetAttr, item.Value.GetType(), Type.EmptyTypes);

                ILGenerator fieldGetIL = fieldGet.GetILGenerator();
                fieldGetIL.Emit(OpCodes.Ldarg_0);
                fieldGetIL.Emit(OpCodes.Ldfld, field);
                fieldGetIL.Emit(OpCodes.Ret);

                MethodBuilder fieldSet = tb.DefineMethod("set_" + item.Key, getSetAttr, null, new Type[] { item.Value.GetType() });

                ILGenerator fieldSetIL = fieldSet.GetILGenerator();
                fieldSetIL.Emit(OpCodes.Ldarg_0);
                fieldSetIL.Emit(OpCodes.Ldarg_1);
                fieldSetIL.Emit(OpCodes.Stfld, field);
                fieldSetIL.Emit(OpCodes.Ret);

                property.SetSetMethod(fieldSet);
                property.SetGetMethod(fieldGet);

            }
            Type t = tb.CreateType();

            ab.Save(aName.Name + ".dll");

            dynamic instance = Activator.CreateInstance(t);

            foreach (var item in dict)
            {
                PropertyInfo prop = t.GetProperty(item.Key);
                prop.SetValue(instance, item.Value, null);
            }

            return instance;
        }
    }
}
