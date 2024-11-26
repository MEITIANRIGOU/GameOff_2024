/// <summary>
/// This is the main controller for any NPC that ONLY talks with no functionality
/// </summary>

using System.IO;

using UnityEngine;

public class GenericNPC : MonoBehaviour
{
    public Dialogue.Dialogue dlg;
    public int LoreNPCIndex = 0;
    public bool isTalking = false;
   // public Animator anim;
    private float _targetIsTalking;
    private float _currentIsTalking;
    public float _lerpSpeed = 0.03f;
    public Transform playerT;
    public PlayerController player;
    public bool interactPressed;
    public bool canContinue;
    public bool canTalk;
  //  public GameObject questMarker;
   // private Transform questTrans;
    public bool tutComplete;
    [SerializeField]
  //  private TutNPCManager npcManager;
    private bool loreDlg1 = false;
  //  private ProgressSaver load;
  //  public float rotationSpeed;
    
    private void Start()
    {
        dlg = gameObject.GetComponent<Dialogue.Dialogue>();
       // anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerT = GameObject.FindWithTag("Player").GetComponent<Transform>();
       // questTrans = transform.Find("QuestMarker");
       //// questMarker = questTrans.gameObject;
       // tutComplete = npcManager.tutComplete;
       /* 
        //Turns the quest marker off if tutorial complete
        if (tutComplete)
        {
            questMarker.SetActive(false);
        }*/
    }
    private void RotateTowardsPlayer()
    {
        //Find the direction to rotate towards the player
        Vector3 direction = (playerT.position - transform.position).normalized;
        
        // Remove any vertical rotation
        direction.y = 0;
        /*
        //Rotate
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        */
    }
    private void Update()
    {
        //Set variables 
        isTalking = dlg.isTalking;
        canContinue = dlg.canContinue;
        
        //Set target talking to isTalking to a lerp value in the animator to smoothly transition between talking states 
        _targetIsTalking = isTalking ? 1 : 0;
        _currentIsTalking = Mathf.Lerp(_currentIsTalking, _targetIsTalking, _lerpSpeed * Time.deltaTime);
      //  anim.SetFloat("isTalking", _currentIsTalking);
        
        //Set interact pressed 
        interactPressed = Input.GetKeyDown(KeyCode.E);

        //Prevents dialogue from calling multiple times 
        if (!loreDlg1)
        {
            loreDlg1 = true;
        }
        
        //Starts the dialogue if player is near. 
        if (canTalk && loreDlg1)
        {
            if (interactPressed && !isTalking)
            {
                dlg.StartDialogue(LoreNPCIndex);
                
               
            }
            
            //Next sentence 
            if (interactPressed && canContinue)
            {
                dlg.NextSentence();
            }
        }

        //Rotates the NPC if talking
        if (isTalking)
        {
            RotateTowardsPlayer();
        }
    }

    // Loads the progress data 
    public int LoadData()
    {
        string json = File.ReadAllText(Application.dataPath + "/saveProgress.json");
        SaveDataWrapper loadedData = JsonUtility.FromJson<SaveDataWrapper>(json);
        int loadedProgress = loadedData.progress;

        return loadedProgress;
    }
    
    //Stores the progress data 
    [System.Serializable]
    private class SaveDataWrapper
    {
        public int progress;
    }
    
    
    public void StartConversation()
    {
        dlg.StartDialogue(LoreNPCIndex);
    }
    
    //If player is nearby, has the ability to talk 
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canTalk = true;
        }
    }
    
    //If player is nearby, has the ability to talk 
    private void OnTriggerStay2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canTalk = true;
        }
    }
    
    // Player can't talk if out of range 
    private void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        canTalk = false;
    }
}