namespace InsertSortParallel;

public class Test : IComparable<Test>, ICloneable
{
    public float Id { get; set; }
    public string Name { get; set; }

    public int CompareTo(Test other)
    {
        return Id.CompareTo(other.Id) + String.Compare(Name, other.Name, StringComparison.Ordinal);
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}