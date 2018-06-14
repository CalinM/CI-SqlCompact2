using System;

namespace Common
{
    public class Helpers
    {
        private static bool _unsavedChanges;

        public static bool UnsavedChanges
        {
            get
            {
                return _unsavedChanges;
            }
            set
            {
                _unsavedChanges = value;
                GenericSetButtonsState2(value);
            }
        }

        public static Func<Boolean, object> GenericSetButtonsState;
        public static Action<Boolean> GenericSetButtonsState2;
        //public delegate OperationResult SaveChanges();
        //public static event EventHandler OnInnerLoadPosterButtonPress;

        public static void OnTextChanged(object sender, EventArgs eventArgs)
        {
            UnsavedChanges = true;
        }
    }
}
