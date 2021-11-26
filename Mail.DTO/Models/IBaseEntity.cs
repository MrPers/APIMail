namespace Mail.Contracts
{
    public interface IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}
