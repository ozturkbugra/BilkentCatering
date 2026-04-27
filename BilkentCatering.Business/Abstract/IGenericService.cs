namespace BilkentCatering.Business.Abstract
{
    public interface IGenericService<T> where T : class
    {
        T GetById(int id);
        T GetSingle();
        IEnumerable<T> GetAll();
        ServiceResult Add(T entity);
        ServiceResult Update(T entity);
        ServiceResult Delete(T entity);
    }
}