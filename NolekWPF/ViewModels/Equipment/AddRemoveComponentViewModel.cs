using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NolekWPF.Data.DataServices;
using NolekWPF.Data.Repositories;
using NolekWPF.Events;
using NolekWPF.Model;
using NolekWPF.Model.Dto;
using Prism.Commands;
using Prism.Events;

namespace NolekWPF.ViewModels.Equipment
{
    public class AddRemoveComponentViewModel : ViewModelBase, IAddRemoveComponentViewModel
    {
        private IEquipmentLookupDataService _equipmentLookupDataService;
        private IErrorDataService _errorDataService;
        private IComponentDataService _componentDataService;
        private IEquipmentRepository _equipmentRepository;

        public ObservableCollection<EquipmentLookup> Equipments { get; }
        public ObservableCollection<ComponentDto> Components { get; }
        public ObservableCollection<ComponentDto> ComponentsForEquipment { get; set; }
        public ObservableCollection<EquipmentComponent> EquipmentComponents { get; set; }

        private IEventAggregator _eventAggregator;
        public Login CurrentUser { get; set; }

        public AddRemoveComponentViewModel(IEquipmentLookupDataService equipmentLookupDataService, IErrorDataService errorDataService,
            IComponentDataService componentDataService, IEquipmentRepository equipmentRepository, IEventAggregator eventAggregator)
        {
            _equipmentLookupDataService = equipmentLookupDataService;
            _componentDataService = componentDataService;
            _errorDataService = errorDataService;
            _equipmentRepository = equipmentRepository;
            Equipments = new ObservableCollection<EquipmentLookup>();
            Components = new ObservableCollection<ComponentDto>();
            ComponentsForEquipment = new ObservableCollection<ComponentDto>();
            EquipmentComponents = new ObservableCollection<EquipmentComponent>();

            AddComponent = new DelegateCommand(OnComponentAdded, OnComponentCanAdded);
            RemoveComponent = new DelegateCommand(OnComponentRemoved, OnComponentCanRemoved);
            SaveChanges = new DelegateCommand(OnChangesSaved, OnChangesCanSaved);

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<AfterUserLogin>().Subscribe(OnLogin);
        }

        private void OnLogin(Login user)
        {
            CurrentUser = user;
        }

        //DISABLE BUTTONS BEFORE CHOOSNG EQUIPMENT
        private bool OnChangesCanSaved()
        {
            return SelectedEquipment != null;
        }

        private bool OnComponentCanRemoved()
        {
            return SelectedEquipment != null;
        }
         
        private bool OnComponentCanAdded()
        {
            return SelectedEquipment != null;
        }

        private async void OnChangesSaved()
        {
                await _equipmentRepository.SaveAsync();
                MessageBox.Show("Components have been updated for equipment.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                
        }

        private void OnComponentRemoved()
        {
            if (SelectedListComponentIndex != null)
            {
                _equipmentRepository.RemoveEquipmentComponent(EquipmentComponents[(int)SelectedListComponentIndex]); //remove from context
                EquipmentComponents.RemoveAt((int)_selectListComponentIndex);
                SelectedListComponentIndex = null;
            }
            else
            {
                MessageBox.Show("Please select a component in the list to remove.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        

        private void OnComponentAdded()
        {
            if (SelectedComponent != null && SelectedComponent.ComponentName != null)
            {
                if (EquipmentComponents.Count(ec => ec.ComponentName == SelectedComponent.ComponentName) < 1)
                {
                    EquipmentComponents.Add(new EquipmentComponent()
                    {
                        ComponentID = SelectedComponent.ComponentId,
                        ComponentName = SelectedComponent.ComponentName,
                        EquipmentID = SelectedEquipment.EquipmentId
                    });
                    _equipmentRepository.UpdateComponents(SelectedComponent,
                        SelectedEquipment.EquipmentId); //add to context
                    //await _equipmentRepository.SaveAsync();
                }
                else
                {
                    MessageBox.Show("The component name already exists for this equipment.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a component and specify a component name before adding.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            
        }

        public async Task LoadAsync()
        {
            try
            {
                //LOAD EQUIPMENTS
                var lookup = await _equipmentLookupDataService.GetEquipmentLookupAsync();

                Equipments.Clear();
                foreach (var item in lookup)
                {
                    Equipments.Add(item);
                }
                //LOAD COMPONENTS
                var components = await _componentDataService.GetComponentLookupAsync();

                Components.Clear();
                foreach (var item in components)
                {
                    Components.Add(item);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "An error occurred", MessageBoxButton.OK, MessageBoxImage.Warning);
                //create new error object from the exception and add to DB
                Error error = new Error
                {
                    ErrorMessage = e.Message,
                    ErrorTimeStamp = DateTime.Now,
                    ErrorStackTrace = e.StackTrace
                };
                await _errorDataService.AddError(error);
            }
        }

        public ICommand AddComponent { get; }
        public ICommand RemoveComponent { get; }
        public ICommand SaveChanges { get; }

        //SELECTED EQUIPMENT AND COMPONENT
        private EquipmentLookup _selectedEquipment;

        public EquipmentLookup SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                try
                {
                    _selectedEquipment = value;
                    LoadComponentForEquipment(_selectedEquipment.EquipmentId);
                    ((DelegateCommand)AddComponent).RaiseCanExecuteChanged();
                    ((DelegateCommand)RemoveComponent).RaiseCanExecuteChanged();
                    ((DelegateCommand)SaveChanges).RaiseCanExecuteChanged();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "An error occurred", MessageBoxButton.OK, MessageBoxImage.Warning);
                    //create new error object from the exception and add to DB
                    Error error = new Error
                    {
                        ErrorMessage = e.Message,
                        ErrorTimeStamp = DateTime.Now,
                        ErrorStackTrace = e.StackTrace
                    };
                    _errorDataService.AddError(error);
                }
            }
        }

        private async void LoadComponentForEquipment(int equipmentId)
        {
            var equipmentComponents = _equipmentRepository.GetEquipmentComponents(SelectedEquipment.EquipmentId);
            EquipmentComponents.Clear();
            foreach (var item in equipmentComponents)
            {
                EquipmentComponents.Add(item);
            }
            /*var components = await _componentDataService.GetComponentsByEquipmentIdAsync(equipmentId);
            ComponentsForEquipment.Clear();
            foreach (var item in components)
            {
                ComponentsForEquipment.Add(item);
            }*/
        }

        private ComponentDto _selectComponent;
        public ComponentDto SelectedComponent
        {
            get { return _selectComponent; }
            set
            {
                _selectComponent = value;
                OnPropertyChanged();


            }
        }
        private int? _selectListComponentIndex;

        public int? SelectedListComponentIndex
        {
            get { return _selectListComponentIndex; }
            set
            {
                _selectListComponentIndex = value;
                OnPropertyChanged();
            }
        }
    }
}
