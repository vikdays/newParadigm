namespace newParadigms
{
    public abstract class Person : BaseEntity
    {
        public string? Name { get; set; } //Геттер позволяет получить значение переменной, а сеттер — установить новое значение.
        protected char Gender { get; set; }
    }
}
