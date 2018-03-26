using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using NolekWPF.Model;
using NolekWPF.Model.Dto;
using NolekWPF.Wrappers;

namespace NolekWPF.ViewModels.Component
{
    public interface IComponentGeneralViewModel
    {
        IComponentListViewModel ComponentListViewModel { get; }
        ObservableCollection<ComponentDto> Components { get; }
        ComponentDto SelectedComponent { get; set; }
        ComponentWrapper Component { get; }

        Task LoadAsync();
        Task LoadComponentChoiceAsync();
    }
}