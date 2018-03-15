using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.Model;
using System.Collections.ObjectModel;
using Prism.Events;
using NolekWPF.Events;
using System.Windows;
using NolekWPF.ViewModels;
using NolekWPF.Data.DataServices;
using NolekWPF.Helpers;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Windows.Data;
using NolekWPF.Model.Dto;
using NolekWPF.Data.Repositories;

namespace NolekWPF.Equipment.ViewModels
{
    public class EquipmentListViewModel : ViewModelBase, IEquipmentListViewModel
    {
        private IEquipmentLookupDataService _equipmentLookupDataService;
        private IEquipmentRepository _equipmentRepository;

        public ObservableCollection<EquipmentLookup> Equipments { get; }
        
        private IEventAggregator _eventAggregator;
        private IErrorDataService _errorDataService;
        public IEquipmentDetailViewModel EquipmentDetailViewModel { get; }
        ConvertTextToImage cv = new ConvertTextToImage();

        public EquipmentListViewModel(IEquipmentLookupDataService equipmentLookupDataService,
            IEventAggregator eventAggregator, IErrorDataService errorDataService, IEquipmentDetailViewModel equipmentDetailViewModel,
            IEquipmentRepository equipmentRepository)
        {
            _equipmentLookupDataService = equipmentLookupDataService;
            Equipments = new ObservableCollection<EquipmentLookup>();
            //initialize event aggregator
            _eventAggregator = eventAggregator;
            _errorDataService = errorDataService;
            _equipmentRepository = equipmentRepository;
            _eventAggregator.GetEvent<AfterEquipmentCreated>().Subscribe(RefreshList);
            _eventAggregator.GetEvent<AfterUserLogin>().Subscribe(OnLogin);
            EquipmentDetailViewModel = equipmentDetailViewModel;
            LoadDetailData();            
        }

        public async Task LoadEquipmentChoiceAsync()
        {
            var choice = await _equipmentRepository.GetEquipmentChoiceAsync();
            SelectedEquipmentLookup = choice;
        }

        private EquipmentSearchDto _equipmentChosen;
        public EquipmentSearchDto EquipmentChosen
        {
            get { return _equipmentChosen; }
            set
            {
                _equipmentChosen = value;
            }
        }

        private IEnumerable<EquipmentSearchDto> _selectedEquipmentLookup;
        public IEnumerable<EquipmentSearchDto> SelectedEquipmentLookup
        {
            get { return _selectedEquipmentLookup; }
            set
            {
                _selectedEquipmentLookup = value;
            }
        }

        public ICollectionView EquipmentView { get; private set; }

        private string _filterString;
        public string FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                FilterCollection();
            }
        }

        private void FilterCollection()
        {
            if (EquipmentView != null)
            {
                EquipmentView.Refresh();
            }
        }

        public bool Filter(object obj)
        {
            var data = obj as EquipmentLookup;
            
            if (EquipmentView != null)
            {
                if (!string.IsNullOrEmpty(_filterString) && EquipmentChosen != null)
                {
                    string allcaps = _filterString.ToUpper();

                    //if (EquipmentChosen.EquipmentSearchId == 1) //search for id
                    //{
                    //    return data.EquipmentId.Equals(_filterString);
                    //}
                    //else if (EquipmentChosen.EquipmentSearchId == 2) //search for date created
                    //{
                    //    return data.DateCreated.Contains(_filterString) || data.ComponentOrderNumber.Contains(allcaps);
                    //}
                    if (EquipmentChosen.EquipmentSearchId == 3) //search for serial
                    {
                        return data.SerialNumber.Contains(_filterString) || data.SerialNumber.Contains(allcaps);
                    }
                    else if (EquipmentChosen.EquipmentSearchId == 4) //search for mainequipmentnumber
                    {
                        return data.MainEquipmentNumber.Contains(_filterString) || data.MainEquipmentNumber.Contains(allcaps);
                    }
                    else if (EquipmentChosen.EquipmentSearchId == 5) //search for type
                    {
                        return data.TypeName.Contains(_filterString) || data.TypeName.Contains(allcaps);
                    }
                    else if (EquipmentChosen.EquipmentSearchId == 6) //search for category
                    {
                        return data.Category.Contains(_filterString) || data.Category.Contains(allcaps);
                    }
                    else if (EquipmentChosen.EquipmentSearchId == 7) //search for mainequipmentnumber
                    {
                        return data.Configuration.Contains(_filterString) || data.Configuration.Contains(allcaps);
                    }
                }
                return true;
            }
            return false;
        }


        private async void LoadDetailData()
        {
            await EquipmentDetailViewModel.LoadCategoriesAsync();
            await EquipmentDetailViewModel.LoadConfigurationsAsync();
            await EquipmentDetailViewModel.LoadTypesAsync();
        }


        private async void RefreshList()
        {
            await LoadAsync();
        }

        private void OnLogin(Login user)
        {
            CurrentUser = user;
        }

        public Login CurrentUser { get; set; }

        public async Task LoadAsync()
        {
            try
            {
                var lookup = await _equipmentLookupDataService.GetEquipmentLookupAsync();
                int i = 0;
                Equipments.Clear();
                foreach (var item in lookup)
                {
                    Equipments.Add(item);
                    //var convert = cv.Convert(Equipments[i].ImagePath);
                    i++;
                }
                EquipmentView = CollectionViewSource.GetDefaultView(Equipments);
                EquipmentView.Filter = new Predicate<object>(Filter);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "An error occurred", MessageBoxButton.OK, MessageBoxImage.Warning);
                //create new error object from the exception and add to DB
                Error error = new Error
                {
                    ErrorMessage = e.Message,
                    ErrorTimeStamp = DateTime.Now,
                    ErrorStackTrace = e.StackTrace,
                    LoginId = CurrentUser.LoginId
                };
                await _errorDataService.AddError(error);
            }
        }

        private EquipmentLookup _selectedEquipment;

        public EquipmentLookup SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                _selectedEquipment = value;
                if (_selectedEquipment != null && CurrentUser.RoleId == 3)
                {
                    _eventAggregator.GetEvent<OpenEquipmentDetailViewEvent>()
                        .Publish(_selectedEquipment.EquipmentId);
                }
            }
        }
    }
}
