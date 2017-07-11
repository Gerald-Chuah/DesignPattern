using System;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Journal
{
	private readonly List<string> entries = new List<string>();
	private static int count=0;
	public int AddEntry(string text)
	{
		entries.Add(String.Format("{0}:{1}",++count,text));
		// entries.Add($"{++count}: {text}"); Only C# 6.0 or greater support interpolated strings
		return count;
	}

	public void RemoveEntry(int index)
	{
		entries.RemoveAt(index);
	}


	public override string ToString()
	{
		return string.Join(Environment.NewLine,entries.ToArray());
	}
}

public class Persistence
{
	public void SaveToFile(Journal journal, string filename, bool overwrite=false)
	{
		if (overwrite || !File.Exists(filename))
		{
			File.WriteAllText(filename,journal.ToString());
		}
	}
}

public class SingleResponsibility : MonoBehaviour
{

	void Start () 
	{
		var j = new Journal();
		j.AddEntry("Hello");
		j.AddEntry("Wei");
		UnityEngine.Debug.Log(j);

		var p = new Persistence ();
		var filename = Application.dataPath + "/TextFile/Journal.txt";
		p.SaveToFile(j, filename,true);
		Process.Start(filename);//open the txt file OwO

	}

}
