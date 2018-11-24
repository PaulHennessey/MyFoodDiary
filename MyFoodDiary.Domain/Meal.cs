using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFoodDiary.Domain
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public override bool Equals(Object obj)
        {
            FoodItem other = obj as FoodItem;

            if (other == null)
                return false;

            return (this.Id == other.Id) &&
                   (this.Name == other.Name);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + Id.GetHashCode();

                if (Name != null)
                    hash = hash * 23 + Name.GetHashCode();

                return hash;
            }
        }
    }
}
