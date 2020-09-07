using Mediti2.McoConnector;
using System;
using System.Collections.Generic;

namespace MeditiWebApp.Proxy
{
    public class McoServiceClient : IMcoConnector_V1
    {
        public IList<PersonMcoDisplay_V1> GetApplicationPeople(string applicationNumber)
        {
            List<PersonMcoDisplay_V1> result = new List<PersonMcoDisplay_V1>();
            if (!string.IsNullOrEmpty(applicationNumber))
            {
                if(applicationNumber == "123456")
                {
                    result.Add(new PersonMcoDisplay_V1()
                    {
                        FirstName = "Demo 1",
                        LastName1 = "Demo 1",
                        LastName2 = "Demo 1",
                        McoDescription = "Mco 1",
                        McoID = 1,
                        MiddleName = "Demo 1",
                        Mpi = "123456",
                        PcpDescription = "Pcp 1",
                        PcpID = 1,
                        PersonID = 1,
                        PmgDescription = "Pmg 1",
                        PmgID = 1
                    });
                }
            }            
            return result;
        }

        public AppMember_V1 GetApplicationPeopleByMpi(string mpi)
        {
            AppMember_V1 result = null;
            if (!string.IsNullOrEmpty(mpi))
            {
                if (mpi.Equals("123456"))
                {
                    result = new AppMember_V1()
                    {
                        FirstName = "Demo 1",
                        LastName1 = "Demo 1",
                        LastName2 = "Demo 1",
                        MemberBirthDate = DateTime.Today,
                        MiddleName = "Demo 1",
                        Mpi = "123456",
                        PersonID = 1,
                        SsnLast4 = "1234"
                    };
                    result.Applications.Add(new MemberAppInfo_V1()
                    {
                        ApplicationID = 1,
                        ApplicationNumber = "123456",
                        ContactFullName = "Contacto Demo 1",
                        ContactID = 1,
                        CountOfMembers = 1,
                        CreationDate = DateTime.Today
                    });                        
                }
            }
            return result;
        }

        public AppMember_V1 GetApplicationPeopleByName(string firstName, string lastName1, string lastName2, string ssnLast4, DateTime dateOfBirth)
        {
            AppMember_V1 result = null;            
            if (ssnLast4.Equals("1234") || dateOfBirth.Date.Equals(DateTime.Today) || firstName.Contains("Demo") || lastName1.Contains("Demo") || lastName2.Contains("Demo"))
            {
                 result = new AppMember_V1()
                 {
                    FirstName = "Demo 1",
                    LastName1 = "Demo 1",
                    LastName2 = "Demo 1",
                    MemberBirthDate = DateTime.Today,
                    MiddleName = "Demo 1",
                    Mpi = "123456",
                    PersonID = 1,
                    SsnLast4 = "1234"
                };
                result.Applications.Add(new MemberAppInfo_V1()
                {
                    ApplicationID = 1,
                    ApplicationNumber = "123456",
                    ContactFullName = "Contacto Demo 1",
                    ContactID = 1,
                    CountOfMembers = 1,
                    CreationDate = DateTime.Now
                });
            }            
            return result;
        }

        internal void Close()
        {
            
        }

        public AppMember_V1 GetApplicationPeopleByPersonID(int personID)
        {
            AppMember_V1 result = null;
            if (personID.Equals(1))
            {
                result = new AppMember_V1()
                {
                    FirstName = "Demo 1",
                    LastName1 = "Demo 1",
                    LastName2 = "Demo 1",
                    MemberBirthDate = DateTime.Today,
                    MiddleName = "Demo 1",
                    Mpi = "123456",
                    PersonID = 1,
                    SsnLast4 = "1234"
                };
                result.Applications.Add(new MemberAppInfo_V1()
                {
                    ApplicationID = 1,
                    ApplicationNumber = "123456",
                    ContactFullName = "Contacto Demo 1",
                    ContactID = 1,
                    CountOfMembers = 1,
                    CreationDate = DateTime.Now
                });
            }
            return result;
        }

