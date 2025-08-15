# DevExpress BindingSource + DataLayoutControl Dialog Sample

This is a minimal \*\*WinForms\*\* sample project using \*\*DevExpress\*\* components to create a modal dialog  
bound to a custom data class via `BindingSource` and `DataLayoutControl`.  
The dialog edits a \*\*clone\*\* of the original object and applies changes only if the user presses \*\*OK\*\*.  
Pressing \*\*Continue\*\* discards all modifications.

## Features

- `BindingSource` bound to a custom `Collaborator` class implementing `INotifyPropertyChanged`.
- `DataLayoutControl` auto-generates editors for model properties using `RetrieveFields()`.
- Manual `LookUpEdit` for selecting a country (bound to `CountryId` property).
- Validation using `DataAnnotations` (`[Required]`, `[Range]`, etc.).
- OK/Continue buttons with `DialogResult` handling.
- Changes are applied only on OK (`CopyFrom()` from the edited clone to the original object).

## Project Structure

```text
DevExpressBindingDialog/
├── DevExpressBindingDialog.csproj     # .NET 8 WinForms project file
├── Program.cs                         # Entry point
├── Country.cs                         # Country class for drop-down list
├── MainForm.cs                        # Main window with "Edit Collaborator" button
├── Collaborator.cs                    # Collaborator model + Country class
└── CollaboratorDialog.cs              # DevExpress XtraForm dialog
```

## Requirements

- Visual Studio 2022 or later.
- .NET 8.0 SDK installed.
- DevExpress WinForms installed.
  - The project references `DevExpress.XtraEditors` and `DevExpress.XtraLayout`.
  - An active DevExpress license or trial is required.

## How to Run

1. Clone the git repository.
2. Open `DevExpressBindingDialog.csproj` in Visual Studio.
3. Make sure DevExpress WinForms assemblies are referenced:
   - If DevExpress is installed, `using DevExpress.*` namespaces should resolve automatically.
   - Otherwise, add references manually from your DevExpress installation or via the DevExpress NuGet feed.
4. Run the project (`F5` or `Ctrl+F5`).

## How It Works

- `MainForm.cs`
  - Holds an instance of `Collaborator` and a list of `Country` objects.
  - Opens `CollaboratorDialog` when the button is clicked.
  - If the dialog returns `DialogResult.OK`, updates the original `Collaborator` with changes.

- `CollaboratorDialog.cs`
  - Creates a clone of the `Collaborator` for editing.
  - Binds the clone to a `BindingSource` and assigns it to `DataLayoutControl.DataSource`.
  - Calls `RetrieveFields()` to auto-generate editors.
  - Adds a `LookUpEdit` for country selection.
  - Validates via `DataAnnotations` before saving.
  - On OK, copies the edited values back to the original object.

## Example Workflow

1. Open the dialog and edit the fields.
2. Select a country from the dropdown list.
3. Click OK to apply changes or Continue to discard them.

## License

1. This sample is provided for test purposes and is not a complete application. With MIT license, you can use it in your projects, but it is not intended for production use as is. 
2. You must have a valid DevExpress WinForms license to build and run it.
For more information, see the [DevExpress License Agreement](https://www.devexpress.com/Support/Center/Question/Details/T113204).
