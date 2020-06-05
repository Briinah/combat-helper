using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Xamarin.Forms;

namespace CombatHelper.Controls
{
    public class XListView : ListView
    {
        public XListView() 
            : base()
        {
            ItemTapped += XListView_ItemTapped;
        }

        private void XListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            if (sender is ListView lv)
                lv.SelectedItem = null;
        }
    }
}
