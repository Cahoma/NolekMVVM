using NolekWPF.Data.DataServices;
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
using NolekWPF.Model.Dto;
using System.ComponentModel;
using System.Windows.Data;
using NolekWPF.Data.Repositories;

namespace NolekWPF.ViewModels.Component
{
    public class ComponentListViewModel : ViewModelBase, IComponentListViewModel
    {
        public ICollectionView ComponentView { get; private set; }
        public ObservableCollection<ComponentDto> Components { get; }

        private IComponentDataService _componentDataService;
        private IComponentRepository _componentRepository;

        private IEventAggregator _eventAggregator;
        private IErrorDataService _errorDataService;

        public IComponentDetailViewModel ComponentDetailViewModel { get; }

        public Login CurrentUser { get; set; }

        public ComponentListViewModel(IComponentDataService componentDataService,
            IEventAggregator eventAggregator, IErrorDataService errorDataService, IComponentDetailViewModel componentDetailViewModel,
            IComponentRepository componentRepository)
        {
            _componentDataService = componentDataService;
            _componentRepository = componentRepository;
            Components = new ObservableCollection<ComponentDto>();
            //initialize event aggregator
            _eventAggregator = eventAggregator;
            _errorDataService = errorDataService;
            _eventAggregator.GetEvent<AfterComponentCreated>().Subscribe(RefreshList);
            ComponentDetailViewModel = componentDetailViewModel;
            _eventAggregator.GetEvent<AfterUserLogin>().Subscribe(OnLogin);
            //_eventAggregator.GetEvent<OpenComponentListViewEvent>().Subscribe(RefreshList);
        }

        public async Task LoadComponentChoiceAsync()
        {
            var choice = await _componentRepository.GetComponentChoiceAsync();
            SelectedComponentLookup = choice;
        }

        private void OnLogin(Login user)
        {
            CurrentUser = user;
        }
             

        private void FilterCollection()
        {
            if (ComponentView != null)
            {
                ComponentView.Refresh();
            }
        }

        public bool Filter(object obj)
        {
            var data = obj as ComponentDto;

            if (ComponentView != null)
            {
                if (!string.IsNullOrEmpty(_filterString) && ComponentChosen != null)
                {
                    string allcaps = _filterString.ToUpper();

                    if(ComponentChosen.ComponentLookupId == 1) //search for type
                    {
                        return data.ComponentType.Contains(_filterString) || data.ComponentType.Contains(allcaps);
                    }
                    else if (ComponentChosen.ComponentLookupId == 2) //search for order
                    {
                        return data.ComponentOrderNumber.Contains(_filterString) || data.ComponentOrderNumber.Contains(allcaps);
                    }
                    else if (ComponentChosen.ComponentLookupId == 3) //search for serial
                    {
                        return data.ComponentSerialNumber.Contains(_filterString) || data.ComponentSerialNumber.Contains(allcaps);
                    }
                    else if (ComponentChosen.ComponentLookupId == 4) //search for supply
                    {
                        return data.ComponentSupplyNumber.Contains(_filterString) || data.ComponentSupplyNumber.Contains(allcaps);
                    }                  
                }
                return true;
            }
            return false;
        }

        private async void RefreshList()
        {
            await LoadAsync();
        }

        public async Task LoadAsync()
        {
            try
            {
                var lookup = await _componentDataService.GetComponentLookupAsync();
                Components.Clear();
                foreach (var item in lookup)
                {
                    Components.Add(item);
                }
                ComponentView = CollectionViewSource.GetDefaultView(Components);
                ComponentView.Filter = new Predicate<object>(Filter);
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

        private ComponentLookupDto _componentChosen;
        public ComponentLookupDto ComponentChosen
        {
            get { return _componentChosen; }
            set
            {
                _componentChosen = value;
            }
        }

        private IEnumerable<ComponentLookupDto> _selectedComponentLookup;
        public IEnumerable<ComponentLookupDto> SelectedComponentLookup
        {
            get { return _selectedComponentLookup; }
            set
            {
                _selectedComponentLookup = value;
            }
        }

        private ComponentDto _selectedComponent;
        public ComponentDto SelectedComponent
        {
            get { return _selectedComponent; }
            set
            {
                _selectedComponent = value;
                if (_selectedComponent != null)
                {
                    _eventAggregator.GetEvent<OpenComponentDetailViewEvent>()
                        .Publish(_selectedComponent.ComponentId);
                }
            }
        }

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


    }
}
