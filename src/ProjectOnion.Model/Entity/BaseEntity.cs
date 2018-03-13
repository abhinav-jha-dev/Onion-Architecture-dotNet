using ProjectOnion.Model.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace ProjectOnion.Model.Entity
{
    public class BaseEntity : Model, IEntity
    {
        private static readonly Random RandomGenerator;
        private Guid? _id;

        static BaseEntity()
        {
            RandomGenerator = new Random();
        }

        public Guid Id
        {
            get
            {
                if (_id != null) return _id.Value;

                var date = BitConverter.GetBytes(DateTime.Now.ToBinary());
                Array.Reverse(date);

                var random = new byte[8];
                RandomGenerator.NextBytes(random);

                var guid = new byte[16];
                Buffer.BlockCopy(date, 0, guid, 0, 8);
                Buffer.BlockCopy(random, 0, guid, 8, 8);

                _id = new Guid(guid);
                return _id.Value;
            }

            set
            {
                if (_id == null)
                {
                    _id = value;
                }
            }
        }

        public DateTime GetCreationDate()
        {
            return Decode(Id);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            foreach (var propertyInfo in GetType().GetRuntimeProperties().Where(x => x.Name != "Revision"))
            {
                var mine = propertyInfo.GetValue(this, null);
                var theirs = propertyInfo.GetValue(obj, null);
                if (mine != null)
                {
                    //TODO: copy algo
                }
                else if (theirs != null)
                {
                    return false;
                }
            }

            return true;
        }

        protected bool Equals(BaseEntity other)
        {
            return _id.Equals(other._id);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }


        private static DateTime Decode(Guid item)
        {
            var date = new byte[8];
            Buffer.BlockCopy(item.ToByteArray(), 0, date, 0, 8);
            Array.Reverse(date);

            try
            {
                return DateTime.FromBinary(BitConverter.ToInt64(date, 0));
            }
            catch
            {
                // ignored
            }

            return DateTime.MinValue;
        }
    }
}
