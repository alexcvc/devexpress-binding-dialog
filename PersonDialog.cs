using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraDataLayout;

// DevExpress namespaces
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;

namespace DevExpressBindingDialog
{
   /// <summary>
   /// XtraForm dialog with binding through BindingSource + DataLayoutControl.
   /// Fields are automatically generated from model properties, Lookup for Country is added manually.
   /// Changes are written to Model draft and applied to the original only when OK.
   /// </summary>
   public class PersonDialog : XtraForm
   {
      private readonly BindingSource _bindingSource = new();
      private readonly DataLayoutControl _dataLayoutControl = new() { Dock = DockStyle.Fill };
      private readonly SimpleButton _buttonContinue = new() { Text = "Continue" };
      private readonly SimpleButton _buttonCancel = new() { Text = "Cancel" };
      private readonly IEnumerable<Country> _countries;
      private readonly Person _original;

      public Person Model { get; }

      public PersonDialog(Person original, IEnumerable<Country> countries)
      {
         _original = original;
         _countries = countries.ToList();
         Model = (Person)original.Clone();

         Text = "Edit Person";
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
         lookUpCountry.DataBindings.Add("EditValue", _bindingSource, nameof(Person.CountryId), true,
            DataSourceUpdateMode.OnPropertyChanged);

         // Add a separate element to the layout (name "Country")
         _dataLayoutControl.Root.AddItem("Country", lookUpCountry);

         // Button panel
         var panelButtons = new PanelControl()
         {
            Dock = DockStyle.Bottom, Height = 48, BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
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

      private void OnContinueClick(object? sender, EventArgs e)
      {
         // Data validation using data annotations
         var validationContext = new ValidationContext(Model);
         var results = new List<ValidationResult>();
         if (!Validator.TryValidateObject(Model, validationContext, results, validateAllProperties: true))
         {
            XtraMessageBox.Show(string.Join("\n", results.Select(r => r.ErrorMessage)), "Validation",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.DialogResult = DialogResult.None; // keep the dialogue open
            return;
         }

         // Apply changes to the original model
         _original.CopyFrom(Model);
      }
   }
}