using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraDataLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

// DevExpress namespaces

namespace DevExpressBindingDialog;

/// <summary>
///    XtraForm dialog with binding through BindingSource + DataLayoutControl.
///    Fields are automatically generated from model properties, Lookup for Country is added manually.
///    Changes are written to Model draft and applied to the original only when OK.
/// </summary>
public class CollaboratorDialog : XtraForm
{
   /// <summary>
   ///    Represents a BindingSource object used for data binding the Collaborator model to the UI components in the dialog.
   /// </summary>
   private readonly BindingSource _bindingSource = new();

   /// <summary>
   ///    Represents the "Cancel" button in the CollaboratorDialog form.
   /// </summary>
   private readonly SimpleButton _buttonCancel = new() { Text = "Cancel" };

   /// <summary>
   ///    Represents the "Continue" button in the CollaboratorDialog form.
   /// </summary>
   private readonly SimpleButton _buttonContinue = new() { Text = "Continue" };

   /// <summary>
   ///    Represents a collection of country data used in the dialog for lookup and selection purposes.
   /// </summary>
   private readonly IEnumerable<Country> _countries = [];

   /// <summary>
   ///    Represents a <see cref="DataLayoutControl" /> used for binding and auto-generating
   ///    UI elements based on the properties of a data source.
   /// </summary>
   private readonly DataLayoutControl _dataLayoutControl = new() { Dock = DockStyle.Fill };

   /// <summary>
   ///    Stores a reference to the original Collaborator object, which acts as the source of truth for data within the
   ///    dialog.
   /// </summary>
   private readonly Collaborator _original;

   /// <summary>
   ///    Represents a dialog form for editing a <see cref="Collaborator" /> object.
   /// </summary>
   /// <param name="original">
   ///    The original <see cref="Collaborator" /> object to be edited.
   ///    A clone of this object is made, and changes are applied to the clone.
   /// </param>
   /// <param name="countries">
   ///    A collection of available countries to populate a dropdown (LookUpEdit) for country selection.
   /// </param>
   public CollaboratorDialog(Collaborator original, IEnumerable<Country>countries)
   {
      _original = original;
      _countries = countries.ToList();
      Model = (Collaborator)original.Clone();

      Text = "Edit Collaborator";
      StartPosition = FormStartPosition.CenterParent;
      MinimizeBox = false;
      MaximizeBox = false;
      Width = 550;
      Height = 420;

      // Bindings
      _bindingSource.DataSource = Model;
      _dataLayoutControl.DataSource = _bindingSource;

      // Autogeneration of editors by model properties
      _dataLayoutControl.RetrieveFields();

      // Creating and customising LookUpEdit for Country
      var lookUpCountry = new LookUpEdit();
      lookUpCountry.Properties.DataSource = _countries;
      lookUpCountry.Properties.DisplayMember = nameof(Country.Name);
      lookUpCountry.Properties.ValueMember = nameof(Country.Id);
      lookUpCountry.Properties.NullText = "";
      lookUpCountry.DataBindings.Add("EditValue", _bindingSource, nameof(Collaborator.CountryId), true,
         DataSourceUpdateMode.OnPropertyChanged);

      // Add a separate element to the layout (name "Country")
      _dataLayoutControl.Root.AddItem("Country", lookUpCountry);

      // Button panel
      var panelButtons = new PanelControl
      {
         Dock = DockStyle.Bottom, Height = 48, BorderStyle = BorderStyles.NoBorder
      };
      _buttonContinue.DialogResult = DialogResult.Continue;
      _buttonCancel.DialogResult = DialogResult.Cancel;
      _buttonContinue.Anchor = AnchorStyles.Right | AnchorStyles.Top;
      _buttonCancel.Anchor = AnchorStyles.Right | AnchorStyles.Top;
      _buttonCancel.Left = panelButtons.Width - 160;
      _buttonContinue.Left = panelButtons.Width - 80;
      _buttonCancel.Top = 10;
      _buttonContinue.Top = 10;
      panelButtons.Resize += (_, __) =>
      {
         _buttonCancel.Left = panelButtons.Width - 180;
         _buttonContinue.Left = panelButtons.Width - 90;
      };
      panelButtons.Controls.Add(_buttonCancel);
      panelButtons.Controls.Add(_buttonContinue);

      Controls.Add(_dataLayoutControl);
      Controls.Add(panelButtons);

      AcceptButton = _buttonContinue;
      CancelButton = _buttonCancel;

      _buttonContinue.Click += OnContinueClick;
   }

   /// <summary>
   ///    Gets the model instance being edited in the dialog.
   /// </summary>
   public Collaborator Model { get; }

   /// <summary>
   ///    Handles the click event of the "Continue" button in the dialog.
   /// </summary>
   /// <param name="sender">
   ///    The source of the click event, typically the "Continue" button.
   /// </param>
   /// <param name="e">
   ///    The event arguments associated with the click action.
   /// </param>
   private void OnContinueClick(object? sender, EventArgs e)
   {
      // Data validation using data annotations
      var validationContext = new ValidationContext(Model);
      var results = new List<ValidationResult>();
      if (!Validator.TryValidateObject(Model, validationContext, results, true))
      {
         XtraMessageBox.Show(string.Join("\n", results.Select(r => r.ErrorMessage)), "Validation",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
         DialogResult = DialogResult.None; // keep the dialogue open
         return;
      }

      // Apply changes to the original model
      _original.CopyFrom(Model);
   }
}