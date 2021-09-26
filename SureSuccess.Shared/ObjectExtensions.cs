using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SureSuccess.Shared
{
    public static class ObjectExtensions
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                var field = type.GetField(name);
                if (field != null)
                {
                    var attr =
                        Attribute.GetCustomAttribute(field,
                            typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                    else
                    {
                        return value.ToString();
                    }
                }
            }
            return null;
        }
        public static TResult MapTo<TResult, TEntity>(this TEntity vm, TResult entity)
        {
            var type = vm.GetType();
            var properties = type.GetProperties();

            foreach (var prop in properties)
            {

                var entProp = entity.GetType().GetProperty(prop.Name);
                if (entProp != null)
                {
                    entProp.SetValue(entity, prop.GetValue(vm), null);
                }

            }

            return entity;

        }

        public static List<TResult> MapTo<TResult, TEntity>(this List<TEntity> vmsEntities, List<TResult> entities) where TResult : new()
        {
            var properties = typeof(TEntity).GetProperties();

            foreach (var entity1 in vmsEntities)
            {
                var obj = new TResult();
                foreach (var prop in properties)
                {

                    var entProp = obj.GetType().GetProperty(prop.Name);
                    if (entProp != null)
                    {
                        entProp.SetValue(obj, prop.GetValue(entity1), null);
                    }

                }

                entities.Add(obj);
            }


            return entities;

        }
    }
}
