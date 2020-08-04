using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

namespace CombatHelper.Controls
{
    public class SaveLabel : Label
    {
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "Text" && int.TryParse(Text, out int result))
            {
                var sign = (result >= 0) ? "+" : "";
                Text = $"{sign} {result},";
            }
        }
    }
}