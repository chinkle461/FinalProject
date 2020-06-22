using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Reservation.DATA.EF//.MetadataFolder
{
    #region Locations

    public class LocationMetadata
    {
        public int LocationId { get; set; }

        [Required(ErrorMessage = "*** Location Name is required ***")]
        [StringLength(50, ErrorMessage = "*** Max 50 characters ***")]
        [Display(Name = "Location")]
        public string LocationName { get; set; }

        [Required(ErrorMessage = "*** Address is required ***")]
        [StringLength(100, ErrorMessage = "*** Max 100 characters ***")]
        public string Address { get; set; }

        [Required(ErrorMessage = "*** City is required ***")]
        [StringLength(100, ErrorMessage = "*** Max 100 characters ***")]
        public string City { get; set; }

        [Required(ErrorMessage = "*** State is required ***")]
        [StringLength(2, ErrorMessage = "*** Max 2 characters ***")]
        public string State { get; set; }

        [Required(ErrorMessage = "*** Zip Code is required ***")]
        [StringLength(5, ErrorMessage = "*** Max 5 characters ***")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "*** Reservations allowed is required ***")]
        public byte ReservationLimit { get; set; }
    }
    [MetadataType(typeof(LocationMetadata))]
    public partial class Location { }

    #endregion

    #region Reservations
    public class ReservationMetadata
    {
        public int ReservationId { get; set; }

        public int OwnerAssetId { get; set; }

        public int LocationId { get; set; }

        [Display(Name = "Reservation Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime ReservationDate { get; set; }
    }
    [MetadataType(typeof(ReservationMetadata))]
    public partial class Reservation { }

    #endregion

    #region OwnerAssets
    public class OwnerAssetMetadata
    {
        public int OwnerAssetId { get; set; }

        [Required(ErrorMessage = "*** Name is required ***")]
        [StringLength(50, ErrorMessage = "*** Max 50 characters ***")]
        [Display(Name = "Name")]
        public string AssetName { get; set; }

        [Required(ErrorMessage = "*** Owner is required ***")]
        [StringLength(128, ErrorMessage = "*** Max 128 characters ***")]
        public string OwnerId { get; set; }

        [StringLength(50, ErrorMessage = "*** Max 50 characters ***")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [Display(Name = "Photo")]
        public string AssetPhoto { get; set; }

        [StringLength(300, ErrorMessage = "*** Max 300 characters ***")]
        [Display(Name = "Notes")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string SpecialNotes { get; set; }

        [Required(ErrorMessage = "*** Is Active is Required ***")]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = " Date Reserved ")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime DateAdded { get; set; }
        public string Relationship { get; set; }
    }
    [MetadataType(typeof(OwnerAssetMetadata))]
    public partial class OwnerAsset { }
    #endregion

    #region UserDetails
    public class UserDetailMetadata
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "*** First Name is required ***")]
        [StringLength(50, ErrorMessage = "*** Max 50 characters ***")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*** Last Name is required ***")]
        [StringLength(50, ErrorMessage = "*** Max 50 characters ***")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
    [MetadataType(typeof(UserDetailMetadata))]
    public partial class UserDetail {
        [Display(Name = "Name")]
        public string FullName { get { return FirstName + " " + LastName; } }
    }

    #endregion
}
