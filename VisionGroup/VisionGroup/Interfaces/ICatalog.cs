namespace VisionGroup.Interfaces
{
    public interface ICatalog<T>
    {
        void Add(T item);

        void Remove(T item);

        void Update(T item);

        void Load(T item);

    }
}