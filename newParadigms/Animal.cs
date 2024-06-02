namespace newParadigms
{
    public abstract class Animal : BaseEntity
    {
        public Food FirstFoodType { get; protected set; }
        public Food SecondFoodType { get; protected set; }
        public string Species { get; private set; }
        public int HungerLevel { get; set; }
        protected string Voice { get; } //член класса доступен только внутри самого класса и в классах-наследниках, но не доступен вне этих классов.

        protected Animal(string species, int hungerLevel, string voice, Food firstFoodType, Food secondFoodType)
        {
            Species = species;
            HungerLevel = hungerLevel;
            Voice = voice;
            FirstFoodType = firstFoodType;
            SecondFoodType = secondFoodType;
        }
        public void Edit(string newSpecies, int newHungryLevel)
        {
            Species = newSpecies;
            HungerLevel = newHungryLevel;
        }
        public void DecreaseHungerLevel()
        {
            HungerLevel -= 2;
        }
        public abstract void Feed();
        public abstract void MakeSound();
        public abstract void FeedFromEnclosure(IEnclosure enclosure);
    }
}
