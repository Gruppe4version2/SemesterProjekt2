namespace VisionGroup2._0.Interfaces
{
    public interface ICatalog<T>
    {
        void Add(T item);

        void Load();

        void Remove(T item);

        void Update(T item);
    }
}