using System;
using System.Collections.ObjectModel;
using Ascon.Pilot.SDK;
using System.Linq;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepPerson : DeepCopy<IPerson>, IPerson
    {
        private DeepPerson(IPerson original) : base(original)
        {
            ActualName = original.ActualName;
            DisplayName = original.DisplayName;
            Id = original.Id;
            MainPosition = original.MainPosition;
            Login = original.Login;
            IsAdmin = original.IsAdmin;
            IsDeleted = original.IsDeleted;
            Positions = original.Positions;
            Sid = original.Sid;
        }

        public static IPerson CreateCopy(IPerson original)
        {
            if (original == null || original is DeepCopy<IPerson>)
            {
                return original;
            }
            return new DeepPerson(original);
        }

        string _actualName;
        public string ActualName
        {
            get
            {
                return _actualName;
            }
            private set
            {
                _actualName = string.Copy(value ?? string.Empty);
            }
        }

        string _displayName;
        public string DisplayName
        {

            get
            {
                return _displayName;
            }
            private set
            {
                _displayName = string.Copy(value ?? string.Empty);
            }

        }

        public int Id
        {
            get; private set;
        }

        IPosition _mainPosition;
        public IPosition MainPosition
        {
            get
            {
                return _mainPosition;
            }
            private set
            {
                _mainPosition = DeepPosition.CreateCopy(value);
            }
        }

        string _login;
        public string Login
        {

            get
            {
                return _login;
            }
            private set
            {
                _login = string.Copy(value ?? string.Empty);
            }

        }

        string _comment;
        public string Comment
        {
            get
            {
                return _comment;
            }
            private set
            {
                _comment = string.Copy(value ?? string.Empty);
            }
        }

        public bool IsAdmin
        {
            get; private set;
        }

        public bool IsDeleted
        {
            get; private set;
        }

        ReadOnlyCollection<IPosition> _positions;
        public ReadOnlyCollection<IPosition> Positions
        {
            get
            {
                return _positions;
            }
            set
            {
                _positions = new ReadOnlyCollection<IPosition>(value.Select(pos => DeepPosition.CreateCopy(pos)).ToArray());
            }
        }

        string _sid;
        public string Sid
        {
            get
            {
                return _sid;
            }
            private set
            {
                _sid = string.Copy(value ?? string.Empty);
            }
        }
    }
}
