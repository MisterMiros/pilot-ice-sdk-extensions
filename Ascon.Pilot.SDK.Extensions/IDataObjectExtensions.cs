using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK.Extensions.Exceptions;
using System.Xml;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class IDataObjectExtensions
    {
        public static ITaskObject ToTask(this IDataObject dataObject)
        {
            if (dataObject.Type.Name == SystemTypeNames.TASK)
            {
                return Extensions.Repository.Get<ITaskObject>(dataObject.Id);
            }
            throw new InvalidCastException("Невозможно преобразовать объект в задачу");
        }

        public static object GetAttributeValue(this IDataObject dataObject, string name)
        {
            IAttribute attribute = dataObject.Type.GetAttribute(name);
            object value;
            if (!dataObject.Attributes.TryGetValue(name, out value) && attribute.IsObligatory)
            {
                throw new AttributeValueException(
                    $"У объекта не задан обязательный атрибут \"{name}\"", dataObject);
            }
            return value;
        }

        public static IEnumerable<IDataObject> GetChildren(this IDataObject dataObject)
        {
            return Extensions.Repository.Get<IDataObject>(dataObject.Children);
        }

        public static IDataObject GetParent(this IDataObject dataObject)
        {
            return Extensions.Repository.Get<IDataObject>(dataObject.ParentId);
        }
    }
}