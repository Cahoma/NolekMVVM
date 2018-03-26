using NolekWPF.Data.DataServices;
using NolekWPF.Data.Repositories;
using NolekWPF.Events;
using NolekWPF.Model;
using NolekWPF.Model.Dto;
using NolekWPF.Wrappers;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace NolekWPF.ViewModels.Component
{
    public class ComponentGeneralViewModel : ViewModelBase, IComponentGeneralViewModel
    {
        public ICollectionView ComponentView { get; private set; }
        public ObservableCollection<ComponentDto> Components { get; }

        private IComponentDataService _componentDataService;
        private IComponentRepository _componentRepository;

        private IEventAggregator _eventAggregator;
        private IErrorDataService _errorDataService;

        private bool _hasChanges;

        public Login CurrentUser { get; set; }

        public IComponentListViewModel ComponentListViewModel { get; }

        public ICommand CreateComponentCommand { get; }
        public ICommand UpdateComponentCommand { get; }
        public ICommand FullListCommand { get; }

        public ComponentGeneralViewModel(IComponentDataService componentDataService,
            IEventAggregator eventAggregator, IErrorDataService errorDataService, IComponentDetailViewModel componentDetailViewModel,
            IComponentRepository componentRepository, IComponentListViewModel componentListViewModel)
        {
            CreateComponentCommand = new DelegateCommand(OnCreateComponentExecute, OnComponentCreateCanExecute);
            UpdateComponentCommand = new DelegateCommand(OnUpdateExecute);
            FullListCommand = new DelegateCommand(OnFullListExecute);

            _componentDataService = componentDataService;
            _componentRepository = componentRepository;
            ComponentListViewModel = componentListViewModel;
            Components = new ObservableCollection<ComponentDto>();
            //initialize event aggregator
            _eventAggregator = eventAggregator;
            _errorDataService = errorDataService;
            _eventAggregator.GetEvent<AfterComponentCreated>().Subscribe(RefreshList);

            _eventAggregator.GetEvent<AfterUserLogin>().Subscribe(OnLogin);
            //_eventAggregator.GetEvent<OpenComponentListViewEvent>().Subscribe(RefreshList);

            Component = CreateNewComponent();
        }

        public void OnFullListExecute()
        {
            _eventAggregator.GetEvent<OpenFullListEvent>().Publish();
        }

        public async void OnUpdateExecute()
        {
            try
            {
                await _componentRepository.SaveAsync();
                Component = UpdateComponent();
                RefreshList();
                Component = null;
                //_eventAggregator.GetEvent<OpenComponentListViewEvent>().Publish();
            }
            catch (Exception e)
            {
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

        public ComponentWrapper UpdateComponent()
        {
            var component = new ComponentWrapper(new Model.Component());

            //_eventAggregator.GetEvent<AfterComponentCreated>().Publish();
            return component;
        }

        private bool OnComponentCreateCanExecute()
        {
            //validate fields to disable/enable buton
            return Component != null && !Component.HasErrors;
        }

        private async void OnCreateComponentExecute()
        {
            try
            {
                await _componentRepository.SaveAsync();
                Component = CreateNewComponent();
                MessageBox.Show("Component was successfully created.");
                RefreshList();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show(validationError.PropertyName + validationError.ErrorMessage + dbEx.Message);
                    }
                }
            }
        }

        public bool HasChanges //is true if changes has been made to equipment
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                }
            }
        }

        private ComponentWrapper CreateNewComponent() //calls the add method in the repository to insert new equipment and return it
        {
            var component = new ComponentWrapper(new Model.Component());
            //if(SelectedComponent != null)
            //{
            //    component = _componentRepository.GetByIdAsync(_selectedComponent.ComponentId);
            //}

            component.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Component.HasErrors))
                {
                    ((DelegateCommand)CreateComponentCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)CreateComponentCommand).RaiseCanExecuteChanged();


            _componentRepository.Add(component.Model); //context is aware of the equipment to add
            return component;
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

                    if (ComponentChosen.ComponentLookupId == 1) //search for type
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
                OnPropertyChanged();
                if (_selectedComponent != null)
                {
                    Component.ComponentType = SelectedComponent.ComponentType;
                    Component.ComponentSupplyNumber = SelectedComponent.ComponentSupplyNumber;
                    Component.ComponentSerialNumber = SelectedComponent.ComponentSerialNumber;
                    Component.ComponentOrderNumber = SelectedComponent.ComponentOrderNumber;
                    Component.ComponentDescription = SelectedComponent.ComponentDescription;
                    Component.ComponentId = SelectedComponent.ComponentId;
                }
            }
        }

        private ComponentWrapper _component;
        public ComponentWrapper Component
        {
            get { return _component; }
            set
            {
                _component = value;
                OnPropertyChanged();
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
