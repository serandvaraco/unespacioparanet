using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace FacRepositoriesContainer.Models
{
    public static class DapperExtension
    {
        public static dynamic DictionaryToObject(IDictionary<string, object> dict)
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
            object objNull = null;
            foreach (var item in dict)
            {
                if (item.Value == null)
                    objNull = DBNull.Value;

                field = tb.DefineField("_" + item.Key, item.Value == null ? objNull.GetType() : item.Value.GetType(), FieldAttributes.Private);
                property = tb.DefineProperty(item.Key, PropertyAttributes.HasDefault, item.Value == null ? objNull.GetType() : item.Value.GetType(), null);

                MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

                MethodBuilder fieldGet = tb.DefineMethod("get_" + item.Key, getSetAttr, item.Value == null ? objNull.GetType() : item.Value.GetType(), Type.EmptyTypes);

                ILGenerator fieldGetIL = fieldGet.GetILGenerator();
                fieldGetIL.Emit(OpCodes.Ldarg_0);
                fieldGetIL.Emit(OpCodes.Ldfld, field);
                fieldGetIL.Emit(OpCodes.Ret);

                MethodBuilder fieldSet = tb.DefineMethod("set_" + item.Key, getSetAttr, null, new Type[] { item.Value == null ? objNull.GetType() : item.Value.GetType() });

                ILGenerator fieldSetIL = fieldSet.GetILGenerator();
                fieldSetIL.Emit(OpCodes.Ldarg_0);
                fieldSetIL.Emit(OpCodes.Ldarg_1);
                fieldSetIL.Emit(OpCodes.Stfld, field);
                fieldSetIL.Emit(OpCodes.Ret);

                property.SetSetMethod(fieldSet);
                property.SetGetMethod(fieldGet);

            }
            Type t = tb.CreateType();

            dynamic instance = Activator.CreateInstance(t);

            foreach (var item in dict)
            {
                if (item.Value == null)
                    objNull = DBNull.Value;

                PropertyInfo prop = t.GetProperty(item.Key);

                //find the property type
                Type propertyType = prop.PropertyType;
                //Convert.ChangeType does not handle conversion to nullable types
                //if the property type is nullable, we need to get the underlying type of the property
                var targetType = IsNullableType(prop.PropertyType) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType;

                var valueType = item.Value == null ? objNull : item.Value;
                //Returns an System.Object with the specified System.Type and whose value is
                //equivalent to the specified object.
                valueType = Convert.ChangeType(valueType, targetType);


                prop.SetValue(instance, valueType, null);
            }


            return instance;
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }
    }
}