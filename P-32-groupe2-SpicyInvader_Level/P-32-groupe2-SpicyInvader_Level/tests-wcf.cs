
namespace SpicyInvader
{
    class Test
    {
        public static void Try()
        {
            SoundServiceReference.SoundServiceClient client = new SoundServiceReference.SoundServiceClient();

            client.PlaySound();

        }
    }
}