        public IList<SimpleEntity_V1> GetCities()
        {
            List<SimpleEntity_V1> result = new List<SimpleEntity_V1>();
            result.Add(new SimpleEntity_V1()
            {
                ID = 1,
                Name = "City 1"
            });
            result.Add(new SimpleEntity_V1()
            {
                ID = 2,
                Name = "City 2"
            });
            result.Add(new SimpleEntity_V1()
            {
                ID = 3,
                Name = "City 3"
            });
            result.Add(new SimpleEntity_V1()
            {
                ID = 4,
                Name = "City 4"
            });
            result.Add(new SimpleEntity_V1()
            {
                ID = 5,
                Name = "City 5"
            });
            return result;
        }

        public IList<EnrollmentHistory_V1> GetEnrollmentChangeHistory(int personID)
        {
            List<EnrollmentHistory_V1> result = null;
            if (personID.Equals(1))
            {
                result.Add(new EnrollmentHistory_V1()
                {
                   ManagedCareOrganizationID = 1,
                   McoEffectiveDate = DateTime.Now,
                   McoModifiedBy = "Admin",
                   McoModifiedDate = DateTime.Now,
                   McoModifiedSource = "Web",
                   PcpEffectiveDate = DateTime.Now,
                   PcpModifiedBy = "Admin",
                   PcpModifiedDate = DateTime.Now,
                   PcpModifiedSource = "Web",
                   PersonMcoPmgPcpHistoryCreateDate = DateTime.Now,
                   PersonMcoPmgPcpHistoryID = 1,
                   PmgEffectiveDate = DateTime.Now,
                   PmgModifiedBy = "Admin",
                   PmgModifiedDate = DateTime.Now,
                   PmgModifiedSource = "Web",
                   PrimaryCarePhysicianID = 1,
                   PrimaryMedicalGroupID = 1
                });                
            }
            return result;
        }

        public IList<SimpleEntity_V1> GetMcoPmgs(int mcoID)
        {
            List<SimpleEntity_V1> result = new List<SimpleEntity_V1>();
            if(mcoID == 1)
            {
                result.Add(new SimpleEntity_V1()
                {

                });
            }
            return result;
        }

        public IList<Mco_V1> GetMcos()
        {
            throw new NotImplementedException();
        }

        public IList<SimpleEntity_V1> GetMcosWithCapacity()
        {
            throw new NotImplementedException();
        }

        public AppMembers_V1 GetMediti2AppMembers(int applicationID)
        {
            throw new NotImplementedException();
        }

        public IList<PcpWithFilter_V1> GetPcpWithFilters(int? managedCareOrganizationID, int? primaryMedicalGroupID, int? primaryCarePhysicianID, int? cityID, int? specialtyID, string npi)
        {
            throw new NotImplementedException();
        }

        public IList<PersonMcoDisplay_V1> GetPeople(string firstName, string lastName1, string lastName2, string ssnLast4, DateTime dateOfBirth)
        {
            throw new NotImplementedException();
        }

        public PersonMcoDisplay_V1 GetPerson(string mpi)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicationPerson_V1> GetPersonApplications(string mpi, int? personID, string firstName, string lastName1, string lastName2, string ssnLast4, DateTime? dateOfBirth)
        {
            throw new NotImplementedException();
        }

        public IList<SimpleEntity_V1> GetPmgPcps(int pmgID)
        {
            throw new NotImplementedException();
        }

        public IList<SimpleEntity_V1> GetSpecialties()
        {
            throw new NotImplementedException();
        }

        public PersonMcoDisplay_V1 SetPersonMco(PersonMco_V1 value, string user, DataSource source)
        {
            throw new NotImplementedException();
        }

        public PersonMcoDisplay_V1 SetRandomMco(int personID, string user, DataSource source)
        {
            throw new NotImplementedException();
        }
    }
}