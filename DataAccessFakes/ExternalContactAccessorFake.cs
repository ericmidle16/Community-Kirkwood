/// <summary>
/// Creator:  Jacob McPherson
/// Created:  2025/02/18
/// Summary:  Fake data for viewing external contacts
/// Last Updated By: Jacob McPherson
/// Last Updated:  2025/03/25
/// What was Changed: 	Merge
/// </summary>

using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class ExternalContactAccessorFake : IExternalContactAccessor
    {
        List<ExternalContact> _externalContacts;
        List<ExternalContactVM> _externalContactViewModel;
        private List<string> _contactTypes;

        public ExternalContactAccessorFake()
        {
            _externalContacts = new List<ExternalContact>();
            _externalContacts.Add(new ExternalContact()
            {
                ExternalContactID = 1,
                ExternalContactTypeID = "Tester",
                ContactName = "Test1",
                AddedBy = 1,
                LastUpdatedBy = 1,
                Active = true
            });
            _externalContacts.Add(new ExternalContact()
            {
                ExternalContactID = 2,
                ExternalContactTypeID = "Tester",
                ContactName = "Test2",
                AddedBy = 1,
                LastUpdatedBy = 1,
                Active = true
            });
            _externalContacts.Add(new ExternalContact()
            {
                ExternalContactID = 3,
                ExternalContactTypeID = "Tester",
                ContactName = "Test3",
                AddedBy = 1,
                LastUpdatedBy = 1,
                Active = false
            });

            _externalContactViewModel = new List<ExternalContactVM>();
            _externalContactViewModel.Add(new ExternalContactVM()
            {
                ExternalContactID = 1,
                ExternalContactTypeID = "Tester",
                ContactName = "Test1",
                AddedBy = 1,
                LastUpdatedBy = 1,
                Active = true,
                TypeDescription = "This is a description for the contact type id"
            });
            _externalContactViewModel.Add(new ExternalContactVM()
            {
                ExternalContactID = 2,
                ExternalContactTypeID = "Tester",
                ContactName = "Test2",
                AddedBy = 1,
                LastUpdatedBy = 1,
                Active = true,
                TypeDescription = "This is a description for the contact type id"
            });
            _externalContactViewModel.Add(new ExternalContactVM()
            {
                ExternalContactID = 3,
                ExternalContactTypeID = "Tester",
                ContactName = "Test3",
                AddedBy = 1,
                LastUpdatedBy = 1,
                Active = false,
                TypeDescription = "This is a description for the contact type id"
            });

            _contactTypes = new List<string>();

            _externalContacts.Add(new ExternalContact()
            {
                ExternalContactID = 100001,
                ExternalContactTypeID = "1",
                ContactName = "AA",
                GivenName = "AAA",
                FamilyName = "AAAA",
                Email = "AAA@AAA@A",
                PhoneNumber = "1234567890",
                JobTitle = "11",
                Address = "111",
                Description = "1111",
                AddedBy = 1,
                LastUpdatedBy = 11,
                Active = true
            });

            _externalContacts.Add(new ExternalContact()
            {
                ExternalContactID = 100002,
                ExternalContactTypeID = "2",
                ContactName = "BB",
                GivenName = "BBB",
                FamilyName = "BBBB",
                Email = "BBB@BBB@B",
                PhoneNumber = "0987654321",
                JobTitle = "22",
                Address = "222",
                Description = "2222",
                AddedBy = 2,
                LastUpdatedBy = 2,
                Active = true
            });

            _contactTypes.Add("Type1");
            _contactTypes.Add("Type2");
            _contactTypes.Add("Type3");

        }
        public List<ExternalContact> SelectAllExternalContacts()
        {
            List<ExternalContact> externalContacts = new List<ExternalContact>();

            foreach(ExternalContact externalContact in _externalContacts)
            {
                if(externalContact.Active)
                {
                    externalContacts.Add(externalContact);
                }
            }

            return externalContacts;
        }

        public ExternalContactVM SelectSingleExternalContact(int externalContactID)
        {
            ExternalContactVM externalContact = null;
            foreach(ExternalContactVM e in _externalContactViewModel)
            {
                if(e.ExternalContactID == externalContactID && e.Active)
                {
                    externalContact = e;
                }
            }
            return externalContact;
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/03/04
        /// 
        /// Updates Fake ExternalContact
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <param name="currentUserId">Id of Current User</param> 
        /// <param name="externalContact">New Contact</param>
        /// <param name="externalContact_old">Old Contact</param>
        public bool UpdateExternalContact(int currentUserId, ExternalContact externalContact, ExternalContact externalContact_old)
        {
            for (int i = 0; i < _externalContacts.Count; i++)
            {
                if (_externalContacts[i].ExternalContactID == externalContact.ExternalContactID)
                {
                    _externalContacts[i] = externalContact;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/02/18
        /// 
        /// Inserts Fake ExternalContact
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <param name="externalContact">Data to Insert</param>
        public bool InsertExternalContact(ExternalContact externalContact)
        {
            int count = _externalContacts.Count;
            _externalContacts.Add(externalContact);
            return _externalContacts.Count > count;
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/02/25
        /// 
        /// Inserts Fake ExternalContactType
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <param name="name">Contact Type Name</param>
        /// <param name="description">Contact Type Description</param>
        public bool InsertExternalContactType(string name, string description)
        {
            int count = _contactTypes.Count;
            _contactTypes.Add(name);
            return _contactTypes.Count > count;
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/02/18
        /// 
        /// Retreives Fake Data
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        public List<string> SelectAllExternalContactTypes()
        {
            return _contactTypes;
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/03/11
        /// 
        /// Deactivates Fake External Contact
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <param name="contactId">Contact ID</param>
        public bool DeactivateExternalContact(int contactId)
        {
            for (int i = 0; i < _externalContacts.Count; i++)
            {
                if (_externalContacts[i].ExternalContactID == contactId)
                {
                    _externalContacts[i].Active = false;
                    return true;
                }
            }

            return false;
        }
    }
}
