﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NolekWPF.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    using Model;

    public partial class wiki_nolek_dk_dbEntities : DbContext
    {
        public wiki_nolek_dk_dbEntities()
            : base("name=wiki_nolek_dk_dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public virtual DbSet<EquipmentConfiguration> EquipmentConfigurations { get; set; }
        public virtual DbSet<EquipmentType> EquipmentTypes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Error> Errors { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Nolek> Noleks { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ContactPerson> ContactPersons { get; set; }
        public virtual DbSet<EquipmentComponent> EquipmentComponents { get; set; }
        public virtual DbSet<CustomerDepartment> CustomerDepartments { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceReport> ServiceReports { get; set; }
        public virtual DbSet<EquipmentView> EquipmentViews { get; set; }
        public virtual DbSet<LoginRole> LoginRoles { get; set; }
        public virtual DbSet<ComponentChoice> ComponentChoices { get; set; }
        public virtual DbSet<EquipmentChoice> EquipmentChoices { get; set; }
    
        public virtual int CfromE(Nullable<int> equipmentComponentID)
        {
            var equipmentComponentIDParameter = equipmentComponentID.HasValue ?
                new ObjectParameter("EquipmentComponentID", equipmentComponentID) :
                new ObjectParameter("EquipmentComponentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CfromE", equipmentComponentIDParameter);
        }
    
        public virtual int ComponentNametoID(string componentName)
        {
            var componentNameParameter = componentName != null ?
                new ObjectParameter("ComponentName", componentName) :
                new ObjectParameter("ComponentName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ComponentNametoID", componentNameParameter);
        }
    
        public virtual int CreateComponentSP(Nullable<int> componentID, string componentName, string componentDescription, string nolekOrderNumber, string nolekSerialNumber, Nullable<int> componentQty, string componentSupplyNo)
        {
            var componentIDParameter = componentID.HasValue ?
                new ObjectParameter("ComponentID", componentID) :
                new ObjectParameter("ComponentID", typeof(int));
    
            var componentNameParameter = componentName != null ?
                new ObjectParameter("ComponentName", componentName) :
                new ObjectParameter("ComponentName", typeof(string));
    
            var componentDescriptionParameter = componentDescription != null ?
                new ObjectParameter("ComponentDescription", componentDescription) :
                new ObjectParameter("ComponentDescription", typeof(string));
    
            var nolekOrderNumberParameter = nolekOrderNumber != null ?
                new ObjectParameter("NolekOrderNumber", nolekOrderNumber) :
                new ObjectParameter("NolekOrderNumber", typeof(string));
    
            var nolekSerialNumberParameter = nolekSerialNumber != null ?
                new ObjectParameter("NolekSerialNumber", nolekSerialNumber) :
                new ObjectParameter("NolekSerialNumber", typeof(string));
    
            var componentQtyParameter = componentQty.HasValue ?
                new ObjectParameter("ComponentQty", componentQty) :
                new ObjectParameter("ComponentQty", typeof(int));
    
            var componentSupplyNoParameter = componentSupplyNo != null ?
                new ObjectParameter("ComponentSupplyNo", componentSupplyNo) :
                new ObjectParameter("ComponentSupplyNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateComponentSP", componentIDParameter, componentNameParameter, componentDescriptionParameter, nolekOrderNumberParameter, nolekSerialNumberParameter, componentQtyParameter, componentSupplyNoParameter);
        }
    
        public virtual int CreateEquipmentSP(Nullable<int> equipmentID, Nullable<System.DateTime> date, string equipmentSN, Nullable<int> equipmentCID, Nullable<int> equipmentTypeID, string mainEquipmentNumber, Nullable<int> departmentID, Nullable<bool> equipmentStatus)
        {
            var equipmentIDParameter = equipmentID.HasValue ?
                new ObjectParameter("EquipmentID", equipmentID) :
                new ObjectParameter("EquipmentID", typeof(int));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("Date", date) :
                new ObjectParameter("Date", typeof(System.DateTime));
    
            var equipmentSNParameter = equipmentSN != null ?
                new ObjectParameter("EquipmentSN", equipmentSN) :
                new ObjectParameter("EquipmentSN", typeof(string));
    
            var equipmentCIDParameter = equipmentCID.HasValue ?
                new ObjectParameter("EquipmentCID", equipmentCID) :
                new ObjectParameter("EquipmentCID", typeof(int));
    
            var equipmentTypeIDParameter = equipmentTypeID.HasValue ?
                new ObjectParameter("EquipmentTypeID", equipmentTypeID) :
                new ObjectParameter("EquipmentTypeID", typeof(int));
    
            var mainEquipmentNumberParameter = mainEquipmentNumber != null ?
                new ObjectParameter("MainEquipmentNumber", mainEquipmentNumber) :
                new ObjectParameter("MainEquipmentNumber", typeof(string));
    
            var departmentIDParameter = departmentID.HasValue ?
                new ObjectParameter("DepartmentID", departmentID) :
                new ObjectParameter("DepartmentID", typeof(int));
    
            var equipmentStatusParameter = equipmentStatus.HasValue ?
                new ObjectParameter("EquipmentStatus", equipmentStatus) :
                new ObjectParameter("EquipmentStatus", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateEquipmentSP", equipmentIDParameter, dateParameter, equipmentSNParameter, equipmentCIDParameter, equipmentTypeIDParameter, mainEquipmentNumberParameter, departmentIDParameter, equipmentStatusParameter);
        }
    
        public virtual int CtoE(Nullable<int> componentID, Nullable<int> equipmentID)
        {
            var componentIDParameter = componentID.HasValue ?
                new ObjectParameter("ComponentID", componentID) :
                new ObjectParameter("ComponentID", typeof(int));
    
            var equipmentIDParameter = equipmentID.HasValue ?
                new ObjectParameter("EquipmentID", equipmentID) :
                new ObjectParameter("EquipmentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CtoE", componentIDParameter, equipmentIDParameter);
        }
    
        public virtual int DeleteComponentSP(Nullable<int> componentID)
        {
            var componentIDParameter = componentID.HasValue ?
                new ObjectParameter("ComponentID", componentID) :
                new ObjectParameter("ComponentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteComponentSP", componentIDParameter);
        }
    
        public virtual int DeleteEquipmentSP(Nullable<int> equipmentID)
        {
            var equipmentIDParameter = equipmentID.HasValue ?
                new ObjectParameter("EquipmentID", equipmentID) :
                new ObjectParameter("EquipmentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteEquipmentSP", equipmentIDParameter);
        }
    
        public virtual int GetDate(Nullable<int> serviceRapportID)
        {
            var serviceRapportIDParameter = serviceRapportID.HasValue ?
                new ObjectParameter("ServiceRapportID", serviceRapportID) :
                new ObjectParameter("ServiceRapportID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetDate", serviceRapportIDParameter);
        }
    
        public virtual int GetRapport(Nullable<int> customerID)
        {
            var customerIDParameter = customerID.HasValue ?
                new ObjectParameter("CustomerID", customerID) :
                new ObjectParameter("CustomerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetRapport", customerIDParameter);
        }
    
        public virtual int GetRapport2(Nullable<int> customerID)
        {
            var customerIDParameter = customerID.HasValue ?
                new ObjectParameter("CustomerID", customerID) :
                new ObjectParameter("CustomerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetRapport2", customerIDParameter);
        }
    
        public virtual int GetRapport3(Nullable<int> customerID)
        {
            var customerIDParameter = customerID.HasValue ?
                new ObjectParameter("CustomerID", customerID) :
                new ObjectParameter("CustomerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetRapport3", customerIDParameter);
        }
    
        public virtual int GetTechName(Nullable<int> serviceRapportID)
        {
            var serviceRapportIDParameter = serviceRapportID.HasValue ?
                new ObjectParameter("ServiceRapportID", serviceRapportID) :
                new ObjectParameter("ServiceRapportID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetTechName", serviceRapportIDParameter);
        }
    
        public virtual int GetWork(Nullable<int> serviceRapportID)
        {
            var serviceRapportIDParameter = serviceRapportID.HasValue ?
                new ObjectParameter("ServiceRapportID", serviceRapportID) :
                new ObjectParameter("ServiceRapportID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetWork", serviceRapportIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> ReadComponent2SP(Nullable<int> componentID)
        {
            var componentIDParameter = componentID.HasValue ?
                new ObjectParameter("ComponentID", componentID) :
                new ObjectParameter("ComponentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("ReadComponent2SP", componentIDParameter);
        }
    
        public virtual int ReadComponentSimpleSP()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ReadComponentSimpleSP");
        }
    
        public virtual ObjectResult<ReadComponentSP_Result> ReadComponentSP()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReadComponentSP_Result>("ReadComponentSP");
        }
    
        public virtual ObjectResult<ReadEquipment_Result> ReadEquipment()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReadEquipment_Result>("ReadEquipment");
        }
    
        public virtual int ReadEquipmentSimpleSP()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ReadEquipmentSimpleSP");
        }
    
        public virtual int ReadEquipmentSuperSimple()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ReadEquipmentSuperSimple");
        }
    
        public virtual int ReadEquipmentTypeDesignationSP(string equipmentTypeDesignation)
        {
            var equipmentTypeDesignationParameter = equipmentTypeDesignation != null ?
                new ObjectParameter("EquipmentTypeDesignation", equipmentTypeDesignation) :
                new ObjectParameter("EquipmentTypeDesignation", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ReadEquipmentTypeDesignationSP", equipmentTypeDesignationParameter);
        }
    
        public virtual ObjectResult<string> ReadImagePath(Nullable<int> equipmentID)
        {
            var equipmentIDParameter = equipmentID.HasValue ?
                new ObjectParameter("EquipmentID", equipmentID) :
                new ObjectParameter("EquipmentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("ReadImagePath", equipmentIDParameter);
        }
    
        public virtual int ServiceTime(Nullable<int> equipmentID)
        {
            var equipmentIDParameter = equipmentID.HasValue ?
                new ObjectParameter("EquipmentID", equipmentID) :
                new ObjectParameter("EquipmentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ServiceTime", equipmentIDParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int UpdateImagePath(Nullable<int> equipmentID, string equipmentImagePath)
        {
            var equipmentIDParameter = equipmentID.HasValue ?
                new ObjectParameter("EquipmentID", equipmentID) :
                new ObjectParameter("EquipmentID", typeof(int));
    
            var equipmentImagePathParameter = equipmentImagePath != null ?
                new ObjectParameter("EquipmentImagePath", equipmentImagePath) :
                new ObjectParameter("EquipmentImagePath", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateImagePath", equipmentIDParameter, equipmentImagePathParameter);
        }
    
        public virtual int UpdComponent(Nullable<int> componentID, string componentName, string componentDescription, string nolekOrderNumber, string nolekSerialNumber, Nullable<int> componentQty, string componentSupplyNo)
        {
            var componentIDParameter = componentID.HasValue ?
                new ObjectParameter("ComponentID", componentID) :
                new ObjectParameter("ComponentID", typeof(int));
    
            var componentNameParameter = componentName != null ?
                new ObjectParameter("ComponentName", componentName) :
                new ObjectParameter("ComponentName", typeof(string));
    
            var componentDescriptionParameter = componentDescription != null ?
                new ObjectParameter("ComponentDescription", componentDescription) :
                new ObjectParameter("ComponentDescription", typeof(string));
    
            var nolekOrderNumberParameter = nolekOrderNumber != null ?
                new ObjectParameter("NolekOrderNumber", nolekOrderNumber) :
                new ObjectParameter("NolekOrderNumber", typeof(string));
    
            var nolekSerialNumberParameter = nolekSerialNumber != null ?
                new ObjectParameter("NolekSerialNumber", nolekSerialNumber) :
                new ObjectParameter("NolekSerialNumber", typeof(string));
    
            var componentQtyParameter = componentQty.HasValue ?
                new ObjectParameter("ComponentQty", componentQty) :
                new ObjectParameter("ComponentQty", typeof(int));
    
            var componentSupplyNoParameter = componentSupplyNo != null ?
                new ObjectParameter("ComponentSupplyNo", componentSupplyNo) :
                new ObjectParameter("ComponentSupplyNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdComponent", componentIDParameter, componentNameParameter, componentDescriptionParameter, nolekOrderNumberParameter, nolekSerialNumberParameter, componentQtyParameter, componentSupplyNoParameter);
        }
    
        public virtual int UpdEquipment(Nullable<int> equipmentID, Nullable<System.DateTime> dateOfCreation, string equipmentSerialNumber, Nullable<int> equipmentConfigurationID, Nullable<int> equipmentTypeID, string mainEquipmentNumber, Nullable<int> nolekDepartmentID, Nullable<bool> equipmentStatus)
        {
            var equipmentIDParameter = equipmentID.HasValue ?
                new ObjectParameter("EquipmentID", equipmentID) :
                new ObjectParameter("EquipmentID", typeof(int));
    
            var dateOfCreationParameter = dateOfCreation.HasValue ?
                new ObjectParameter("DateOfCreation", dateOfCreation) :
                new ObjectParameter("DateOfCreation", typeof(System.DateTime));
    
            var equipmentSerialNumberParameter = equipmentSerialNumber != null ?
                new ObjectParameter("EquipmentSerialNumber", equipmentSerialNumber) :
                new ObjectParameter("EquipmentSerialNumber", typeof(string));
    
            var equipmentConfigurationIDParameter = equipmentConfigurationID.HasValue ?
                new ObjectParameter("EquipmentConfigurationID", equipmentConfigurationID) :
                new ObjectParameter("EquipmentConfigurationID", typeof(int));
    
            var equipmentTypeIDParameter = equipmentTypeID.HasValue ?
                new ObjectParameter("EquipmentTypeID", equipmentTypeID) :
                new ObjectParameter("EquipmentTypeID", typeof(int));
    
            var mainEquipmentNumberParameter = mainEquipmentNumber != null ?
                new ObjectParameter("MainEquipmentNumber", mainEquipmentNumber) :
                new ObjectParameter("MainEquipmentNumber", typeof(string));
    
            var nolekDepartmentIDParameter = nolekDepartmentID.HasValue ?
                new ObjectParameter("NolekDepartmentID", nolekDepartmentID) :
                new ObjectParameter("NolekDepartmentID", typeof(int));
    
            var equipmentStatusParameter = equipmentStatus.HasValue ?
                new ObjectParameter("EquipmentStatus", equipmentStatus) :
                new ObjectParameter("EquipmentStatus", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdEquipment", equipmentIDParameter, dateOfCreationParameter, equipmentSerialNumberParameter, equipmentConfigurationIDParameter, equipmentTypeIDParameter, mainEquipmentNumberParameter, nolekDepartmentIDParameter, equipmentStatusParameter);
        }
    }
}
