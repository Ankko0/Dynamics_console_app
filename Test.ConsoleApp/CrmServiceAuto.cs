using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Linq;
using Dynamics.ConsoleApp.Entities;

namespace Test.ConsoleApp
{
    class CrmServiceAuto
    {
        public void AddCommunication(IOrganizationService service,  EntityCollection contactsWithoutComunication)
        {
            foreach (var contact in contactsWithoutComunication.Entities.Select(e=>e.ToEntity<Contact>()))
            {
                if (contact.Telephone1 != null  && contact.EMailAddress1 == null )
                {
                    var newCommunicationId =  service.Create(new nav_communication());
                    var newCommunication = service.Retrieve(nav_communication.EntityLogicalName, newCommunicationId, new ColumnSet(true)).ToEntity<nav_communication>();
                    newCommunication.nav_phone = contact.Telephone1;
                    newCommunication.nav_name = contact.FullName;
                    newCommunication.nav_main = true;
                    newCommunication.nav_type = nav_communication_nav_type.Telefon;
                    newCommunication.nav_contactid.Id = contact.Id;

                    service.Update(newCommunication);

                }

                if (contact.Telephone1 == null && contact.EMailAddress1 != null)
                {
                    var newCommunicationId = service.Create(new nav_communication());
                    var newCommunication = service.Retrieve(nav_communication.EntityLogicalName, newCommunicationId, new ColumnSet(true)).ToEntity<nav_communication>();
                    newCommunication.nav_email = contact.EMailAddress1;
                    newCommunication.nav_name = contact.FullName;
                    newCommunication.nav_main = false;
                    newCommunication.nav_type = nav_communication_nav_type.E_mail;
                    newCommunication.nav_contactid.Id = contact.Id;

                    service.Update(newCommunication);

                }

                if (contact.Telephone1 != null && contact.EMailAddress1 != null)
                {
                    var newCommunicationId = service.Create(new nav_communication());
                    var newCommunication = service.Retrieve(nav_communication.EntityLogicalName, newCommunicationId, new ColumnSet(true)).ToEntity<nav_communication>();
                    newCommunication.nav_phone = contact.Telephone1;
                    newCommunication.nav_name = contact.FullName + " Phone";
                    newCommunication.nav_main = true;
                    newCommunication.nav_type = nav_communication_nav_type.Telefon;
                    newCommunication.nav_contactid.Id = contact.Id;

                    service.Update(newCommunication);

                    newCommunicationId = service.Create(new nav_communication());
                    newCommunication = service.Retrieve(nav_communication.EntityLogicalName, newCommunicationId, new ColumnSet(true)).ToEntity<nav_communication>();
                    newCommunication.nav_email = contact.EMailAddress1;
                    newCommunication.nav_name = contact.FullName + "E-mail";
                    newCommunication.nav_main = false;
                    newCommunication.nav_type = nav_communication_nav_type.E_mail;
                    newCommunication.nav_contactid.Id = contact.Id;

                    service.Update(newCommunication);

                }
            }
        }
    }
}
