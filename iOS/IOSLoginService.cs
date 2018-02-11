using System;
using Firebase.Auth;

namespace Homeful.mobile.iOS
{
    public class IOSLoginService : ILoginService
    {
        public IOSLoginService()
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
            Auth.DefaultInstance.SignIn(email, password, (user, error) => {
                if (error != null)
                {
                    AuthErrorCode errorCode;
                    if (IntPtr.Size == 8) // 64 bits devices
                        errorCode = (AuthErrorCode)((long)error.Code);
                    else // 32 bits devices
                        errorCode = (AuthErrorCode)((int)error.Code);

                    // Posible error codes that SignIn method with email and password could throw
                    // Visit https://firebase.google.com/docs/auth/ios/errors for more information
                    switch (errorCode)
                    {
                        case AuthErrorCode.OperationNotAllowed:
                        case AuthErrorCode.InvalidEmail:
                        case AuthErrorCode.UserDisabled:
                        case AuthErrorCode.WrongPassword:
                        default:
                            // Print error
                            break;
                    }
                }
                else
                {
                    
                }
            });
        }

        public void Signup(string email, string password)
        {
            Auth.DefaultInstance.CreateUser(email, password, (user, error) => {
                if (error != null)
                {
                    AuthErrorCode errorCode;
                    if (IntPtr.Size == 8) // 64 bits devices
                        errorCode = (AuthErrorCode)((long)error.Code);
                    else // 32 bits devices
                        errorCode = (AuthErrorCode)((int)error.Code);

                    // Posible error codes that CreateUser method could throw
                    switch (errorCode)
                    {
                        case AuthErrorCode.InvalidEmail:
                        case AuthErrorCode.EmailAlreadyInUse:
                        case AuthErrorCode.OperationNotAllowed:
                        case AuthErrorCode.WeakPassword:
                        default:
                            // Print error
                            break;
                    }
                }
                else
                {
                    // Do your magic to handle authentication result
                }
            });
        }
    }
}
