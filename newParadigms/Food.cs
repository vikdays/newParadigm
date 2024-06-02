namespace newParadigms
{
    public enum FoodType
    {
        Apple,
        Meat,
        Fish,
        Banana
    }

    public abstract class Food
    {
        public virtual int Value { get; protected set; } = 0;
        public virtual FoodType Type { get; protected set; } = FoodType.Apple;
    }
}
