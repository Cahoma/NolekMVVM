using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NolekWPF.Model;
using NolekWPF.Model.Dto;

namespace NolekWPF.ViewModels.Component
{
    public interface IComponentListViewModel
    {
        IComponentDetailViewModel ComponentDetailViewModel { get; }
        ObservableCollection<ComponentDto> Components { get; }
        ComponentDto SelectedComponent { get; set; }

        Task LoadAsync();
        Task LoadComponentChoiceAsync();
    }
}