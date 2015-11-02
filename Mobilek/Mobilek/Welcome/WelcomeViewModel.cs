using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mobilek
{
    class WelcomeViewModel : ObservableObject, IPageViewModel
    {

        private ICommand _toCustomerLogin;
        private ICommand _toWorkerLogin;

        public string Name
        {
            get { return "Welcome my friend"; }
        }

        #region Commands
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
        public ICommand ToWorkerLogin
        {
            get
            {
                if (_toWorkerLogin == null)
                {
                    _toWorkerLogin = new RelayCommand(
                        param => ToWorker()
                    );
                }
                return _toWorkerLogin;
            }
        }

        

        #endregion

        #region Methods
        private void ToCustomer()
        {
            var msg = new ChangePageHelper() { PageName = new UserLoginViewModel() };
            Messenger.Default.Send<ChangePageHelper>(msg);
        }
        private void ToWorker()
        {
            var msg = new ChangePageHelper() { PageName = new WorkerLoginViewModel() };
            Messenger.Default.Send<ChangePageHelper>(msg);
        }
        #endregion
        
    }
}
