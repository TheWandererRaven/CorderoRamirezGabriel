// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CitiesTableApp
{
	[Register ("CitiesTVController")]
	partial class CitiesTVController
	{
		[Outlet]
		UIKit.UITableView CitiesTable { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CitiesTable != null) {
				CitiesTable.Dispose ();
				CitiesTable = null;
			}
		}
	}
}
