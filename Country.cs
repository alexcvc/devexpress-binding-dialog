using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevExpressBindingDialog;

/// <summary>
///    Represents a country with an identification number and a name.
/// </summary>
public class Country
{
   /// <summary>
   ///    Gets or sets the unique identifier for the country.
   /// </summary>
   [DisplayName("Country ID")]
   [Description("The unique identifier for the country.")]
   [Range(1, 193, ErrorMessage = "Country ID must be a positive integer.")]
   public int Id { get; set; }

   /// <summary>
   ///    Gets or sets the name of the country.
   /// </summary>
   [DisplayName("Country Name")]
   [Description("The name of the country.")]
   public string Name { get; set; } = string.Empty;

   /// Converts the current instance to its string representation.
   /// <returns>
   ///    The name of the country as a string.
   /// </returns>
   public override string ToString()
   {
      return Name;
   }
}