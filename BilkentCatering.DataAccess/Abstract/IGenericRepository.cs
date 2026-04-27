namespace BilkentCatering.DataAccess.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        T GetSingle();
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}