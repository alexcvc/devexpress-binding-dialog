using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevExpressBindingDialog;

/// <summary>
///    Represents a collaborator with properties for name, age, start date, country, and active status.
///    Provides functionality for property change notifications, cloning, and copying data from another instance.
/// </summary>
public class Collaborator : INotifyPropertyChanged, ICloneable
{
   /// <summary>
   ///    Represents the private backing field for the <c>Age</c> property in the <c>Collaborator</c> class.
   ///    Stores the age of the collaborator as an integer value.
   /// </summary>
   private int _age;

   /// <summary>
   ///    Represents the identifier of a country associated with the collaborator.
   /// </summary>
   private int? _countryId;

   /// <summary>
   ///    Represents the date when an activity or event associated with this entity begins.
   /// </summary>
   private DateTime _dateOfEntry = DateTime.Today;

   /// <summary>
   ///    Represents a boolean field indicating the active status of the collaborator.
   /// </summary>
   private bool _isActive;

   /// <summary>
   ///    Represents the private backing field for the <see cref="OfficialRepresentive" /> property, storing the name of the
   ///    collaborator.
   /// </summary>
   private string _officialRepresentive = string.Empty;

   /// Gets or sets the identifier of the country associated with the collaborator.
   /// This property is nullable and allows differentiation between assigned and unassigned countries.
   /// Changes to this property trigger the PropertyChanged event.
   [DisplayName("Country ID")]
   [Description("The identifier of the country associated with the collaborator.")]
   [Range(1, 193, ErrorMessage = "Country ID must be a positive integer.")]
   [DefaultValue(null)]
   public int? CountryId
   {
      get => _countryId;
      set
      {
         if (_countryId != value)
         {
            _countryId = value;
            OnPropertyChanged(nameof(CountryId));
         }
      }
   }

   /// Represents the active status of the collaborator.
   /// This property indicates whether the collaborator is currently active or not.
   /// It is a boolean value where true denotes active and false denotes inactive.
   /// When the value changes, a PropertyChanged event is raised to notify observers.
   [DisplayName("Is Active")]
   [Description("Indicates whether the collaborator is currently active.")]
   public bool IsActive
   {
      get => _isActive;
      set
      {
         if (_isActive != value)
         {
            _isActive = value;
            OnPropertyChanged(nameof(IsActive));
         }
      }
   }

   /// Gets or sets the name of the collaborator.
   /// This property represents the name of an individual and is required to be a non-empty string.
   /// It is restricted to a maximum length of 100 characters. Changing the value of this property
   /// triggers the PropertyChanged event.
   /// A validation exception will be thrown if the assigned value does not meet the requirements.
   /// See also:
   /// Age, DateOfEntry, CountryId, IsActive.
   [Required]
   [StringLength(100)]
   [DisplayName("Official Representive")]
   [Description("The official representive of the collaborator.")]
   public string OfficialRepresentive
   {
      get => _officialRepresentive;
      set
      {
         if (_officialRepresentive != value)
         {
            _officialRepresentive = value;
            OnPropertyChanged(nameof(OfficialRepresentive));
         }
      }
   }

   /// Gets or sets the age of the collaborator.
   /// The value must be within the range of 0 to 120.
   [Range(0, 120, ErrorMessage = "Age must be between 0 and 120.")]
   [DisplayName("Age")]
   [Description("The age of the collaborator in years.")]
   public int Age
   {
      get => _age;
      set
      {
         if (_age != value)
         {
            _age = value;
            OnPropertyChanged(nameof(Age));
         }
      }
   }

   /// Gets or sets the start date associated with the collaborator.
   /// This property represents the starting date related to a collaborator's record.
   /// It is initialized to today's date by default and can be updated as needed.
   /// Changes to this property raise the PropertyChanged event.
   [DisplayName("Date of Entry")]
   [Description("The date when the collaborator entered the coalition.")]
   public DateTime DateOfEntry
   {
      get => _dateOfEntry;
      set
      {
         if (_dateOfEntry != value)
         {
            _dateOfEntry = value;
            OnPropertyChanged(nameof(DateOfEntry));
         }
      }
   }

   /// Creates a shallow copy of the current Collaborator instance.
   /// <returns>
   ///    A new object that is a shallow copy of the current instance.
   /// </returns>
   public object Clone()
   {
      return MemberwiseClone();
   }

   /// Occurs when a property value changes in the implementing class.
   /// This event is part of the INotifyPropertyChanged interface and is raised
   /// to notify subscribers that a property value has been updated. It is used to
   /// support data binding by enabling UI components to automatically reflect changes
   /// in underlying data.
   public event PropertyChangedEventHandler? PropertyChanged;

   /// Invokes the PropertyChanged event to notify listeners that a property value has changed.
   /// This method is typically called by property setters to signal that a particular property has been updated.
   /// <param name="name">The name of the property that changed.</param>
   private void OnPropertyChanged(string name)
   {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
   }

   /// Copies the values from the specified `Collaborator` object to the current instance.
   /// <param name="other">
   ///    The `Collaborator` object from which the values will be copied. Values of all properties
   ///    including OfficialRepresentive, Age, DateOfEntry, CountryId, and IsActive will be copied.
   /// </param>
   public void CopyFrom(Collaborator other)
   {
      OfficialRepresentive = other.OfficialRepresentive;
      Age = other.Age;
      DateOfEntry = other.DateOfEntry;
      CountryId = other.CountryId;
      IsActive = other.IsActive;
   }

   /// Converts the current object to its string representation.
   /// <returns>
   ///    A string that represents the current object. The string includes the properties
   ///    OfficialRepresentive, Age, DateOfEntry, CountryId, and IsActive, formatted as OfficialRepresentive=Value, Age=Value,
   ///    DateOfEntry=Value, CountryId=Value, IsActive=Value.
   /// </returns>
   public override string ToString()
   {
      return
         $"OfficialRepresentive={OfficialRepresentive}, Age={Age}, DateOfEntry={DateOfEntry:d}, CountryId={CountryId}, IsActive={IsActive}";
   }
}