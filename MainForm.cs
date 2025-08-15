using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DevExpressBindingDialog
{
    public class MainForm : Form
    {
        private readonly Button _btnEdit = new() { Text = "Edit Person (DevExpress Dialog)", Dock = DockStyle.Top, Height = 40 };
        private readonly TextBox _info = new() { Multiline = true, Dock = DockStyle.Fill, ReadOnly = true, ScrollBars = ScrollBars.Vertical };

        private readonly List<Country> _countries = new()
        {
            new Country{ Id = 1, Name = "Germany" },
            new Country{ Id = 2, Name = "USA" },
            new Country{ Id = 3, Name = "Japan" },
        };

        private readonly Person _person = new Person { Name = "Alex", Age = 35, StartDate = DateTime.Today, CountryId = 1, IsActive = true };

        public MainForm()
        {
            Text = "DevExpress BindingSource + DataLayoutControl sample";
            Width = 800;
            Height = 500;

            Controls.Add(_info);
            Controls.Add(_btnEdit);

            _btnEdit.Click += (_, __) =>
            {
                using var dlg = new PersonDialog(_person, _countries);
                var result = dlg.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    // Коммитим изменения в оригинальную модель
                    _person.CopyFrom(dlg.Model);
                    MessageBox.Show("Saved changes:\n" + _person, "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Canceled. No changes applied.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                RenderInfo();
            };

            RenderInfo();
        }

        private void RenderInfo()
        {
            _info.Text = "Current Person:\r\n" + _person + "\r\n\r\nCountries:\r\n" + string.Join(", ", _countries.Select(c => $"{c.Id}:{c.Name}"));
        }
    }
}
