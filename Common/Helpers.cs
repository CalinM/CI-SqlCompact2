using System;

namespace Common
{
    public class Helpers
    {
        public static bool UnsavedChanges { get; set; }

        public delegate OperationResult SaveChanges();
        //public static event EventHandler OnInnerLoadPosterButtonPress;
    }
}
