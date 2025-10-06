/// <summary>
/// Creator:  Jacob McPherson
/// Created:  2025/02/18
/// Summary:  The interface for viewing external contacts
/// Last Updated By: Jacob McPherson
/// Last Updated: 2025/03/25
/// What was Changed: 	Merge
/// </summary>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IExternalContactAccessor
    {
        public List<ExternalContact> SelectAllExternalContacts();
        public ExternalContactVM SelectSingleExternalContact(int externalContactID);
        public List<string> SelectAllExternalContactTypes();
        public bool InsertExternalContact(ExternalContact externalContact);
        public bool InsertExternalContactType(string name, string description);
        public bool UpdateExternalContact(int currentUserId, ExternalContact externalContact, ExternalContact externalContact_old);
        public bool DeactivateExternalContact(int contactId);
    }
}
