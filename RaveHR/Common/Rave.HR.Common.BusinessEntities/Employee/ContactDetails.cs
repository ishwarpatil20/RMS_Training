using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    /// <summary>
    /// Defines business entities related to employees EmployeeContacts 
    /// </summary>
    [Serializable]
    public class ContactDetails
    {
        #region Properties

        /// <summary>
        /// Gets or sets the EmployeeEmployeeContactsId .
        /// </summary>
        /// <value>The EmployeeEmployeeContactsId.</value>
        public int EmployeeContactId { get; set; }

        /// <summary>
        /// Gets or sets the EMPId.
        /// </summary>
        /// <value>The EMPId.</value>
        public int EMPId { get; set; }

        /// <summary>
        /// Gets or sets the CityCode of the EmployeeContacts.
        /// </summary>
        /// <value>The CityCode of the EmployeeContacts.</value>
        public int CityCode { get; set; }

        /// <summary>
        /// Gets or sets the CountryCode of the EmployeeContacts.
        /// </summary>
        /// <value>The CountryCode of the EmployeeContacts.</value>
        public int CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the ContactNo of the EmployeeContacts.
        /// </summary>
        /// <value>The ContactNo of the EmployeeContacts.</value>
        public string ContactNo { get; set; }

        /// <summary>
        /// Gets or sets the Extension of the EmployeeContacts.
        /// </summary>
        /// <value>The Extension of EmployeeContacts.</value>
        public int Extension { get; set; }

        /// <summary>
        /// Gets or sets the AvalibilityTime of the EmployeeContacts.
        /// </summary>
        /// <value>The AvalibilityTime of the EmployeeContacts.</value>
        public string AvalibilityTime { get; set; }


        /// <summary>
        /// Gets or sets the ContactType of EmployeeContacts.
        /// </summary>
        /// <value>The ContactType for EmployeeContacts.</value>
        public int ContactType { get; set; }

        /// <summary>
        /// Gets or sets the CreatedById of EmployeeContacts.
        /// </summary>
        /// <value>The CreatedById for EmployeeContacts.</value>
        public string CreatedById { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate of EmployeeContacts.
        /// </summary>
        /// <value>The CreatedDate for EmployeeContacts.</value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the LastModifiedById of EmployeeContacts.
        /// </summary>
        /// <value>The LastModifiedById for EmployeeContacts.</value>
        public string LastModifiedById { get; set; }

        /// <summary>
        /// Gets or sets the LastModifiedDate of EmployeeContacts.
        /// </summary>
        /// <value>The LastModifiedDate for EmployeeContacts.</value>
        public DateTime LastModifiedDate { get; set; }
        
        /// <summary>
        /// Gets or sets the IsActive of EmployeeContacts.
        /// </summary>
        /// <value>The IsActive.</value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the ContactTypeName of EmployeeContacts.
        /// </summary>
        /// <value>The ContactTypeName for EmployeeContacts.</value>
        public string ContactTypeName { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public int Mode { get; set; }

        /// <summary>
        /// Gets or sets the name of the seat.
        /// </summary>
        /// <value>The name of the seat.</value>
        public string SeatName { get; set; }

        
        #endregion Properties
    }
}
