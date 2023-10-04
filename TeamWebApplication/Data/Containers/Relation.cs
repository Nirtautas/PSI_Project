namespace TeamWebApplication.Data
{
    public struct Relation<T>
    {
        public T Value1 { get; set; }
        public T Value2 { get; set; }

        public Relation(T value1, T value2)
        {
            this.Value1 = value1;
            this.Value2 = value2;
        }

        public void Swap()
        {
            T temp = Value1;
            Value1 = Value2;
            Value2 = temp;
        }

        public bool IsSelf()
        {
            return Value1.Equals(Value2);
        }

        public override string ToString()
        {
            return Value1 + ";" + Value2;
        }
    }
}
