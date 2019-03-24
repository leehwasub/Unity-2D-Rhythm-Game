#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("Kr5cWyfkHOeyWj7RXCvlcCI1mMq89HWnkCp70BIYQJd+JUnsuXUXfdcAf5nG2gvkNNBZbCqzt+jP6wgmJ5UWNScaER49kV+R4BoWFhYSFxSkGRPGpwvRcLYLOlLmJNDLj5LXU/rub1RgkXPtDA1UGKrMlAUee0a+YT352uNLNy0uS8i3SB2wxc1zh1/gmTXgFxKNjQ5+Wzqo7fkvggB4qtKrrovuZ272sobKqbLuJguO6Rg/lRYYFyeVFh0VlRYWF4QYqFn5pPBoajweWuYf8wi4dNCjxzfvw3BP8WOBh+3O55Dm/rY9Nnlf7wwl5sNIAdspBs+YBWmyXG07U5ik6s2TwwvYkvaMgNksUFRj1DGj5FFFHGte6PXkzxT8ky8PehUUFhcW");
        private static int[] order = new int[] { 12,8,10,12,9,11,11,9,11,11,11,13,12,13,14 };
        private static int key = 23;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
