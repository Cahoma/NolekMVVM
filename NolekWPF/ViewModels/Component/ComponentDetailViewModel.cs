using NolekWPF.Data.DataServices;
using NolekWPF.Data.Repositories;
using NolekWPF.Events;
using NolekWPF.Model;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NolekWPF.ViewModels.Component
{
    public class ComponentDetailViewModel : ViewModelBase, IComponentDetailViewModel
    {
        private IComponentDataService _dataService;
        private IComponentRepository _componentRepository;
        private IEventAggregator _eventAggregator;
        private IErrorDataService _errorDataService;
        public Login CurrentUser { get; set; }

        public ComponentDetailViewModel(IComponentDataService dataService,
            IEventAggregator eventAggregator, IErrorDataService errorDataService, IComponentRepository componentRepository)
        {
            _dataService = dataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenComponentDetailViewEvent>()
                .Subscribe(OnOpenComponentDetailView);
            _errorDataService = errorDataService;
            _eventAggregator.GetEvent<AfterUserLogin>().Subscribe(OnLogin);
            _componentRepository = componentRepository;
            UpdateCommand = new DelegateCommand(OnUpdateExecute);
        }

        private void OnLogin(Login user)
        {
            CurrentUser = user;
        }

        public ICommand UpdateCommand { get; set; }

        private async void OnOpenComponentDetailView(int componentId)
        {
            await LoadAsync(componentId);
        }

        public async void OnUpdateExecute()
        {
            try
            {
                await _componentRepository.SaveAsync();
                Component = UpdateComponent();
                MessageBox.Show("Component was successfully updated.");
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

        public Model.Component UpdateComponent()
        {
            return new Model.Component();
        }

        public async Task LoadAsync(int componentId)
        {
            try
            {
                Component = await _componentRepository.GetByIdAsync(componentId);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "An error occurred", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private Model.Component _component;
        public Model.Component Component
        {
            get { return _component; }
            private set
            {
                _component = value;
                OnPropertyChanged();
            }
        }
    }
}
