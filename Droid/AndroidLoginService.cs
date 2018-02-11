using System;
using Android.Widget;
using Firebase.Auth;

namespace Homeful.mobile.Droid
{
    public class AndroidLoginService : ILoginService
    {
        public AndroidLoginService()
        {
        }

        public void FacebookLogin()
        {
            throw new NotImplementedException();
        }

        public void GoogleLogin()
        {
            throw new NotImplementedException();
        }

        public void Login(string email, string password)
        {
            try
            {
                FirebaseAuth.Instance.SignInWithEmailAndPassword(email, password);
            }
            catch (Exception ex)
            {
            }
        }

        public void Signup(string email, string password)
        {
            try
            {
                FirebaseAuth.Instance.CreateUserWithEmailAndPassword(email, password);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
