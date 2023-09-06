using System;

namespace ET
{
    [EnableMethod]
    public class RankRoleInfo: Entity, IAwake, IRankObj
    {
        public string Name { get; }

        public string HeadIcon { get; }

        public int Level { get; }

        public long Fight { get; set; }

        public int Sex { get; set; }

        private bool Equals(RankRoleInfo other)
        {
            return this.Name == other.Name && this.HeadIcon == other.HeadIcon && this.Level == other.Level && this.Fight == other.Fight && this.Sex == other.Sex;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((RankRoleInfo)obj);
        }

        public override int GetHashCode()
        {
            return $"{this.Name}_{this.HeadIcon}_{this.Level}".HashCode();
        }

        public static bool operator ==(RankRoleInfo l, RankRoleInfo r)
        {
            return l.Equals(r);
        }

        public static bool operator !=(RankRoleInfo l, RankRoleInfo r)
        {
            return !l.Equals(r);
        }
    }
}