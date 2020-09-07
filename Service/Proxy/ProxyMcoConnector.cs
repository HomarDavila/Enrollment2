using Common;
using Mediti2.McoConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeditiWebApp.Proxy
{
    public class ProxyMCOConnector : IDisposable
    {
        McoConnector_V1Client McoConnector_V1Client = null;
        
        public void Dispose()
        {
            McoConnector_V1Client = null;
        }

        public AppPersonSearchResult_V1 GetApplicationPeopleByMpi(string mpi)
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = McoConnector_V1Client.GetApplicationPeopleByMpi(mpi);
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }
        
        public AppPersonSearchResult_V1 GetApplicationPeopleByPersonID(int personID)
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = McoConnector_V1Client.GetApplicationPeopleByPersonID(personID);
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }

        public AppMemberInfo_V1 GetMediti2AppMembers(int applicationMemberId)
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = McoConnector_V1Client.GetMediti2AppMembers(applicationMemberId);
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }

        public AppPersonSearchResult_V1 GetApplicationPeopleByName(string firstName, string lastName1, string lastName2, string ssnLast4, DateTime dateOfBirth)
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = McoConnector_V1Client.GetApplicationPeopleByName(firstName, lastName1, lastName2, ssnLast4, dateOfBirth);
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }

        public IList<AppPerson_V1> GetPeople(string mpi, System.Nullable<int> personID, string firstName, string lastName1, string lastName2, string ssnLast4, System.Nullable<System.DateTime> dateOfBirth)
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = McoConnector_V1Client.GetPeople2(mpi, personID, firstName, lastName1, lastName2, ssnLast4, dateOfBirth);
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }

        public IList<PcpSpecialty_V1> GetSpecialties()
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = McoConnector_V1Client.GetSpecialties();
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }

        public IList<EnrollmentHistory_V1> GetEnrollmentChangeHistory(int personID)
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = McoConnector_V1Client.GetEnrollmentChangeHistory(personID);
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }

        public IList<Mco_V1> GetMcos()
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = McoConnector_V1Client.GetMcos();
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }
                
        public IList<SimpleEntity_V1> GetMcosWithCapacity()
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = McoConnector_V1Client.GetMcosWithCapacity();
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }

        public IList<PersonMcoDisplay_V1> GetApplicationPeople(string applicationNumber)
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = McoConnector_V1Client.GetApplicationPeople(applicationNumber);
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }

        public PersonMcoDisplay_V1 GetPerson(string mpi)
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = McoConnector_V1Client.GetPerson(mpi);
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }
        
        public PersonMcoDisplay_V1 SetPersonMco(PersonMco_V1 value, string user, DataSource_V1 source)
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = McoConnector_V1Client.SetPersonMco(value, user, source);
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }

        public PersonMcoDisplay_V1 SetRandomMco(int personID, string user, DataSource_V1 source)
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = McoConnector_V1Client.SetRandomMco(personID, user, source);
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }
        }
                
        public IList<PcpPmg_V1> GetPCPs()
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = McoConnector_V1Client.GetPrimaryCarePhysicians();
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }

        public IList<Pmg_V1> GetPMGs()
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = McoConnector_V1Client.GetPMGs();
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }
        
        public IList<PcpPmg_V1> GetPcpWithFilters(System.Nullable<int> managedCareOrganizationID, string carrierID, string pcpFullName, string npi, string specialtyCode, string pmgTaxID)
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = McoConnector_V1Client.GetPcpWithFilters(managedCareOrganizationID, carrierID, pcpFullName, npi, specialtyCode, pmgTaxID);
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }


        public bool IsOverCapacityPCP(string Npi)
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = false;
                var responseT = McoConnector_V1Client.GetPcpCapacityByNpi(Npi);
                if (responseT != null)
                    if(responseT.OverCapacity.HasValue)
                        response = responseT.OverCapacity.Value;
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }

        public bool IsOverCapacityMCO(int McoId)
        {
            McoConnector_V1Client = new McoConnector_V1Client();
            try
            {
                var response = false;
                var responseT = McoConnector_V1Client.GetMcoCapacityById(McoId);
                if (responseT != null)
                    if (responseT.OverCapacity.HasValue)
                        response = responseT.OverCapacity.Value;
                McoConnector_V1Client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                McoConnector_V1Client = null;
            }

        }


    }
}