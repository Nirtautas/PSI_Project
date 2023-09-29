namespace TeamWebApplication.Data
{
    public struct Relation<T>
    {
        public T value1;
        public T value2;

        public Relation(T value1, T value2)
        {
            this.value1 = value1;
            this.value2 = value2;
        }

        public void Swap()
        {
            T temp = value1;
            value1 = value2;
            value2 = temp;
        }

        public bool IsSelf()
        {
            return value1.Equals(value2);
        }

        public override string ToString()
        {
            return value1 + ";" + value2;
        }
    }
}
