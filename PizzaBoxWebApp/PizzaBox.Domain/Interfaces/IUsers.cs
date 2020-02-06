using PizzaBox.Domain.Models;



namespace PizzaBox.Domain.Interfaces
{
    public interface IUsers : IRepository<Users>
    {
        public void SignIn(string email, string password, ref Users u);
    }
}
