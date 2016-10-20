using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK;

namespace Ascon.Pilot.SDK.Extensions
{
    public class NoPersonException : Exception
    {
        public NoPersonException(string message) : base(message) { }
    }

    public class DataObjectException : Exception
    {
        public IDataObject DataObject
        {
            get; private set;
        }
        public DataObjectException(string message, IDataObject dataObject) : base(message)
        {
            Data["Data Object Title"] = dataObject.DisplayName;
            Data["Data Object Type"] = dataObject.Type.Name;
            Data["Data Object Guid"] = dataObject.Id;
            DataObject = DeepCopies.DeepDataObject.CreateCopy(dataObject);
        }
    }
    public class AttributeValueException : DataObjectException
    {
        public AttributeValueException(string message, IDataObject dataObject) : base(message, dataObject) { }
    }

    public class TypeException : Exception
    {
        public IType Type
        {
            get; private set;
        }

        public TypeException(string message, IType type) : base(message)
        {
            Data["Type Title"] = type.Title;
            Data["Type Name"] = type.Name;
            Type = DeepCopies.DeepType.CreateCopy(type);
        }
    }
    public class NoAttributeException : TypeException
    {
        public NoAttributeException(string message, IType type) : base(message, type)
        {

        }
    }
    public class AttributeConfigurationException: TypeException
    {
        public AttributeConfigurationException(string message, IType type) : base(message, type)
        {

        }
    }
}
