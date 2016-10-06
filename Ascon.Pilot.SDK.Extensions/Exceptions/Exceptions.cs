using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK;

namespace Ascon.Pilot.SDK.Extensions.Exceptions
{
    class TaskObjectException : Exception
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
            Task = DeepCopies.DeepTaskObject.CreateCopy(task);
        }
    }
    class InitiatorEmptyException : TaskObjectException
    {
        public InitiatorEmptyException(string message, ITaskObject task) : base(message, task) { }
    }
    class ExecutorEmptyException : TaskObjectException
    {
        public ExecutorEmptyException(string message, ITaskObject task) : base(message, task) { }
    }
    class NoAttachmentsException : TaskObjectException
    {
        public NoAttachmentsException(string message, ITaskObject task) : base(message, task) { }
    }
    class NoProjectRelatedAttachmentsException : TaskObjectException
    {
        public NoProjectRelatedAttachmentsException(string message, ITaskObject task) : base(message, task) { }
    }
    class NoProjectCodeInTaskException : TaskObjectException
    {
        public NoProjectCodeInTaskException(string message, ITaskObject task) : base(message, task) { }
    }

    class NoPersonException : Exception
    {
        public NoPersonException(string message) : base(message) { }
    }

    class PersonException : Exception
    {
        public IPerson Person
        {
            get; private set;
        }
        public PersonException(string message, IPerson person) : base(message)
        {
            Data["Person Name"] = person.DisplayName;
            Data["Person Login"] = person.Login;
            Person = DeepCopies.DeepPerson.CreateCopy(person);
        }
    }
    class NoChiefException : PersonException
    {
        public NoChiefException(string message, IPerson person) : base(message, person) { }
    }
    class NoADUserExistsException : PersonException
    {
        public NoADUserExistsException(string message, IPerson person) : base(message, person) { }
    }
    class InvalidEmailException : PersonException
    {
        public InvalidEmailException(string message, IPerson person) : base(message, person)
        {

        }
    }

    class DataObjectException : Exception
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
    class AttributeValueException : DataObjectException
    {
        public AttributeValueException(string message, IDataObject dataObject) : base(message, dataObject) { }
    }

    class TypeException : Exception
    {
        public IType Type
        {
            get; private set;
        }

        public TypeException(string message, IType _type) : base(message)
        {
            Data["Type Title"] = _type.Title;
            Data["Type Name"] = _type.Name;
            Type = DeepCopies.DeepType.CreateCopy(_type);
        }
    }
    class NoAttributeException : TypeException
    {
        public NoAttributeException(string message, IType type) : base(message, type)
        {

        }
    }
    class AttributeConfigurationException: TypeException
    {
        public AttributeConfigurationException(string message, IType type) : base(message, type)
        {

        }
    }
}
