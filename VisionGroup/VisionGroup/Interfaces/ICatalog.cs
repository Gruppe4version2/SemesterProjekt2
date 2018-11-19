namespace VisionGroup.Interfaces
{
    public interface ICatalog<T>
    {
        void Add(T add);

        void Remove(T remove);

        void Update(T update);

        void Load(T load);
    }
}