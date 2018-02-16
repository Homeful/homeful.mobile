using System;
namespace Homeful.mobile
{
    public class StopDataStore : FirebaseDataStore<Stop>
    {
        protected override string Path { get; set; } = "stops";
    }
}
