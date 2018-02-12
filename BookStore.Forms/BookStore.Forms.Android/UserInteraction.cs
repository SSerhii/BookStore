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
using System.Threading.Tasks;
using BookStore.Core.Interfaces;

namespace BookStore.Forms.Droid
{
    class UserInteraction : IUserInteraction
    {
        Context CurrentContext => Xamarin.Forms.Forms.Context;

        public Task ShowMessageAsync(string message)
        {
            return AlertAsync(message, null, "OK", true  );
        }

        private void Alert(string message, Action doneAction, string title, string okButton, bool cancellable)
        {
            Application.SynchronizationContext.Post(ignored =>
            {
                if (CurrentContext == null)
                    return;

                new AlertDialog.Builder(CurrentContext)
                                           .SetMessage(message)
                                  .SetTitle(title)
                                        .SetCancelable(cancellable)
                                           .SetOnCancelListener(new UserInteractionListener(doneAction))
                                           .SetPositiveButton(okButton, (sender, e) => doneAction?.Invoke())
                                  .Show();
            }, null);
        }

        public Task AlertAsync(string message, string title, string okButton, bool cancellable)
        {
            var tcs = new TaskCompletionSource<bool>();
            Alert(message, () => tcs.TrySetResult(true), title, okButton, cancellable);
            return tcs.Task;
        }
    }
}