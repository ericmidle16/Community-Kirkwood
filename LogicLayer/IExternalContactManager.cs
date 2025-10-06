/// <summary>
/// Creator:  Jacob McPherson
/// Created:  2025/02/18
/// Summary:  The manager interface for viewing external contacts
/// Last Updated By: Jacob McPherson
/// Last Updated: 2025/03/25
/// What was Changed: Merge
/// </summary>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IExternalContactManager
    {
        public List<ExternalContact> ViewAllExternalContacts();
        public ExternalContactVM ViewSingleExternalContact(int externalContactID);
        public List<string> GetAllExternalContactTypes();
        public bool AddExternalContact(ExternalContact externalContact);
        public bool AddExternalContactType(string name, string description);
        public bool EditExternalContact(int currentUserId, ExternalContact externalContact, ExternalContact externalContact_old);
        public bool DeactivateExternalContact(int contactId);
    }
}
