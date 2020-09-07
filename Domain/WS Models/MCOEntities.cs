namespace Mediti2.McoConnector
{
    using System.Runtime.Serialization;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "AppPerson_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.McoConnector")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Mediti2.McoConnector.AppPersonSearchResult_V1))]
    public partial class AppPerson_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private System.DateTime DateOfBirthField;

        private string FirstNameField;

        private string LastName1Field;

        private string LastName2Field;

        private string MPIField;

        private string MiddleNameField;

        private int PersonIDField;

        private string SsnLast4Field;

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
        public string MPI
        {
            get
            {
                return this.MPIField;
            }
            set
            {
                this.MPIField = value;
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
        public string SsnLast4
        {
            get
            {
                return this.SsnLast4Field;
            }
            set
            {
                this.SsnLast4Field = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "AppPersonSearchResult_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.McoConnector")]
    public partial class AppPersonSearchResult_V1 : Mediti2.McoConnector.AppPerson_V1
    {

        private Mediti2.McoConnector.AppMemberContactResult_V1[] ActiveApplicationMembersField;

        private Mediti2.McoConnector.AppPerson_V1[] AdditionalMatchesField;

        [System.Runtime.Serialization.DataMemberAttribute()]
        public Mediti2.McoConnector.AppMemberContactResult_V1[] ActiveApplicationMembers
        {
            get
            {
                return this.ActiveApplicationMembersField;
            }
            set
            {
                this.ActiveApplicationMembersField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public Mediti2.McoConnector.AppPerson_V1[] AdditionalMatches
        {
            get
            {
                return this.AdditionalMatchesField;
            }
            set
            {
                this.AdditionalMatchesField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "AppMemberContactResult_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.McoConnector")]
    public partial class AppMemberContactResult_V1 : Mediti2.McoConnector.AppMember_V1
    {

        private string ApplicationNumberField;

        private System.Nullable<System.DateTime> CertificationDateField;

        private string ContactFirstNameField;

        private string ContactLastName1Field;

        private string ContactLastName2Field;

        private string ContactMPIField;

        private string ContactMiddleNameField;

        private int ContactPersonIDField;

        private System.Nullable<System.DateTime> EffectiveDateField;

        private System.Nullable<System.DateTime> ExpirationDateField;

        private System.Nullable<System.DateTime> ResultModifierDateField;

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
        public System.Nullable<System.DateTime> CertificationDate
        {
            get
            {
                return this.CertificationDateField;
            }
            set
            {
                this.CertificationDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContactFirstName
        {
            get
            {
                return this.ContactFirstNameField;
            }
            set
            {
                this.ContactFirstNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContactLastName1
        {
            get
            {
                return this.ContactLastName1Field;
            }
            set
            {
                this.ContactLastName1Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContactLastName2
        {
            get
            {
                return this.ContactLastName2Field;
            }
            set
            {
                this.ContactLastName2Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContactMPI
        {
            get
            {
                return this.ContactMPIField;
            }
            set
            {
                this.ContactMPIField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContactMiddleName
        {
            get
            {
                return this.ContactMiddleNameField;
            }
            set
            {
                this.ContactMiddleNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ContactPersonID
        {
            get
            {
                return this.ContactPersonIDField;
            }
            set
            {
                this.ContactPersonIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> EffectiveDate
        {
            get
            {
                return this.EffectiveDateField;
            }
            set
            {
                this.EffectiveDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> ExpirationDate
        {
            get
            {
                return this.ExpirationDateField;
            }
            set
            {
                this.ExpirationDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> ResultModifierDate
        {
            get
            {
                return this.ResultModifierDateField;
            }
            set
            {
                this.ResultModifierDateField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "AppMember_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.McoConnector")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Mediti2.McoConnector.AppMemberContactResult_V1))]
    public partial class AppMember_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private int ApplicationIDField;

        private int ApplicationMemberIDField;

        private string FirstNameField;

        private string LastName1Field;

        private string LastName2Field;

        private string MPIField;

        private string MiddleNameField;

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
        public int ApplicationMemberID
        {
            get
            {
                return this.ApplicationMemberIDField;
            }
            set
            {
                this.ApplicationMemberIDField = value;
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
        public string MPI
        {
            get
            {
                return this.MPIField;
            }
            set
            {
                this.MPIField = value;
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
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "AppMemberInfo_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.McoConnector")]
    public partial class AppMemberInfo_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private int ApplicationIDField;

        private int ApplicationMemberIDField;

        private string ApplicationNumberField;

        private string CarrierNameField;

        private System.Nullable<int> ContactApplicationMemberIDField;

        private string ContactFirstNameField;

        private string ContactLastName1Field;

        private string ContactLastName2Field;

        private string ContactMPIField;

        private string ContactMiddleNameField;

        private System.Nullable<int> ContactPersonIDField;

        private string ContactSsnLast4Field;

        private System.Nullable<System.DateTime> EffectiveDateField;

        private string EligibilityENField;

        private string EligibilityESField;

        private byte EligibilityIDField;

        private System.Nullable<System.DateTime> ExpirationDateField;

        private string FirstNameField;

        private System.Nullable<bool> IsAvailableForChangeField;

        private string LastName1Field;

        private string LastName2Field;

        private string MPIField;

        private System.Nullable<int> ManagedCareOrganizationIDField;

        private string McoDescriptionField;

        private string MiddleNameField;

        private string PcpDescriptionField;

        private string PcpNpiField;

        private int PersonIDField;

        private string PmgDescriptionField;

        private string PmgTaxIDField;

        private string ReasonField;

        private System.Nullable<System.DateTime> ResultModifierDateField;

        private string SSNField;

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
        public int ApplicationMemberID
        {
            get
            {
                return this.ApplicationMemberIDField;
            }
            set
            {
                this.ApplicationMemberIDField = value;
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
        public string CarrierName
        {
            get
            {
                return this.CarrierNameField;
            }
            set
            {
                this.CarrierNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ContactApplicationMemberID
        {
            get
            {
                return this.ContactApplicationMemberIDField;
            }
            set
            {
                this.ContactApplicationMemberIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContactFirstName
        {
            get
            {
                return this.ContactFirstNameField;
            }
            set
            {
                this.ContactFirstNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContactLastName1
        {
            get
            {
                return this.ContactLastName1Field;
            }
            set
            {
                this.ContactLastName1Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContactLastName2
        {
            get
            {
                return this.ContactLastName2Field;
            }
            set
            {
                this.ContactLastName2Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContactMPI
        {
            get
            {
                return this.ContactMPIField;
            }
            set
            {
                this.ContactMPIField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContactMiddleName
        {
            get
            {
                return this.ContactMiddleNameField;
            }
            set
            {
                this.ContactMiddleNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ContactPersonID
        {
            get
            {
                return this.ContactPersonIDField;
            }
            set
            {
                this.ContactPersonIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContactSsnLast4
        {
            get
            {
                return this.ContactSsnLast4Field;
            }
            set
            {
                this.ContactSsnLast4Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> EffectiveDate
        {
            get
            {
                return this.EffectiveDateField;
            }
            set
            {
                this.EffectiveDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EligibilityEN
        {
            get
            {
                return this.EligibilityENField;
            }
            set
            {
                this.EligibilityENField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EligibilityES
        {
            get
            {
                return this.EligibilityESField;
            }
            set
            {
                this.EligibilityESField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte EligibilityID
        {
            get
            {
                return this.EligibilityIDField;
            }
            set
            {
                this.EligibilityIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> ExpirationDate
        {
            get
            {
                return this.ExpirationDateField;
            }
            set
            {
                this.ExpirationDateField = value;
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
        public System.Nullable<bool> IsAvailableForChange
        {
            get
            {
                return this.IsAvailableForChangeField;
            }
            set
            {
                this.IsAvailableForChangeField = value;
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
        public string MPI
        {
            get
            {
                return this.MPIField;
            }
            set
            {
                this.MPIField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ManagedCareOrganizationID
        {
            get
            {
                return this.ManagedCareOrganizationIDField;
            }
            set
            {
                this.ManagedCareOrganizationIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string McoDescription
        {
            get
            {
                return this.McoDescriptionField;
            }
            set
            {
                this.McoDescriptionField = value;
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
        public string PcpDescription
        {
            get
            {
                return this.PcpDescriptionField;
            }
            set
            {
                this.PcpDescriptionField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PcpNpi
        {
            get
            {
                return this.PcpNpiField;
            }
            set
            {
                this.PcpNpiField = value;
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
        public string PmgDescription
        {
            get
            {
                return this.PmgDescriptionField;
            }
            set
            {
                this.PmgDescriptionField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PmgTaxID
        {
            get
            {
                return this.PmgTaxIDField;
            }
            set
            {
                this.PmgTaxIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Reason
        {
            get
            {
                return this.ReasonField;
            }
            set
            {
                this.ReasonField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> ResultModifierDate
        {
            get
            {
                return this.ResultModifierDateField;
            }
            set
            {
                this.ResultModifierDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SSN
        {
            get
            {
                return this.SSNField;
            }
            set
            {
                this.SSNField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "PcpSpecialty_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.McoConnector")]
    public partial class PcpSpecialty_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string SpecialtyField;

        private string SpecialtyCodeField;

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
        public string Specialty
        {
            get
            {
                return this.SpecialtyField;
            }
            set
            {
                this.SpecialtyField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SpecialtyCode
        {
            get
            {
                return this.SpecialtyCodeField;
            }
            set
            {
                this.SpecialtyCodeField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "EnrollmentHistory_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.McoConnector")]
    public partial class EnrollmentHistory_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string ManagedCareOrganizationField;

        private System.Nullable<int> ManagedCareOrganizationIDField;

        private System.Nullable<System.DateTime> McoEffectiveDateField;

        private string McoModifiedByField;

        private System.Nullable<System.DateTime> McoModifiedDateField;

        private string McoModifiedSourceField;

        private string PMGNameField;

        private string PcpField;

        private System.Nullable<System.DateTime> PcpEffectiveDateField;

        private string PcpModifiedByField;

        private System.Nullable<System.DateTime> PcpModifiedDateField;

        private string PcpModifiedSourceField;

        private string PcpNpiField;

        private System.DateTime PersonMcoPmgPcpHistoryCreateDateField;

        private int PersonMcoPmgPcpHistoryIDField;

        private System.Nullable<System.DateTime> PmgEffectiveDateField;

        private string PmgModifiedByField;

        private System.Nullable<System.DateTime> PmgModifiedDateField;

        private string PmgModifiedSourceField;

        private string PmgTaxIDField;

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
        public string ManagedCareOrganization
        {
            get
            {
                return this.ManagedCareOrganizationField;
            }
            set
            {
                this.ManagedCareOrganizationField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ManagedCareOrganizationID
        {
            get
            {
                return this.ManagedCareOrganizationIDField;
            }
            set
            {
                this.ManagedCareOrganizationIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> McoEffectiveDate
        {
            get
            {
                return this.McoEffectiveDateField;
            }
            set
            {
                this.McoEffectiveDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string McoModifiedBy
        {
            get
            {
                return this.McoModifiedByField;
            }
            set
            {
                this.McoModifiedByField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> McoModifiedDate
        {
            get
            {
                return this.McoModifiedDateField;
            }
            set
            {
                this.McoModifiedDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string McoModifiedSource
        {
            get
            {
                return this.McoModifiedSourceField;
            }
            set
            {
                this.McoModifiedSourceField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PMGName
        {
            get
            {
                return this.PMGNameField;
            }
            set
            {
                this.PMGNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Pcp
        {
            get
            {
                return this.PcpField;
            }
            set
            {
                this.PcpField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> PcpEffectiveDate
        {
            get
            {
                return this.PcpEffectiveDateField;
            }
            set
            {
                this.PcpEffectiveDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PcpModifiedBy
        {
            get
            {
                return this.PcpModifiedByField;
            }
            set
            {
                this.PcpModifiedByField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> PcpModifiedDate
        {
            get
            {
                return this.PcpModifiedDateField;
            }
            set
            {
                this.PcpModifiedDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PcpModifiedSource
        {
            get
            {
                return this.PcpModifiedSourceField;
            }
            set
            {
                this.PcpModifiedSourceField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PcpNpi
        {
            get
            {
                return this.PcpNpiField;
            }
            set
            {
                this.PcpNpiField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime PersonMcoPmgPcpHistoryCreateDate
        {
            get
            {
                return this.PersonMcoPmgPcpHistoryCreateDateField;
            }
            set
            {
                this.PersonMcoPmgPcpHistoryCreateDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PersonMcoPmgPcpHistoryID
        {
            get
            {
                return this.PersonMcoPmgPcpHistoryIDField;
            }
            set
            {
                this.PersonMcoPmgPcpHistoryIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> PmgEffectiveDate
        {
            get
            {
                return this.PmgEffectiveDateField;
            }
            set
            {
                this.PmgEffectiveDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PmgModifiedBy
        {
            get
            {
                return this.PmgModifiedByField;
            }
            set
            {
                this.PmgModifiedByField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> PmgModifiedDate
        {
            get
            {
                return this.PmgModifiedDateField;
            }
            set
            {
                this.PmgModifiedDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PmgModifiedSource
        {
            get
            {
                return this.PmgModifiedSourceField;
            }
            set
            {
                this.PmgModifiedSourceField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PmgTaxID
        {
            get
            {
                return this.PmgTaxIDField;
            }
            set
            {
                this.PmgTaxIDField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Mco_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.McoConnector")]
    public partial class Mco_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private System.Nullable<int> AmountOfLivesEnrolledField;

        private System.Nullable<int> AmountOfLivesPendingField;

        private System.Nullable<decimal> BufferPercentField;

        private System.Nullable<int> CapacityField;

        private System.Nullable<int> CapacityWithBufferField;

        private string CarrierNameField;

        private string CarrierNumberField;

        private string EinField;

        private int ManagedCareOrganizationIDField;

        private string NpiField;

        private System.Nullable<bool> OverCapacityFlagField;

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
        public System.Nullable<int> AmountOfLivesEnrolled
        {
            get
            {
                return this.AmountOfLivesEnrolledField;
            }
            set
            {
                this.AmountOfLivesEnrolledField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> AmountOfLivesPending
        {
            get
            {
                return this.AmountOfLivesPendingField;
            }
            set
            {
                this.AmountOfLivesPendingField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> BufferPercent
        {
            get
            {
                return this.BufferPercentField;
            }
            set
            {
                this.BufferPercentField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Capacity
        {
            get
            {
                return this.CapacityField;
            }
            set
            {
                this.CapacityField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> CapacityWithBuffer
        {
            get
            {
                return this.CapacityWithBufferField;
            }
            set
            {
                this.CapacityWithBufferField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CarrierName
        {
            get
            {
                return this.CarrierNameField;
            }
            set
            {
                this.CarrierNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CarrierNumber
        {
            get
            {
                return this.CarrierNumberField;
            }
            set
            {
                this.CarrierNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Ein
        {
            get
            {
                return this.EinField;
            }
            set
            {
                this.EinField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ManagedCareOrganizationID
        {
            get
            {
                return this.ManagedCareOrganizationIDField;
            }
            set
            {
                this.ManagedCareOrganizationIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Npi
        {
            get
            {
                return this.NpiField;
            }
            set
            {
                this.NpiField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> OverCapacityFlag
        {
            get
            {
                return this.OverCapacityFlagField;
            }
            set
            {
                this.OverCapacityFlagField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SimpleEntity_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.McoConnector")]
    public partial class SimpleEntity_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private int IDField;

        private string LocationField;

        private string NameField;

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
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Location
        {
            get
            {
                return this.LocationField;
            }
            set
            {
                this.LocationField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "PersonMcoDisplay_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.McoConnector")]
    public partial class PersonMcoDisplay_V1 : Mediti2.McoConnector.PersonMco_V1
    {

        private string FirstNameField;

        private string LastName1Field;

        private string LastName2Field;

        private string McoDescriptionField;

        private string MiddleNameField;

        private string MpiField;

        private string PcpDescriptionField;

        private string PmgDescriptionField;

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
        public string McoDescription
        {
            get
            {
                return this.McoDescriptionField;
            }
            set
            {
                this.McoDescriptionField = value;
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
        public string Mpi
        {
            get
            {
                return this.MpiField;
            }
            set
            {
                this.MpiField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PcpDescription
        {
            get
            {
                return this.PcpDescriptionField;
            }
            set
            {
                this.PcpDescriptionField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PmgDescription
        {
            get
            {
                return this.PmgDescriptionField;
            }
            set
            {
                this.PmgDescriptionField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "PersonMco_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.McoConnector")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Mediti2.McoConnector.PersonMcoDisplay_V1))]
    public partial class PersonMco_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string CarrierNumberField;

        private System.Nullable<int> McoIDField;

        private string PcpNpiField;

        private int PersonIDField;

        private string PmgTaxIDField;

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
        public string CarrierNumber
        {
            get
            {
                return this.CarrierNumberField;
            }
            set
            {
                this.CarrierNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> McoID
        {
            get
            {
                return this.McoIDField;
            }
            set
            {
                this.McoIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PcpNpi
        {
            get
            {
                return this.PcpNpiField;
            }
            set
            {
                this.PcpNpiField = value;
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
        public string PmgTaxID
        {
            get
            {
                return this.PmgTaxIDField;
            }
            set
            {
                this.PmgTaxIDField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "DataSource_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.McoConnector")]
    public enum DataSource_V1 : byte
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Medicaid = 10,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Counselor = 20,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Web = 30,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Ases = 100,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "PcpPmg_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.McoConnector")]
    public partial class PcpPmg_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private System.Nullable<bool> ActiveField;

        private string CarrierIDField;

        private string CarrierNameField;

        private string FullNameField;

        private string LicenseNumberField;

        private System.Nullable<int> ManagedCareOrganizationIDField;

        private string NPIField;

        private System.Nullable<bool> OverCapacityFlagField;

        private string PMGField;

        private string PMGNameField;

        private string PMGTaxIDField;

        private int PcpPmgProviderIDField;

        private System.Nullable<int> ProviderCapacityField;

        private string ProviderTypeField;

        private string SpecialtyField;

        private string SpecialtyCodeField;

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
        public System.Nullable<bool> Active
        {
            get
            {
                return this.ActiveField;
            }
            set
            {
                this.ActiveField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CarrierID
        {
            get
            {
                return this.CarrierIDField;
            }
            set
            {
                this.CarrierIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CarrierName
        {
            get
            {
                return this.CarrierNameField;
            }
            set
            {
                this.CarrierNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FullName
        {
            get
            {
                return this.FullNameField;
            }
            set
            {
                this.FullNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LicenseNumber
        {
            get
            {
                return this.LicenseNumberField;
            }
            set
            {
                this.LicenseNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ManagedCareOrganizationID
        {
            get
            {
                return this.ManagedCareOrganizationIDField;
            }
            set
            {
                this.ManagedCareOrganizationIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NPI
        {
            get
            {
                return this.NPIField;
            }
            set
            {
                this.NPIField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> OverCapacityFlag
        {
            get
            {
                return this.OverCapacityFlagField;
            }
            set
            {
                this.OverCapacityFlagField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PMG
        {
            get
            {
                return this.PMGField;
            }
            set
            {
                this.PMGField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PMGName
        {
            get
            {
                return this.PMGNameField;
            }
            set
            {
                this.PMGNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PMGTaxID
        {
            get
            {
                return this.PMGTaxIDField;
            }
            set
            {
                this.PMGTaxIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PcpPmgProviderID
        {
            get
            {
                return this.PcpPmgProviderIDField;
            }
            set
            {
                this.PcpPmgProviderIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ProviderCapacity
        {
            get
            {
                return this.ProviderCapacityField;
            }
            set
            {
                this.ProviderCapacityField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProviderType
        {
            get
            {
                return this.ProviderTypeField;
            }
            set
            {
                this.ProviderTypeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Specialty
        {
            get
            {
                return this.SpecialtyField;
            }
            set
            {
                this.SpecialtyField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SpecialtyCode
        {
            get
            {
                return this.SpecialtyCodeField;
            }
            set
            {
                this.SpecialtyCodeField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Pmg_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.McoConnector")]
    public partial class Pmg_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string CarrierIDField;

        private string PMGField;

        private string PMGNameField;

        private string PMGTaxIDField;

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
        public string CarrierID
        {
            get
            {
                return this.CarrierIDField;
            }
            set
            {
                this.CarrierIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PMG
        {
            get
            {
                return this.PMGField;
            }
            set
            {
                this.PMGField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PMGName
        {
            get
            {
                return this.PMGNameField;
            }
            set
            {
                this.PMGNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PMGTaxID
        {
            get
            {
                return this.PMGTaxIDField;
            }
            set
            {
                this.PMGTaxIDField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SimpleCapacity_V1", Namespace = "http://schemas.datacontract.org/2004/07/Mediti2.McoConnector")]
    public partial class SimpleCapacity_V1 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private System.Nullable<int> AmountOfLivesEnrolledField;

        private System.Nullable<bool> OverCapacityField;

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
        public System.Nullable<int> AmountOfLivesEnrolled
        {
            get
            {
                return this.AmountOfLivesEnrolledField;
            }
            set
            {
                this.AmountOfLivesEnrolledField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> OverCapacity
        {
            get
            {
                return this.OverCapacityField;
            }
            set
            {
                this.OverCapacityField = value;
            }
        }
    }
}