using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BookStore.Forms.Droid
{
    internal class UserInteractionListener : Java.Lang.Object, IDialogInterfaceOnCancelListener
    {
        private readonly Action _listenerAction;

        public UserInteractionListener(Action listenerAction)
        {
            _listenerAction = listenerAction;
        }

        public void OnCancel(IDialogInterface dialog)
        {
            _listenerAction?.Invoke();
        }
    }
}