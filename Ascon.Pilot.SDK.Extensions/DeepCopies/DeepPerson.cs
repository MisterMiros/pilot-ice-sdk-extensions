using System;
using System.Collections.ObjectModel;
using Ascon.Pilot.SDK;
using System.Linq;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepPerson : DeepCopy<IPerson>, IPerson
    {
        private DeepPerson(IPerson original) : base(original) { }

        [DeepCopyCreator(typeof(IPerson))]
        public static IPerson CreateCopy(IPerson original)
        {
            return IsCopy(original) ? original : new DeepPerson(original);
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
                _actualName = value == null ? null : string.Copy(value);
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
                _displayName = value == null ? null : string.Copy(value);
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
                _mainPosition = value.Copy();
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
                _login = value == null ? null : string.Copy(value);
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
                _comment = value == null ? null : string.Copy(value);
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
                _positions = value == null ? null : new ReadOnlyCollection<IPosition>(value.Select(pos => pos.Copy()).ToArray());
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
                _sid = value == null ? null : string.Copy(value);
            }
        }
    }

    public static class IPersonDeepCopyExtension
    {
        public static IPerson Copy(this IPerson original)
        {
            return DeepPerson.CreateCopy(original);
        }
    }
}
