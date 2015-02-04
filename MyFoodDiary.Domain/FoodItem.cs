using System;

namespace MyFoodDiary.Domain
{
    public class FoodItem
    {
        public FoodItem()
        { }

        public FoodItem(int id, string code, string name, int quantity, DateTime date)
        {
            Id = id;
            FoodCode = code;
            Name = name;
            Quantity = quantity;
            Date = date;
        }

        public int Id { get; set; }
        public string FoodCode { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public bool ValuesArePerItem { get; set; }


        public override bool Equals(Object obj)
        {
            FoodItem other = obj as FoodItem;

            if (other == null)
                return false;

            return (this.Id == other.Id) &&
                   (this.FoodCode == other.FoodCode) &&
                   (this.Name == other.Name) &&
                   (this.Quantity == other.Quantity) &&
                   (this.Date == other.Date) &&
                   (this.ValuesArePerItem == other.ValuesArePerItem);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + Id.GetHashCode();

                if (FoodCode != null)
                    hash = hash * 23 + FoodCode.GetHashCode();

                if (Name != null)
                    hash = hash * 23 + Name.GetHashCode();

                hash = hash * 23 + Quantity.GetHashCode();
                hash = hash * 23 + Date.GetHashCode();
                hash = hash * 23 + ValuesArePerItem.GetHashCode();

                return hash;
            }
        }

    }
}
