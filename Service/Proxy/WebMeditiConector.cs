﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IWebMeditiConector_V1")]
public interface IWebMeditiConector_V1
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebMeditiConector_V1/FindPerson", ReplyAction="http://tempuri.org/IWebMeditiConector_V1/FindPersonResponse")]
    int[] FindPerson(string firstName, string lastName1, string ssnLast4, System.DateTime birthDate);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebMeditiConector_V1/IsSecurityAnswerCorrect", ReplyAction="http://tempuri.org/IWebMeditiConector_V1/IsSecurityAnswerCorrectResponse")]
    bool IsSecurityAnswerCorrect(int personID, int securityQuestionId, string answer);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebMeditiConector_V1/GetDemographicData", ReplyAction="http://tempuri.org/IWebMeditiConector_V1/GetDemographicDataResponse")]
    Mediti2.WebConnector.WebDemographicData_V1 GetDemographicData(int personID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebMeditiConector_V1/SubmitWebApplication", ReplyAction="http://tempuri.org/IWebMeditiConector_V1/SubmitWebApplicationResponse")]
    Mediti2.WebConnector.WebApplicationResponse_V1 SubmitWebApplication(int webApplicationID, Mediti2.WebConnector.WebApplication_V1 application, Mediti2.WebConnector.CalculationType_V1 calcType);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebMeditiConector_V1/GetApplicationStatus", ReplyAction="http://tempuri.org/IWebMeditiConector_V1/GetApplicationStatusResponse")]
    Mediti2.WebConnector.WebApplicationStatus_V1 GetApplicationStatus(int webApplicationID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebMeditiConector_V1/GetEligibilities", ReplyAction="http://tempuri.org/IWebMeditiConector_V1/GetEligibilitiesResponse")]
    Mediti2.WebConnector.WebPersonEligibility_V1[] GetEligibilities(int webApplicationID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebMeditiConector_V1/GetMultipleApplicationsStatus", ReplyAction="http://tempuri.org/IWebMeditiConector_V1/GetMultipleApplicationsStatusResponse")]
    Mediti2.WebConnector.WebApplicationStatusPair_V1[] GetMultipleApplicationsStatus(int[] webApplicationID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebMeditiConector_V1/GetApplicationResultPdf", ReplyAction="http://tempuri.org/IWebMeditiConector_V1/GetApplicationResultPdfResponse")]
    byte[] GetApplicationResultPdf(int webApplicationID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebMeditiConector_V1/GetMediti2Applications", ReplyAction="http://tempuri.org/IWebMeditiConector_V1/GetMediti2ApplicationsResponse")]
    Mediti2.WebConnector.WebMediti2ApplicationInfo_V1[] GetMediti2Applications(int[] personIDs);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebMeditiConector_V1/CheckPerson", ReplyAction="http://tempuri.org/IWebMeditiConector_V1/CheckPersonResponse")]
    bool CheckPerson(string ssn, string mpi, System.Nullable<int> personID);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IWebMeditiConector_V1Channel : IWebMeditiConector_V1, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class WebMeditiConector_V1Client : System.ServiceModel.ClientBase<IWebMeditiConector_V1>, IWebMeditiConector_V1
{
    
    public WebMeditiConector_V1Client()
    {
    }
    
    public WebMeditiConector_V1Client(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public WebMeditiConector_V1Client(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public WebMeditiConector_V1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public WebMeditiConector_V1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public int[] FindPerson(string firstName, string lastName1, string ssnLast4, System.DateTime birthDate)
    {
        return base.Channel.FindPerson(firstName, lastName1, ssnLast4, birthDate);
    }
    
    public bool IsSecurityAnswerCorrect(int personID, int securityQuestionId, string answer)
    {
        return base.Channel.IsSecurityAnswerCorrect(personID, securityQuestionId, answer);
    }
    
    public Mediti2.WebConnector.WebDemographicData_V1 GetDemographicData(int personID)
    {
        return base.Channel.GetDemographicData(personID);
    }
    
    public Mediti2.WebConnector.WebApplicationResponse_V1 SubmitWebApplication(int webApplicationID, Mediti2.WebConnector.WebApplication_V1 application, Mediti2.WebConnector.CalculationType_V1 calcType)
    {
        return base.Channel.SubmitWebApplication(webApplicationID, application, calcType);
    }
    
    public Mediti2.WebConnector.WebApplicationStatus_V1 GetApplicationStatus(int webApplicationID)
    {
        return base.Channel.GetApplicationStatus(webApplicationID);
    }
    
    public Mediti2.WebConnector.WebPersonEligibility_V1[] GetEligibilities(int webApplicationID)
    {
        return base.Channel.GetEligibilities(webApplicationID);
    }
    
    public Mediti2.WebConnector.WebApplicationStatusPair_V1[] GetMultipleApplicationsStatus(int[] webApplicationID)
    {
        return base.Channel.GetMultipleApplicationsStatus(webApplicationID);
    }
    
    public byte[] GetApplicationResultPdf(int webApplicationID)
    {
        return base.Channel.GetApplicationResultPdf(webApplicationID);
    }
    
    public Mediti2.WebConnector.WebMediti2ApplicationInfo_V1[] GetMediti2Applications(int[] personIDs)
    {
        return base.Channel.GetMediti2Applications(personIDs);
    }
    
    public bool CheckPerson(string ssn, string mpi, System.Nullable<int> personID)
    {
        return base.Channel.CheckPerson(ssn, mpi, personID);
    }
}