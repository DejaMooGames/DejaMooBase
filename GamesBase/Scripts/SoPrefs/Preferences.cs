using System.Collections.Generic;
using DejaMoo.SoPrefs;
using DejaMoo.Utilities;

public class Preferences : Singleton<Preferences>
{
	private readonly List<IResetablePref> prefs = new List<IResetablePref>();

	public void ResetPrefs()
	{
		foreach (var pref in prefs)
		{
			pref.ResetPref();
		}
	}

	public void RegisterPref(IResetablePref pref)
	{
		prefs.Add(pref);
	}
}