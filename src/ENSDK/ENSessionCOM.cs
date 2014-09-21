namespace ENSDK
{
	public class ENSessionCOM
	{

		public void SetSharedSessionConsumerKey(string sessionConsumerKey, string sessionConsumerSecret, string sessionHost = null)
		{
			ENSession.SetSharedSessionConsumerKey(sessionConsumerKey, sessionConsumerSecret, sessionHost);
		}

		public ENSession SharedSession()
		{
			return ENSession.SharedSession;
		}

	}


}