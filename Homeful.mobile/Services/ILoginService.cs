using System;
namespace Homeful.mobile
{
    public interface ILoginService
    {
        void Signup(string email, string password);
        void Login(string email, string password);
        void GoogleLogin();
        void FacebookLogin();
    }
}
