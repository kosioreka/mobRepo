using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mobilek
{
    class UserLoginViewModel : ObservableObject, IPageViewModel, IDataErrorInfo
    {

        #region private Fields 
        
        private ICommand _toCustomerAccount;
        private ICommand _toCustomerLogin;
        private string login_userName;
        private string login_password;

        private string firstName;
        private string surname;
        private string pesel;
        private string creditCardNr;
        private string userName;
        private string phoneNr;
        private string email;
        private string password;
        private string repeatPassword;
        #endregion

        #region properties
        public string Name
        {
            get { return "Log in my friend"; }
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
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
                OnPropertyChanged("isRegistrationValid");
            }
        }
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
                OnPropertyChanged("isRegistrationValid");
            }
        }
        public string Pesel
        {
            get { return pesel; }
            set
            {
                pesel = value;
                OnPropertyChanged("Pesel");
                OnPropertyChanged("isRegistrationValid");
            }
        }
        public string CreditCardNr
        {
            get { return creditCardNr; }
            set
            {
                creditCardNr = value;
                OnPropertyChanged("CreditCardNr");
                OnPropertyChanged("isRegistrationValid");
            }
        }
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
                OnPropertyChanged("isRegistrationValid");
            }
        }
        public string PhoneNr
        {
            get { return phoneNr; }
            set
            {
                phoneNr = value;
                OnPropertyChanged("isRegistrationValid");
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
                OnPropertyChanged("isRegistrationValid");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
                OnPropertyChanged("isRegistrationValid");
            }
        }
        public string RepeatPassword
        {
            get { return repeatPassword; }
            set
            {
                repeatPassword = value;
                OnPropertyChanged("RepeatPassword");
                OnPropertyChanged("isRegistrationValid");
            }
        }
        #endregion

        #region Validation
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

        static readonly string[] ValidatedPropertiesRegistration = 
        {
            "FirstName", "Surname", "Pesel", "CreditCardNr", "UserName", "Password", "RepeatPassword", "PhoneNr", "Email"                                                       
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
        public bool isRegistrationValid
        {
            get
            {
                foreach (string properity in ValidatedPropertiesRegistration)
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
                    break;
                case "Login_Password":
                    if (String.IsNullOrEmpty(Login_UserName))
                    {
                        return "Please enter password!";
                    }
                    break;
                case "FirstName":
                    if (String.IsNullOrWhiteSpace(FirstName))
                    {
                        return "Spaces are not allowed in first name";
                    }
                    else if (String.IsNullOrEmpty(FirstName))
                    {
                        return "First name is required!";
                    }
                    break;
                case "Surname":
                    if (String.IsNullOrEmpty(Surname))
                    {
                        return "Surname is required!";
                    }
                    break;
                case "Pesel":
                    if (String.IsNullOrEmpty(Pesel))
                    {
                        return "Pesel is required!";
                    }
                    else if (Regex.IsMatch(Pesel, @"^\d+$") == false)
                        return "Only digits are allowed in Pesel field.";
                    break;
                case "CreditCardNr":
                    if (String.IsNullOrEmpty(CreditCardNr))
                    {
                        return "Credit Card Number is required!";
                    }
                    break;
                case "UserName":
                    if (String.IsNullOrEmpty(UserName))
                    {
                        return "User name is required!";
                    }
                    break;
                case "PhoneNr":
                    if (String.IsNullOrEmpty(PhoneNr))
                    {
                        return "Phone is required!";
                    }
                    else if (Regex.IsMatch(PhoneNr, @"^\d+$") == false)
                        return "Only digits are allowed in Phone Number field.";
                    break;
                case "Email":
                    if (String.IsNullOrEmpty(Email))
                    {
                        return "Email is required!";
                    }
                    else if (Regex.IsMatch(Email, @"^[A-Za-z0-9_\-\.]+@(([A-Za-z0-9\-])+\.)+([A-Za-z\-])+$") == false)
                        return "Please enter valid e-mail adress.";
                    break;
                case "Password":
                    if (String.IsNullOrEmpty(Password))
                    {
                        return "Password is required!";
                    }
                    else if (Password.Length < 8)
                        return "Password must have at least 8 characters";
                    break;
                case "RepeatPassword":
                    if (String.IsNullOrEmpty(RepeatPassword))
                    {
                        return "Please repeat your password";
                    }
                    else if (RepeatPassword != Password)
                        return "Passwords don't much";
                    break;
                default:
                    throw new NotImplementedException("Error name not found");
            }

            return null;
        }
        #endregion

        #region Commands
        public ICommand ToCustomerAccount
        {
            get
            {
                if (_toCustomerAccount == null)
                {
                    _toCustomerAccount = new RelayCommand(
                        param => ToAccount()
                    );
                }
                return _toCustomerAccount;
            }
        }
        public ICommand ToCustomerLogin
        {
            get
            {
                if (_toCustomerLogin == null)
                {
                    _toCustomerLogin = new RelayCommand(
                        param => ToCustomer()
                    );
                }
                return _toCustomerLogin;
            }
        }
        
        #endregion

        #region NavigationMethods
        private void ToAccount()
        {
            var msg = new ChangePageHelper() { PageName = new UserOrderViewModel() };
            Messenger.Default.Send<ChangePageHelper>(msg);

            //var msg2 = new ChangePanelHelper() { panelType = PanelType.User };
            //Messenger.Default.Send<ChangePanelHelper>(msg2); 
        }
        private void ToCustomer()
        {
            var msg = new ChangePageHelper() { PageName = new UserLoginViewModel() };
            Messenger.Default.Send<ChangePageHelper>(msg);
        }
        
        #endregion
    }
}
