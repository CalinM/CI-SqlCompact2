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
                if (GenericSetButtonsState2 != null)
                    GenericSetButtonsState2(value);
            }
        }

        public static Func<Boolean, object> GenericSetButtonsState;
        public static Action<Boolean> GenericSetButtonsState2;

        public static IntPtr MainFormHandle;

        public static void OnTextChanged(object sender, EventArgs eventArgs)
        {
            UnsavedChanges = true;
        }
    }
}
