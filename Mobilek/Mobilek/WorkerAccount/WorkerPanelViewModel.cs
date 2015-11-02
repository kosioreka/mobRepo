using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobilek.Helper_Classes;
using System.Windows.Input;

namespace Mobilek
{
    class WorkerPanelViewModel : ObservableObject, IPageViewModel
    {
        private string _searchStreet;
        private string _searchStreetNo;
        private string _searchCity;
        private string _searchZipCode;

        private ICommand _getStations;

        ObservableCollection<Station> _stationsCollection = new ObservableCollection<Station>();

        public ObservableCollection<Station> StationsCollection
        {
            get
            {
                using (var entities = new MobilekEntities())
                {
                    var stations = entities.Stations.AsEnumerable();

                    if(!string.IsNullOrWhiteSpace(_searchStreet))
                    {
                        stations = stations.Where(s => s.street.StartsWith(_searchStreet));
                    }
                    if (!string.IsNullOrWhiteSpace(_searchStreetNo))
                    {
                        int no;
                        if(Int32.TryParse(_searchStreetNo,out no))
                        {
                            stations = stations.Where(s => s.streetNumer == no);
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(_searchCity))
                    {
                        stations = stations.Where(s => s.city.StartsWith(_searchCity));
                    }
                    if (!string.IsNullOrWhiteSpace(_searchZipCode))
                    {
                        stations = stations.Where(s => s.zipCode.StartsWith(_searchZipCode));
                    }
                    

                    _stationsCollection = ToObservableCollectioncs.ToObservableCollection<Station>(stations);
                }


                    return _stationsCollection;
            }
            set
            {
                _stationsCollection = value;
                OnPropertyChanged("StationsCollection");
            }
        }

        public ICommand GetStationsCommand
        {
            get
            {
                if (_getStations == null)
                {
                    _getStations = new RelayCommand(
                        param => GetStations()
                    );
                }
                return _getStations;
            }
        }

        private void GetStations()
        {

            using (var entities = new MobilekEntities())
            {
                var stations = entities.Stations.AsEnumerable();

                if (!string.IsNullOrWhiteSpace(_searchStreet))
                {
                    stations = stations.Where(s => s.street.StartsWith(_searchStreet));
                }
                if (!string.IsNullOrWhiteSpace(_searchStreetNo))
                {
                    int no;
                    if (Int32.TryParse(_searchStreetNo, out no))
                    {
                        stations = stations.Where(s => s.streetNumer == no);
                    }
                }
                if (!string.IsNullOrWhiteSpace(_searchCity))
                {
                    stations = stations.Where(s => s.city.StartsWith(_searchCity));
                }
                if (!string.IsNullOrWhiteSpace(_searchZipCode))
                {
                    stations = stations.Where(s => s.zipCode.StartsWith(_searchZipCode));
                }


                StationsCollection = ToObservableCollectioncs.ToObservableCollection<Station>(stations);
            }



        }

        public string SearchStreet
        {
            get { return _searchStreet; }
            set
            {
                _searchStreet = value;
                OnPropertyChanged("SearchStreet");
            }
        }
        public string SearchStreetNo
        {
            get { return _searchStreetNo; }
            set
            {
                _searchStreetNo = value;
                OnPropertyChanged("SearchStreetNo");
            }
        }
        public string SearchCity
        {
            get { return _searchCity; }
            set
            {
                _searchCity = value;
                OnPropertyChanged("SearchCity");
            }
        }
        public string SearchZipCode
        {
            get { return _searchZipCode; }
            set
            {
                _searchZipCode = value;
                OnPropertyChanged("SearchZipCode");
            }
        }

        public string Name
        {
            get
            {
                return "WorkerPanel";
            }
        }
    }
}
