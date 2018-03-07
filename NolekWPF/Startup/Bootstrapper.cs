using Autofac;
using NolekWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.DataAccess;
using Prism.Events;
using NolekWPF.Data.DataServices;
using NolekWPF.Data.Repositories;
using NolekWPF.Equipment.ViewModels;
using NolekWPF.ViewModels.Component;
using NolekWPF.ViewModels.Customers;
using NolekWPF.ViewModels.Equipment;

namespace NolekWPF.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            //windows
            builder.RegisterType<MainWindow>().AsSelf();

            //db context
            builder.RegisterType<wiki_nolek_dk_dbEntities>().AsSelf(); //create new context when it is needed

            //data services
            builder.RegisterType<EquipmentDataService>().As<IEquipmentDataService>();
            builder.RegisterType<EquipmentLookupDataService>().As<IEquipmentLookupDataService>();
            builder.RegisterType<ErrorDataService>().As<IErrorDataService>();
            builder.RegisterType<ComponentDataService>().As<IComponentDataService>();
            builder.RegisterType<UserDataService>().As<IUserDataService>();
            builder.RegisterType<UserLookupDataService>().As<IUserLookupDataService>();
            builder.RegisterType<CustomerDataService>().As<ICustomerDataService>();

            //repositories
            builder.RegisterType<EquipmentRepository>().As<IEquipmentRepository>();
            builder.RegisterType<ComponentRepository>().As<IComponentRepository>();
            builder.RegisterType<ErrorRepository>().As<IErrorRepository>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();

            //view models

            //Equipment
            builder.RegisterType<EquipmentListViewModel>().As<IEquipmentListViewModel>();
            builder.RegisterType<EquipmentCreateViewModel>().As<IEquipmentCreateViewModel>();
            builder.RegisterType<EquipmentDetailViewModel>().As<IEquipmentDetailViewModel>();
            builder.RegisterType<AddRemoveComponentViewModel>().As<IAddRemoveComponentViewModel>();

            //Component
            builder.RegisterType<ComponentListViewModel>().As<IComponentListViewModel>();
            builder.RegisterType<ComponentCreateViewModel>().As<IComponentCreateViewModel>();
            builder.RegisterType<ComponentDetailViewModel>().As<IComponentDetailViewModel>();

            //Customer
            builder.RegisterType<CustomerCreateViewModel>().As<ICustomerCreateViewModel>();
            builder.RegisterType<CustomerListViewModel>().As<ICustomerListViewModel>();
            builder.RegisterType<AddRemoveEquipmentToFromCustomerViewModel>().As<IAddRemoveEquipmentToFromCustomerViewModel>();

            builder.RegisterType<MainViewModel>().AsSelf();

            //register event aggregators, det er en singleton
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            //Eventaggregator skal være singleinstance. De snakker jo alle igennem den samme event
            //builder.RegisterType<EventAggregator>().As<IEventAggregator>();

            return builder.Build();
        }
    }
}
