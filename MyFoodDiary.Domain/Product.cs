using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFoodDiary.Domain
{
    public class Product
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Dictionary<string, double> Nutrients { get; set; }

        public override bool Equals(Object obj)
        {
            Product other = obj as Product;

            if (other == null)
                return false;

            return (this.Code == other.Code) &&
                   (this.Name == other.Name) &&
                   (this.Nutrients.SequenceEqual(other.Nutrients));
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + Code.GetHashCode();

                if (Name != null)
                    hash = hash * 23 + Name.GetHashCode();

                if (Nutrients != null)
                    hash = hash * 23 + Nutrients.GetHashCode();

                return hash;
            }
        }
    }
}
