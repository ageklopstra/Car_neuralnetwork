using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class besturing : MonoBehaviour
{
    public Manager manager;
	private bool dead = false;
    private bool initilized;
    private NeuralNetwork net;
	public Rigidbody rig;
    public float input;
    public float richting;

    #region besturings vars
    public float MotorForce, SteerForce, BreakForce, friction;
    public GameObject car;
    public Transform auto;
	public Transform start;

    #endregion

    #region output
    public float rijdsnelheid = 2;
    
    #region input
    public muur_detectie links_script;
    public muur_detectie rechts_script;
    public muur_detectie midden_script;
    private float timer = 1;
    public float timerlengte = 1;
    private float timer1 = 0;
    #endregion
    #endregion

 
    private void FixedUpdate()
    {
        if (initilized)
        {
            float[] inputs = new float[1];
            inputs[0] = input;
            float[] output = net.FeedForward(inputs);
            richting = output[0];
            

        }
    }
    #region fitness
    private void OnTriggerEnter(Collider other)
    {
        if (!dead)
        {
            if (other.tag == "finish")
            {
                finish();
            }

            if (other.tag == "muur")
            {
                die();
            }
        }
    }

    void finish()
    {
        if (timer1 <= 20)
        {
            net.AddFitness(20 - timer1);
        }
        timer = 0;
        die();
    }
    #endregion

    void Update()
    {


        if (!dead)
        {
            net.AddFitness(Time.deltaTime);
        }
        timer -= Time.deltaTime;
        #region varsetup
        if (links_script.muurcol)
        {
            input = -1;
        }
        
        if (rechts_script.muurcol)
        {
            input = 1;
        }

        #endregion
        #region autobesturing

        if (rijdsnelheid != 0)
        {

            transform.Translate(Vector3.forward * Time.deltaTime * rijdsnelheid);
        }
            
        car.transform.Rotate(Vector3.up * SteerForce * Time.deltaTime * (richting * 2), Space.World);
        
		#endregion
    }

    public void Init(NeuralNetwork net)
    {

        this.net = net;
        initilized = true;
    }

    public void die()
    {
        plushoi() ;
        this.transform.position = new Vector3 (200, 1, 200);
		dead = true;
	}
    public static void plushoi()
    {
        Manager.plushoi(1);
    }
}