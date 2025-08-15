using System;
using System.Linq;
using System.Windows.Forms;

namespace DevExpressBindingDialog;

/// Main form for editing a collaborator's details using DevExpress dialog.
/// Displays current collaborator info and a list of countries.
public class MainForm : Form
{
   /// Button to open the collaborator edit dialog.
   private readonly Button _buttonEdit = new()
      { Text = @"Edit Collaborator in DevExpress Dialog", Dock = DockStyle.Top, Height = 40 };

   /// Collaborator data model.
   private readonly Collaborator _collaborator = new()
   {
      OfficialRepresentive = "Friedrich Merz", Age = 64, DateOfEntry = DateTime.Today, CountryId = 1, IsActive = true
   };

   /// Read-only textbox showing collaborator info and countries.
   private readonly TextBox _information = new()
      { Multiline = true, Dock = DockStyle.Fill, ReadOnly = true, ScrollBars = ScrollBars.Vertical };

   /// Initializes the form and sets up controls.
   public MainForm()
   {
      Text = "DevExpress BindingSource + DataLayoutControl Sample";
      Width = 800;
      Height = 500;

      Controls.Add(_information);
      Controls.Add(_buttonEdit);

      _buttonEdit.Click += (_, __) =>
      {
         using var dlg = new CollaboratorDialog(_collaborator, Coalition.Collaborators);
         var result = dlg.ShowDialog(this);
         if (result == DialogResult.Continue)
         {
            _collaborator.CopyFrom(dlg.Model);
            MessageBox.Show("Saved changes:\n" + _collaborator, "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
         }
         else
         {
            MessageBox.Show("Canceled. No changes applied.", "Cancel", MessageBoxButtons.OK,
               MessageBoxIcon.Information);
         }

         RenderInfo();
      };

      RenderInfo();
   }

   /// Updates the textbox with collaborator info and countries.
   private void RenderInfo()
   {
      _information.Text = "Current Country Representative:\r\n" + _collaborator + "\r\n\r\nCountries:\r\n" +
                          string.Join(", ", Coalition.Collaborators.Select(c => $"{c.Id}:{c.Name}"));
   }
}