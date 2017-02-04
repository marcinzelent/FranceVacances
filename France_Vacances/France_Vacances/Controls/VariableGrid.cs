using Windows.UI.Xaml.Controls;
using France_Vacances.Model;

namespace France_Vacances.Controls
{
    public class VariableGrid : GridView
    {
        protected override void PrepareContainerForItemOverride
            (Windows.UI.Xaml.DependencyObject element, object item)
        {

            var tile = item as AnnouncementModel;
            if (tile != null)
            {
                var griditem = element as GridViewItem;
                if (griditem != null)
                {
                    VariableSizedWrapGrid.SetColumnSpan(griditem, tile.ColumnSpan);
                    VariableSizedWrapGrid.SetRowSpan(griditem, tile.RowSpan);
                }
            }
            base.PrepareContainerForItemOverride(element, item);
        }
    }
}
