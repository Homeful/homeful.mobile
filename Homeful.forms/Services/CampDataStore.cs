using System;
namespace Homeful.mobile
{
    public class CampDataStore : FirebaseDataStore<Camp>
    {
        protected override string Path { get; set; } = "camps";
    }
}
