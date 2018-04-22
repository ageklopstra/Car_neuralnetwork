using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    private bool isTraning = false;
    public Text text;
	public static int henk;

    public GameObject auto;
    private int populationSize = 50;
    private int[] layers = new int[] { 1, 10, 10, 1 }; 
    private List<besturing> carlist = null;

    #region vars to save
    public List<NeuralNetwork> nets;
    public int generationNumber = 0;
    #endregion



	public static void plushoi(int x){
        henk += x;
	}


	void Update ()
    {
        
		if (henk >= 49) {
			isTraning = false;
		}
            if (isTraning == false)
            {
                if (generationNumber == 0)
                {
                    InitcarNeuralNetworks();
                }
                else
                {
                    nets.Sort();
                
				for (int i = 0; i < populationSize / 2; i++)
                    {
                        nets[i] = new NeuralNetwork(nets[i + (populationSize / 2)]);
                        nets[i].Mutate();

                        nets[i + (populationSize / 2)] = new NeuralNetwork(nets[i + (populationSize / 2)]);
                    }
                nets[0].Save();
                saveloadmanager.GenSave(this);
                for (int i = 0; i < populationSize; i++)
                    {
                        nets[i].SetFitness(0f);
                    }
                }


                generationNumber++;
                text.text = "Generation: " + generationNumber;

                henk = 0;
                isTraning = true;
                createcar();
                
                
            }
        


     
    }


    private void createcar()
    {
        if (carlist != null)
        {
            for (int i = 0; i < carlist.Count; i++)
            {
                GameObject.Destroy(carlist[i].gameObject);
            }

        }

        carlist = new List<besturing>();

        for (int i = 0; i < populationSize; i++)
        {
            besturing auto1 = ((GameObject)Instantiate(auto, new Vector3(5,1,Random.Range(-3,2)),auto.transform.rotation)).GetComponent<besturing>();
            auto1.Init(nets[i]);
            carlist.Add(auto1);
        }

    }

    void InitcarNeuralNetworks()
    {
        if (populationSize % 2 != 0)
        {
            populationSize = 20; 
        }

        nets = new List<NeuralNetwork>();
        generationNumber = saveloadmanager.genLoad();

        for (int i = 0; i < populationSize; i++)
        {
            NeuralNetwork net = new NeuralNetwork(layers);
            net.weights = saveloadmanager.netLoad();
            if (saveloadmanager.netLoad() == null)
            {
                net = new NeuralNetwork(layers);
            }
            nets.Add(net);
        }
    }
}
