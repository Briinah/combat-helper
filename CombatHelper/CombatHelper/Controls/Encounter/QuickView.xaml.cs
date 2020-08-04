﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CombatHelper.Controls.Encounter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuickView : ContentView
    {
        public event EventHandler OnSetConditionsClicked;
        public static readonly BindableProperty OnSetConditionsClickedProperty = BindableProperty.Create("OnSetConditionsClicked", typeof(EventHandler), typeof(QuickView));

        public QuickView()
        {
            InitializeComponent();
        }

        private void SetConditions(object sender, EventArgs e)
        {
            OnSetConditionsClicked.Invoke(sender, e);
        }
    }
}