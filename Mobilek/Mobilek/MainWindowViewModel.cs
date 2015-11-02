using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices;
using GalaSoft.MvvmLight.Messaging;

namespace Mobilek
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Fields

        private ICommand _changePageCommand;
        //private ICommand _changePanelCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;
        private List<IPageViewModel> _customerViewModels;
        private List<IPageViewModel> _shownViewModels;        

        #endregion

        public MainWindowViewModel()
        {
            // Add available pages
            PageViewModels.Add(new WelcomeViewModel());
            PageViewModels.Add(new HomeViewModel());
            PageViewModels.Add(new UserLoginViewModel());
            PageViewModels.Add(new WorkerLoginViewModel());
            PageViewModels.Add(new ProductsViewModel());
            PageViewModels.Add(new UserOrderViewModel());

            ShownViewModels.Add(new WelcomeViewModel());
            CustomerViewModels.Add(new UserOrderViewModel());

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];

            Messenger.Default.Register<ChangePageHelper>
            (
                 this,
                 (action) => ChangeViewModel(action.PageName)
            );
            //Messenger.Default.Register<ChangePanelHelper>
            //(
            //    this,
            //    (action) => ChangePanelViewModel(action.panelType)
            //);


        }

        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

        //public ICommand ChangePanelCommand
        //{
        //    get
        //    {
        //        if (_changePanelCommand == null)
        //        {
        //            _changePanelCommand = new RelayCommand(
        //                p => ChangePanelViewModel((PanelType)p),
        //                p => p is PanelType);
        //        }

        //        return _changePanelCommand;
        //    }
        //}

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }
        public List<IPageViewModel> CustomerViewModels
        {
            get
            {
                if (_customerViewModels == null)
                    _customerViewModels = new List<IPageViewModel>();

                return _customerViewModels;
            }
        }
        public List<IPageViewModel> ShownViewModels
        {
            get
            {
                if (_shownViewModels == null)
                    _shownViewModels = new List<IPageViewModel>();

                return _shownViewModels;
            }
        }
        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        //public List<IPageViewModel> CurrentPanelViewModel
        //{
        //    get
        //    {
        //        return _shownViewModels;
        //    }
        //    set
        //    {
        //        if (_shownViewModels != value)
        //        {
        //            _shownViewModels = value;
        //            OnPropertyChanged("ShownPanelViewModel");
        //        }
        //    }
        //}

        #endregion

        #region Methods

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }
        //private void ChangePanelViewModel(PanelType viewModel)
        //{
        //    if (viewModel == PanelType.User)
        //    {
        //        CurrentPanelViewModel = CustomerViewModels;
        //    }
        //}

        #endregion
    }
}
