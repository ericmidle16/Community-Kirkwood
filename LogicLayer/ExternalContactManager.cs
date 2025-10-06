/// <summary>
/// Creator:  Jacob McPherson
/// Created:  2025/02/18
/// Summary:  The manager for viewing external contacts
/// Last Updated By: Jacob McPherson
/// Last Updated: 2025/03/25
/// What was Changed: 	
/// </summary>

using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ExternalContactManager : IExternalContactManager
    {
        IExternalContactAccessor _contactAccessor;

        public ExternalContactManager()
        {
            _contactAccessor = new ExternalContactAccessor();
        }
        public ExternalContactManager(IExternalContactAccessor externalContactsAccessor)
        {
            this._contactAccessor = externalContactsAccessor;
        }

        // Author: Stan Anderson
        public List<ExternalContact> ViewAllExternalContacts()
        {
            List<ExternalContact> contacts = new List<ExternalContact>();
            try
            {
                contacts = _contactAccessor.SelectAllExternalContacts();
            }
            catch (Exception)
            { 
                throw;
            }
            return contacts;
        }

        // Author: Stan Anderson
        public ExternalContactVM ViewSingleExternalContact(int externalContactID)
        {
            ExternalContactVM externalContact = new ExternalContactVM();
            try
            {
                externalContact = _contactAccessor.SelectSingleExternalContact(externalContactID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return externalContact;
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/02/18
        /// 
        /// Adds New External Contact
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <param name="externalContact">Contact Information</param>
        public bool AddExternalContact(ExternalContact externalContact)
        {
            try
            {
                return _contactAccessor.InsertExternalContact(externalContact);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/02/25
        /// 
        /// Adds New External Contact Type
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <param name="name">Contact Type Name</param>
        /// <param name="description">Contact Type Description</param>
        public bool AddExternalContactType(string name, string description)
        {
            try
            {
                return _contactAccessor.InsertExternalContactType(name, description);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/03/04
        /// 
        /// Updates External Contact
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <param name="currentUserId">Id of Current User</param> 
        /// <param name="externalContactNew">New Contact Information</param>
        /// <param name="externalContactOld">Old Contact Information</param>
        public bool EditExternalContact(int currentUserId, ExternalContact externalContactNew, ExternalContact externalContactOld)
        {
            try
            {
                return _contactAccessor.UpdateExternalContact(currentUserId, externalContactNew, externalContactOld);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/02/18
        /// 
        /// Retreives All ExternalContactTypes
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        public List<string> GetAllExternalContactTypes()
        {
            try
            {
                return _contactAccessor.SelectAllExternalContactTypes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Jacob McPherson
        /// Created: 2025/03/11
        /// 
        /// Deactivates External Contact
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <param name="contactId">Contact ID</param>
        public bool DeactivateExternalContact(int contactId)
        {
            try
            {
                return _contactAccessor.DeactivateExternalContact(contactId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
