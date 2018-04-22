using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;


public static class saveloadmanager  {

	public static void NetSave(float[][][] network)
    {
        BinaryFormatter bf = new BinaryFormatter();
		FileStream networkstream = new FileStream(Application.persistentDataPath + "/network.sav", FileMode.Create);
		NetworkData netwdata = new NetworkData(network);
		bf.Serialize (networkstream, netwdata);
		networkstream.Close ();
    }

    public static void GenSave(Manager manager)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream generationstream = new FileStream(Application.persistentDataPath + "/generation.sav", FileMode.Create);
        generationData genedata = new generationData(manager);
        bf.Serialize(generationstream, genedata);
        generationstream.Close();

    }

    public static float[][][] netLoad (){
		if (File.Exists (Application.persistentDataPath + "/network.sav")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream networkstream = new FileStream (Application.persistentDataPath + "/network.sav", FileMode.Open);
			NetworkData netdata = bf.Deserialize (networkstream) as NetworkData;
			networkstream.Close ();
			return netdata.weights1;
		} else {
            return null;
		}
	}

	public static int genLoad(){
		if (File.Exists (Application.persistentDataPath + "/generation.sav")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream genstream = new FileStream (Application.persistentDataPath + "/generation.sav", FileMode.Open);
			generationData gendata = bf.Deserialize (genstream) as generationData; 
			genstream.Close ();
			return gendata.generationNumber;
				
		} else {
			return 0;
		}
	}
		

	[Serializable]
	public class NetworkData{
        public float[][][] weights1;

		public NetworkData(float[][][] network1){
			weights1 = network1;
		}
	}

	[Serializable]
	public class generationData{
		public int generationNumber;

		public generationData(Manager manager){
			generationNumber = manager.generationNumber;
		}
}
}