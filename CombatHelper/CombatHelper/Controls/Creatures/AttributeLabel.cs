using CombatHelper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

namespace CombatHelper.Controls
{
    public class AttributeLabel : Label
    {
        //public AttributeLabel() : base()
        //{
        //    Text = Mechanics.GetAttributeString(int.Parse(Text));
        //}

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if(propertyName == "Text" && int.TryParse(Text, out int result))
                Text = Mechanics.GetAttributeString(result);
        }
    }
}