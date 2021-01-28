using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;

namespace LocalKabaRest.Dals
{
    public class PrvDals
    {
        private EF.LocalServicesDBEntities dbEnt;
        private DbConnection con;
        private DbTransaction trans;




        public Models.NetworkStructs.GetPrvCaseDetailsResponse GetPrvCaseDetails(
            int externalInterfaceKey,
            double longitude,
            double latitude,
            int radius
            )
        {                   

            OpenConnection();
            Models.NetworkStructs.GetPrvCaseDetailsResponse response = new Models.NetworkStructs.GetPrvCaseDetailsResponse();
            response.ExternalInterfaceKey = externalInterfaceKey;
            response.prvCases = new List<Models.PreventionCase>();
            Models.PreventionCase prvCase;

            try
            {
                SpCommand spCom = new SpCommand(con, "[dbo].[GetPrvCaseDetails]");

                spCom.AddParameter(DbType.String, "@inLongitude", longitude);
                spCom.AddParameter(DbType.String, "@inLatitude", latitude);
                spCom.AddParameter(DbType.String, "@inRadius", radius);

                DbDataReader reader = spCom.Execute();

                if (!reader.HasRows)
                {
                    response.Status = General.Constants.REQUEST_STATUS_ERROR;
                    response.ErrorMsg = "לא קיימים תיקי מניעה ברדיוס שהתקבל";
                }
                else
                {
                    while (reader.Read())
                    {
                        prvCase = new Models.PreventionCase();

                        prvCase.PreventionCaseId = reader.GetInt32(0);
                        prvCase.CaseName = reader.GetString(1);
                        prvCase.CaseType = reader.GetString(2);
                        prvCase.AirDistanceFromPoint = reader.IsDBNull(1) ? 0 : reader.GetDouble(3);
                        prvCase.CaseAddress = reader.GetString(4);
                        prvCase.DialerAuthorized = reader.GetBoolean(5);
                        prvCase.contacts = GetPrvCaseContacts(prvCase.PreventionCaseId);
                        prvCase.operations = GetPrvOperations(prvCase.PreventionCaseId);
                        response.prvCases.Add(prvCase);
                    }
                }
            }
            catch (Exception ex)
            {               
                General.TraceUtility tu = new General.TraceUtility("TraceLog");
                tu.TraceError(0, "GetPrvCaseDetailsResponse : " + ex.ToString());
                response.Status = General.Constants.REQUEST_STATUS_ERROR;
                response.ErrorMsg = "ConnectionError";
                response.prvCases = new List<Models.PreventionCase>();
            }
          
            CloseConnection();

            response.InterfaceKey = SetInterfaceIn(response.Status,
                response.ErrorMsg,
                "ExternalInterfaceKey: " + externalInterfaceKey.ToString() + " " +
                "Longitude: " + longitude.ToString() + " " +
                "Latitude: " + latitude.ToString() + " " +
                "Radius: " + radius.ToString()
                ); 

            return response;
        }

        private int SetInterfaceIn(int status,
              string errorMsg,
              string requestData
              )
        {
            int id = 0;

            OpenConnection();          

            try
            {
                SpCommand spCom = new SpCommand(con, "[dbo].[SetInterfaceIn]");

                spCom.AddParameter(DbType.String, "@inRequestData", requestData);
                spCom.AddParameter(DbType.String, "@inErrorMsg", errorMsg);
                spCom.AddParameter(DbType.Int32, "@inStatusId", status);

                DbDataReader reader = spCom.Execute();

             
                    while (reader.Read())
                    {
                    id = reader.GetInt32(0);
                    }
                
            }
            catch (Exception ex)
            {
                General.TraceUtility tu = new General.TraceUtility("TraceLog");
                tu.TraceError(0, "SetInterfaceIn : " + ex.ToString());
          
            }

            CloseConnection();

            return id;
        }

        public List<Models.CaseContact> GetPrvCaseContacts(
           int prvCaseId
           )
        {
            OpenConnection();
            List<Models.CaseContact> contacts = new List<Models.CaseContact>();
            Models.CaseContact contact;
            try
            {
                SpCommand spCom = new SpCommand(con, "[dbo].[GetPrvContacts]");

                spCom.AddParameter(DbType.Int32, "@inPrvId", prvCaseId);
                
                DbDataReader reader = spCom.Execute();

               
                    while (reader.Read())
                    {
                    contact = new Models.CaseContact();
                    contact.ContactFullName = reader.GetString(0);
                    contact.Contact = reader.GetString(1);
                    contact.ContactPhone1 = reader.GetString(2);
                    contact.ContactPhone2 = reader.GetString(3);
                    contact.ContactPhone3 = reader.GetString(4);
                    contact.ContactUpdatedDate = reader.GetDateTime(5);
                    contacts.Add(contact);
                    }                
            }
            catch (Exception ex)
            {
                General.TraceUtility tu = new General.TraceUtility("TraceLog");
                tu.TraceError(0, "GetPrvCaseDetailsResponse : " + ex.ToString());             
            }

            CloseConnection();


            return contacts;
        }

        public List<Models.OperationInfo> GetPrvOperations(
                  int prvCaseId
                  )
        {
            OpenConnection();
            List<Models.OperationInfo> operations = new List<Models.OperationInfo>();
            Models.OperationInfo operation;
            try
            {
            SpCommand spCom = new SpCommand(con, "[dbo].[GetPrvOperationalInfo]");

            spCom.AddParameter(DbType.String, "@inPrvId", prvCaseId);

            DbDataReader reader = spCom.Execute();

            while (reader.Read())
                {
                    operation = new Models.OperationInfo();
                    operation.OperationLabel = reader.GetString(0);
                    operation.OperationInfoDesc = reader.GetString(1);
                    operation.OrderNum = reader.GetInt32(2);
                    operations.Add(operation);
                }
            
            }
            catch (Exception ex)
            {
                General.TraceUtility tu = new General.TraceUtility("TraceLog");
                tu.TraceError(0, "GetPrvCaseDetailsResponse : " + ex.ToString());
            }

            CloseConnection();


            return operations;
        }


        private void OpenConnection()
        {
            try
            {
                dbEnt = new EF.LocalServicesDBEntities();
                con = dbEnt.Database.Connection;
                //   using (var connection = dbEnt.Database.Connection)
                // {
                if (con != null && con.State == ConnectionState.Closed)
                    con.Open();
            }
            catch (Exception ex)
            {
                General.TraceUtility tu = new General.TraceUtility("TraceLog");
                tu.TraceError(0, "OpenConnection : " + ex.ToString());
            }
        }

        private void StartTransaction()
        {
            trans = con.BeginTransaction();
        }

        private void CommitTransaction()
        {
            trans.Commit();
        }

        private void RollbackTransaction()
        {
            trans.Rollback();
        }

        private void CloseConnection(bool bTransaction = false, bool bTransSuccess = false)
        {
            if (con != null && con.State == ConnectionState.Open)
                con.Close();
        }
    }
}




