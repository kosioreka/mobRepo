using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mobilek
{
    class WorkerLoginViewModel: ObservableObject, IPageViewModel, IDataErrorInfo
    {
        private ICommand _toWorkerAccount;
        private string login_userName;
        private string login_password;

        public string Name
        {
            get { return "Log in Admin"; }
        }

        public string Login_UserName
        {
            get { return login_userName; }
            set
            {
                login_userName = value;
                OnPropertyChanged("Login_UserName");
                OnPropertyChanged("isLoginValid");
            }
        }
        public string Login_Password
        {
            get { return login_password; }
            set
            {
                login_password = value;
                OnPropertyChanged("Login_Password");
                OnPropertyChanged("isLoginValid");
            }
        }


        //#region Validation
        string IDataErrorInfo.Error
        {
            get { return null; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return GetValidationError(propertyName);
            }
        }

        static readonly string[] ValidatedPropertiesLogin = {
                                                                "Login_UserName", "Login_Password"
                                                            };


        public bool isLoginValid
        {
            get
            {
                foreach (string properity in ValidatedPropertiesLogin)
                    if (GetValidationError(properity) != null)
                        return false;
                return true;
            }
        }

        string GetValidationError(String propertyName)
        {
            switch (propertyName)
            {
                case "Login_UserName":
                    if (String.IsNullOrWhiteSpace(Login_UserName))
                    {
                        return "Spaces are not allowed in first name";
                    }
                    else if (String.IsNullOrEmpty(Login_UserName))
                    {
                        return "Please enter your user name";
                    }
                    using (var entities = new MobilekEntities())
                    {
                        var user = entities.Workers.FirstOrDefault(x => x.login == Login_UserName);
                        if (user== null)
                        {
                            return "User not found";
                        }
                    }
                        break;
                case "Login_Password":
                    if (String.IsNullOrEmpty(Login_UserName))
                    {
                        return "Please enter password!";
                    }
                    else
                    {
                        using (var entities = new MobilekEntities())
                        {
                            var user = entities.Workers.FirstOrDefault(x => x.login == Login_UserName);
                            if(user== null)
                            {
                                return "User not found";
                            }
                            else
                            {
                                if (user.password != Login_Password)
                                {
                                    return "Incorrect password";
                                }
                            }
                        }
                    }
                    break;
                default:
                    throw new NotImplementedException("Error name not found");
            }

            return null;
        }


        public ICommand ToWorkerAccount
        {
            get
            {
                if (_toWorkerAccount == null)
                {
                    _toWorkerAccount = new RelayCommand(
                        param => ToAccount()
                    );
                }
                return _toWorkerAccount;
            }
        }
        private void ToAccount()
        {
            var msg = new ChangePageHelper() { PageName = new WorkerPanelViewModel() };
            Messenger.Default.Send<ChangePageHelper>(msg);
        }
    }
}
