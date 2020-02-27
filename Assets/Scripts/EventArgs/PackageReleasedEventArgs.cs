namespace EventArgs
{
    public class PackageReleasedEventArgs
    {
        public Package Package { get; }

        public PackageReleasedEventArgs(Package package)
        {
            Package = package;
        }
    }
}