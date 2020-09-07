using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediti2.WebConnector
{
    using System.Runtime.Serialization;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WebDemographicData_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.WebConnector")]
    public partial class WebDemographicData_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private Mediti2.WebConnector.WebAddress_V1 PhysicalAddressField;

        private Mediti2.WebConnector.WebAddress_V1 PostalAddressField;

        private string PrimaryPhoneField;

        private string SecondaryPhoneField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public Mediti2.WebConnector.WebAddress_V1 PhysicalAddress
        {
            get
            {
                return this.PhysicalAddressField;
            }
            set
            {
                this.PhysicalAddressField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public Mediti2.WebConnector.WebAddress_V1 PostalAddress
        {
            get
            {
                return this.PostalAddressField;
            }
            set
            {
                this.PostalAddressField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PrimaryPhone
        {
            get
            {
                return this.PrimaryPhoneField;
            }
            set
            {
                this.PrimaryPhoneField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SecondaryPhone
        {
            get
            {
                return this.SecondaryPhoneField;
            }
            set
            {
                this.SecondaryPhoneField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WebAddress_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.WebConnector")]
    public partial class WebAddress_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string AddressLine1Field;

        private string AddressLine2Field;

        private string CityField;

        private System.Nullable<int> NeighborhoodIDField;

        private string PlusFourField;

        private string StateField;

        private string ZipCodeField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AddressLine1
        {
            get
            {
                return this.AddressLine1Field;
            }
            set
            {
                this.AddressLine1Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AddressLine2
        {
            get
            {
                return this.AddressLine2Field;
            }
            set
            {
                this.AddressLine2Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string City
        {
            get
            {
                return this.CityField;
            }
            set
            {
                this.CityField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> NeighborhoodID
        {
            get
            {
                return this.NeighborhoodIDField;
            }
            set
            {
                this.NeighborhoodIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PlusFour
        {
            get
            {
                return this.PlusFourField;
            }
            set
            {
                this.PlusFourField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string State
        {
            get
            {
                return this.StateField;
            }
            set
            {
                this.StateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ZipCode
        {
            get
            {
                return this.ZipCodeField;
            }
            set
            {
                this.ZipCodeField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WebApplication_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.WebConnector")]
    public partial class WebApplication_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private Mediti2.WebConnector.WebPerson_V1[] ApplicantsField;

        private Mediti2.WebConnector.WebDemographicData_V1 DemographicDataField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public Mediti2.WebConnector.WebPerson_V1[] Applicants
        {
            get
            {
                return this.ApplicantsField;
            }
            set
            {
                this.ApplicantsField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public Mediti2.WebConnector.WebDemographicData_V1 DemographicData
        {
            get
            {
                return this.DemographicDataField;
            }
            set
            {
                this.DemographicDataField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WebPerson_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.WebConnector")]
    public partial class WebPerson_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private Mediti2.WebConnector.WebCivilStatus_V1 CivilStatusEnumField;

        private System.DateTime DateOfBirthField;

        private byte ExpectedChildCountField;

        private string FirstNameField;

        private bool HasPartAorBField;

        private int[] HealthInsuranceField;

        private bool IncarceratedField;

        private Mediti2.WebConnector.WebIncomeDebt_V1[] IncomeDebtField;

        private bool IsBlindField;

        private bool IsDisableField;

        private bool IsParentCaretakerField;

        private bool IsPoliceField;

        private bool IsPregnantField;

        private string LastName1Field;

        private string LastName2Field;

        private bool LawfulPresenceField;

        private string MiddleNameField;

        private int PersonIDField;

        private Mediti2.WebConnector.WebRelation_V1[] RelationshipsField;

        private Mediti2.WebConnector.WebSex_V1 SexEnumField;

        private bool VeteranField;

        private bool WantHealthCoverageField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public Mediti2.WebConnector.WebCivilStatus_V1 CivilStatusEnum
        {
            get
            {
                return this.CivilStatusEnumField;
            }
            set
            {
                this.CivilStatusEnumField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DateOfBirth
        {
            get
            {
                return this.DateOfBirthField;
            }
            set
            {
                this.DateOfBirthField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte ExpectedChildCount
        {
            get
            {
                return this.ExpectedChildCountField;
            }
            set
            {
                this.ExpectedChildCountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName
        {
            get
            {
                return this.FirstNameField;
            }
            set
            {
                this.FirstNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool HasPartAorB
        {
            get
            {
                return this.HasPartAorBField;
            }
            set
            {
                this.HasPartAorBField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int[] HealthInsurance
        {
            get
            {
                return this.HealthInsuranceField;
            }
            set
            {
                this.HealthInsuranceField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Incarcerated
        {
            get
            {
                return this.IncarceratedField;
            }
            set
            {
                this.IncarceratedField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public Mediti2.WebConnector.WebIncomeDebt_V1[] IncomeDebt
        {
            get
            {
                return this.IncomeDebtField;
            }
            set
            {
                this.IncomeDebtField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsBlind
        {
            get
            {
                return this.IsBlindField;
            }
            set
            {
                this.IsBlindField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsDisable
        {
            get
            {
                return this.IsDisableField;
            }
            set
            {
                this.IsDisableField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsParentCaretaker
        {
            get
            {
                return this.IsParentCaretakerField;
            }
            set
            {
                this.IsParentCaretakerField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsPolice
        {
            get
            {
                return this.IsPoliceField;
            }
            set
            {
                this.IsPoliceField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsPregnant
        {
            get
            {
                return this.IsPregnantField;
            }
            set
            {
                this.IsPregnantField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName1
        {
            get
            {
                return this.LastName1Field;
            }
            set
            {
                this.LastName1Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName2
        {
            get
            {
                return this.LastName2Field;
            }
            set
            {
                this.LastName2Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool LawfulPresence
        {
            get
            {
                return this.LawfulPresenceField;
            }
            set
            {
                this.LawfulPresenceField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MiddleName
        {
            get
            {
                return this.MiddleNameField;
            }
            set
            {
                this.MiddleNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PersonID
        {
            get
            {
                return this.PersonIDField;
            }
            set
            {
                this.PersonIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public Mediti2.WebConnector.WebRelation_V1[] Relationships
        {
            get
            {
                return this.RelationshipsField;
            }
            set
            {
                this.RelationshipsField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public Mediti2.WebConnector.WebSex_V1 SexEnum
        {
            get
            {
                return this.SexEnumField;
            }
            set
            {
                this.SexEnumField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Veteran
        {
            get
            {
                return this.VeteranField;
            }
            set
            {
                this.VeteranField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool WantHealthCoverage
        {
            get
            {
                return this.WantHealthCoverageField;
            }
            set
            {
                this.WantHealthCoverageField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WebCivilStatus_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.WebConnector")]
    public enum WebCivilStatus_V1 : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Single = 1099,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Married = 1100,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Divorced = 1101,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Widowed = 1102,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Separated = 1103,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Unknown = 1145,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WebIncomeDebt_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.WebConnector")]
    public partial class WebIncomeDebt_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private decimal AmountField;

        private int FinancialInfoIDField;

        private byte FinancialIntervalIDField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Amount
        {
            get
            {
                return this.AmountField;
            }
            set
            {
                this.AmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int FinancialInfoID
        {
            get
            {
                return this.FinancialInfoIDField;
            }
            set
            {
                this.FinancialInfoIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte FinancialIntervalID
        {
            get
            {
                return this.FinancialIntervalIDField;
            }
            set
            {
                this.FinancialIntervalIDField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WebRelation_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.WebConnector")]
    public partial class WebRelation_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private int PersonIDField;

        private int RelationTypeIDField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PersonID
        {
            get
            {
                return this.PersonIDField;
            }
            set
            {
                this.PersonIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RelationTypeID
        {
            get
            {
                return this.RelationTypeIDField;
            }
            set
            {
                this.RelationTypeIDField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WebSex_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.WebConnector")]
    public enum WebSex_V1 : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Unknown = 1124,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Female = 1098,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Male = 1097,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "CalculationType_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.WebConnector")]
    public enum CalculationType_V1 : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Magi = 10,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        NoMagi = 20,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Transition = 30,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WebApplicationResponse_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.WebConnector")]
    public partial class WebApplicationResponse_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private Mediti2.WebConnector.WebApplicationStatus_V1 ApplicationStatusField;

        private Mediti2.WebConnector.WebPersonEligibility_V1[] EligibilitiesField;

        private string ErrorMessageENField;

        private string ErrorMessageESField;

        private System.Nullable<System.Guid> ErrorNumberField;

        private bool ReceivedField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public Mediti2.WebConnector.WebApplicationStatus_V1 ApplicationStatus
        {
            get
            {
                return this.ApplicationStatusField;
            }
            set
            {
                this.ApplicationStatusField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public Mediti2.WebConnector.WebPersonEligibility_V1[] Eligibilities
        {
            get
            {
                return this.EligibilitiesField;
            }
            set
            {
                this.EligibilitiesField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorMessageEN
        {
            get
            {
                return this.ErrorMessageENField;
            }
            set
            {
                this.ErrorMessageENField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorMessageES
        {
            get
            {
                return this.ErrorMessageESField;
            }
            set
            {
                this.ErrorMessageESField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.Guid> ErrorNumber
        {
            get
            {
                return this.ErrorNumberField;
            }
            set
            {
                this.ErrorNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Received
        {
            get
            {
                return this.ReceivedField;
            }
            set
            {
                this.ReceivedField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WebApplicationStatus_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.WebConnector")]
    public enum WebApplicationStatus_V1 : byte
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Error = 10,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Received = 20,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Preliminary = 30,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        WaitingForAction = 40,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Rejected = 90,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Processed = 100,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WebPersonEligibility_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.WebConnector")]
    public partial class WebPersonEligibility_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private Mediti2.WebConnector.WebEligibility_V1 EligibilityField;

        private int PersonIDField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public Mediti2.WebConnector.WebEligibility_V1 Eligibility
        {
            get
            {
                return this.EligibilityField;
            }
            set
            {
                this.EligibilityField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PersonID
        {
            get
            {
                return this.PersonIDField;
            }
            set
            {
                this.PersonIDField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WebEligibility_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.WebConnector")]
    public enum WebEligibility_V1 : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Ineligible = 100,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Commonwealth = 200,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Medicaid = 300,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        CHIP = 400,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WebApplicationStatusPair_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.WebConnector")]
    public partial class WebApplicationStatusPair_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private Mediti2.WebConnector.WebApplicationStatus_V1 StatusField;

        private int WebApplicationIDField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public Mediti2.WebConnector.WebApplicationStatus_V1 Status
        {
            get
            {
                return this.StatusField;
            }
            set
            {
                this.StatusField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int WebApplicationID
        {
            get
            {
                return this.WebApplicationIDField;
            }
            set
            {
                this.WebApplicationIDField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WebMediti2ApplicationInfo_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.WebConnector")]
    public partial class WebMediti2ApplicationInfo_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private int ApplicationIDField;

        private string ApplicationNumberField;

        private System.DateTime ProcessDateField;

        private string StatusENField;

        private string StatusESField;

        private System.Nullable<int> WebApplicationIDField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ApplicationID
        {
            get
            {
                return this.ApplicationIDField;
            }
            set
            {
                this.ApplicationIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ApplicationNumber
        {
            get
            {
                return this.ApplicationNumberField;
            }
            set
            {
                this.ApplicationNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ProcessDate
        {
            get
            {
                return this.ProcessDateField;
            }
            set
            {
                this.ProcessDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StatusEN
        {
            get
            {
                return this.StatusENField;
            }
            set
            {
                this.StatusENField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StatusES
        {
            get
            {
                return this.StatusESField;
            }
            set
            {
                this.StatusESField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> WebApplicationID
        {
            get
            {
                return this.WebApplicationIDField;
            }
            set
            {
                this.WebApplicationIDField = value;
            }
        }
    }
}

