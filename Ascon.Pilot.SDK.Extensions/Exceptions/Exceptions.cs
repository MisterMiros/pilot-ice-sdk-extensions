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

    public class TaskObjectException : Exception
    {
        public ITaskObject Task
        {
            get; private set;
        }
        public TaskObjectException(string message, ITaskObject task) : base(message)
        {
            Data["Task Title"] = task.DisplayTitle;
            Data["Task Description"] = task.Description;
            Data["Task Guid"] = task.Id;
            try { Data["Task Initiator"] = task.Initiator.Login; } catch { }
            try { Data["Task Executor"] = task.Executor.Login; } catch { }
            Task = DeepCopies.DeepCopyFactory.CreateCopy(task);
        }
    }

    public class PersonException : Exception
    {
        public IPerson Person
        {
            get; private set;
        }
        public PersonException(string message, IPerson person) : base(message)
        {
            Data["Person Name"] = person.DisplayName;
            Data["Person Login"] = person.Login;
            Person = DeepCopies.DeepCopyFactory.CreateCopy(person);
        }
    }
}
