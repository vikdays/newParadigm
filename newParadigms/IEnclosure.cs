namespace newParadigms
{
    public interface IEnclosure
    {
        string Name { get; }
        int AnimalCount { get; }
        List<Animal> Animals { get; }
        List<Animal> OpenPartOfEnclosure { get; }
        List<Animal> ClosePartOfEnclosure { get; }
        int FoodSupply { get; }

        void FeedAnimals();
        void AddAnimal(Animal animal);
        void ClearAnimals();
        void MoveAnimal(Animal animal);
        void RemoveAnimal(Animal animal);
        void ReplenishFood(int amount);

        bool HasSpace();
        bool IsFoodDepleted();

        string CheckStatus();
        string CheckOpenPartStatus();
    }
}
