
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ball_controller : MonoBehaviour
{
    //GAMEOBJECTS
    public GameObject ball, rod, ballCyphere;
    public GameObject endgamepanel, finishgamepanel,settingsPanel;
    public GameObject portal, hole, sparkle;
    public GameObject progressbar;
    //NUMERICS
    public float mouse_y, temp_mouse_y;
    public float max_rot_x, rot_x, pos_y;
    public float force;
    public float maxProgress;
    public int score,bestscore;
    public float startPos, tempPos;
    public int level;
    public float speed;
    //BOOLS
    public bool holding, released;
    public bool isFinished, isBoosted;
    //COMPENENTS
    Camera cam;
    Rigidbody rb;
    private generate_level gl; 
    public Image nextLevelImg;
    public TextMeshProUGUI currentLtext,nextLtext,scoretext,bestscoretext;
    public AudioClip releaseS, goldLootS, stickS,finishS,metalS,boostedS,breakS;
    public AudioSource auSo;
    public Material flame, wind;

    void Start()
    {
        bestscore = PlayerPrefs.GetInt("bestscore");
        cam = Camera.main;
        max_rot_x = -40f;
        rb = gameObject.GetComponent<Rigidbody>();
        gl = GameObject.FindGameObjectWithTag("levelgenerator").GetComponent<generate_level>();//GET LEVEL GENERATOR SCRIPT
        auSo = gameObject.GetComponent<AudioSource>();
        startPos = 0;
        level = PlayerPrefs.GetInt("level");//GET CURRENT LEVEL
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            //FOR EDITOR TESTING !!!
            speed = 150f;
        }
        if (Application.platform == RuntimePlatform.Android) 
        {
            //FOR MOBILE BUILD !!!
            speed = 300f;
        }

    }

    
    void Update()
    {
        if (!isBoosted) {
            ballCyphere.GetComponent<TrailRenderer>().material = wind; //IF IS BOOSTED CHANGE TRAIL TO WHITE WIND
        }
        if (gameObject.transform.position.y < (tempPos - 5f)) {
            if (!isFinished)
            {
                gameOver();
            }
        }
        scoretext.text = score.ToString();
        bestscoretext.text = "Best: "+bestscore.ToString();
        maxProgress = gl.maxTowerLenght;
        pos_y = ball.transform.position.y;
        rot_x = ball.transform.rotation.x *100;
        force = mouse_y - temp_mouse_y;
        if (isFinished == false)
        {
            progressbar.transform.localScale = new Vector2(pos_y / maxProgress, 1); //CHANGE PROGRESS BAR WITH BALL HEIGHT FOR TOWER HEIGHT
        }
        currentLtext.text = gl.level.ToString(); //LEFT LEVEL CIRCLE TEXT
        nextLtext.text = (gl.level + 1).ToString(); //RIGHT LEVEL CIRCLE TEXT
       
            
        
      
            
        
        if (!released) //IF BALL STICK IN WALL
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            if (Input.GetMouseButtonDown(0))
            {
                temp_mouse_y = Camera.main.ScreenToViewportPoint(Input.mousePosition).y; // GET MOUSE OR FINGER POS FOR FIRST TOUCH
            }
            if (Input.GetMouseButtonUp(0))//FINGER OR MOUSE UP RELEASE BALL
            {
                if (mouse_y < temp_mouse_y)
                {
                    ReleaseBall();
                }
            }
        }
        if (transform.position.y < gl.maxTowerLenght) // IF BALL LOWER THEN TOWER HEIGHT
        {
            if (Input.GetMouseButton(0))
            {
                if (!released)
                {
                    holding = true;//IF WE HOLDING 
                    mouse_y = Camera.main.ScreenToViewportPoint(Input.mousePosition).y; // GET MOUSE OR FINGER POS WHEN DRAG
                }

            }
            else
            {
                holding = false; //IF WE NOT HOLDING
            }
        }
        if (released) // IF BALL IS FALLING OR GOING UP
        {
            if (pos_y < gl.maxTowerLenght)
            {
                ballCyphere.gameObject.transform.Rotate(Vector3.right * 500f * Time.deltaTime);//ROTATE BALL AROUND ITSELF FOR VISUAL
                gameObject.GetComponent<BoxCollider>().enabled = false;
                if (Input.GetMouseButtonDown(0))
                {
                    StickWall();//IF WE CLICK OR TOUCH TRY TO STICK WALL
                }
            }
        }
        
        if (holding) //IF WE HOLDING
        {
            if (ball.transform.rotation.x != max_rot_x) {
                ball.transform.Rotate(force * 5, 0,0);   //ROTATE ROD AND BALL TOGETHER LIKE A BOW
            }
            if (rot_x < max_rot_x) {
                ball.transform.rotation = Quaternion.Euler(-45, 0, 0); //ROTATE THEM UNTIL 45 DEGRESS
            }
            if (rot_x > 0)
            {
                ball.transform.rotation = Quaternion.Euler(0, 0, 0); //FOR DENIED ROTATE UP
            }
        }
    }
    void ReleaseBall() { // RELEASE BALL AFTER STRECH

        rb.isKinematic = false;
        rb.useGravity = true;
        released = true;
        ball.transform.rotation = Quaternion.Euler(0, 0, 0); //SET ROTATION ZERO
        
        
        rod.SetActive(false);//CLOSE THE ROD

        if (isBoosted)//IF OUR BALL BOOSTED
        {
            auSo.clip = boostedS; //CHANGE SOUND CLIP 
            auSo.Play();//PLAY BOOST SOUND
            rb.AddForce(Vector3.up * speed * 2 * Math.Abs(force * 10)); //AD FORCE 2X AND X DRAG VALUE
        }
        else {
            auSo.clip = releaseS;//CHANGE SOUND CLIP 
            auSo.Play();//PLAY RELAESE SOUND
            rb.AddForce(Vector3.up * speed * Math.Abs(force * 10));//AD FORCE X DRAG VALUE
        }
        
        mouse_y = 0;
        temp_mouse_y = 0; //SET MOUSE AND TEMPMOUSE POS TO ZERO
        
        Debug.Log("Ball Released");
    }
    void StickWall() { 
        isBoosted = false; 
        Invoke("setFalse", 0.01f);
        tempPos = transform.position.y; //GET BALL POS TO TEMPORARIY POS
        rb.isKinematic = true;
        rb.useGravity = false;
        rod.SetActive(true); // OPEN THE ROD
        ball.transform.rotation = Quaternion.Euler(0, 0, 0); //SET BALL ROT ZERO
        auSo.clip = stickS; //CHANGE SOUND CLIP 
        auSo.Play();//PLAY STICK SOUND
        createHole();//CREATE AN OBJECT LIKE WE DIG HOLE
        StartCoroutine(pointsForDistance());//ADD SOME POINT FOR DISTANCE 
    }
    IEnumerator brokenRod() {
        
        rb.isKinematic = false;
        rb.useGravity = true;
        ball.transform.rotation = Quaternion.Euler(0, 0, 0);//SET BALL ROT ZERO
        auSo.clip = metalS;//CHANGE SOUND CLIP 
        auSo.Play();//PLAY METAL SOUND
        rod.SetActive(false);//CLOSE THE ROD
        yield return new WaitForSeconds(0.3f);//WAIT 0.3SEC FOR HIT AGAIN
        released = true;
    }
    void gameOver() { // GAMEOVER
        Time.timeScale = 0;
        endgamepanel.SetActive(true); //OPEN RESTART GAME PANEL
    }
    void Finished() {//FINISHED
        Debug.Log("Finished");
        Invoke("createPortal", 0.5f);//CREATE PORTAL FOR AND GAME
        nextLevelImg.color = Color.yellow;//RIGHT LEVEL CIRCLE COLORED YELLOW
        isFinished = true;
        Invoke("addMass", 0.1f);
        
        
    }
    void addMass() {
        rb.mass = 4000;
        rb.AddForce(Vector3.down * 2000f);//AD FORCE X DRAG VALUE
    }
    void setFalse() {
        released = false;
    }
    void createPortal() {
        GameObject port = Instantiate(portal, new Vector3(0, gl.maxTowerLenght - 1f, -2.9f), Quaternion.identity) as GameObject;//CREATE PORTAL
        port.transform.eulerAngles = new Vector3(90, 0, 0);//SET PORTAL ROT
    }
    void createHole() {
        GameObject holee = Instantiate(hole, new Vector3(0, transform.position.y, -1.253f), Quaternion.identity) as GameObject;//CREATE HOLE
        holee.transform.eulerAngles = new Vector3(180, 0, 0);//SET HOLE ROT
    }
    
    IEnumerator createSparkle() {
        GameObject sparklee = Instantiate(sparkle, new Vector3(0, transform.position.y, -1.5f), Quaternion.identity) as GameObject;//CREATE SPARKLE
        sparklee.transform.eulerAngles = new Vector3(180, 0, 0);//SET SPARKLE ROT
        yield return new WaitForSeconds(.5f);//WAIT 0.5 SEC FOR DESTROY SPARK
        Destroy(sparklee);
    }
    IEnumerator pointsForDistance() { //ADD SOME POINTS FOR DISTANCE
        score += Convert.ToInt32(tempPos-startPos);
        yield return new WaitForSeconds(.2f);
        startPos = tempPos;
    }
   
    private void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.tag == "metalsurface") {//IF WE HIT METAL SURFACE

            StartCoroutine(createSparkle());//CREATE SPARKLE
            StartCoroutine(brokenRod());//BROKE ROD

        }
        if (coll.gameObject.tag == "dangersurface")//IF WE HIT DANGER SURFACE
        {
            gameOver();//GAMEOVER
        }
        if (coll.gameObject.tag == "break")//IF WE HIT BREABLEWALL
        {
            gameObject.GetComponent<SphereCollider>().isTrigger = true;
            auSo.clip = breakS;//CHANGE SOUND CLIP
            auSo.Play();//PLAY BREAK SOUND
        }
        if (coll.gameObject.tag == "finishline")//IF WE ACROSS FINISH
        {
            Finished();//FINISH
        }
        if (coll.gameObject.tag == "portal")//IF WE GO IN PORTAL
        {
            finishgamepanel.SetActive(true);//OPEN NEXT LEVEL PANEL
            auSo.clip = finishS;//CHANGE SOUND CLIP
            auSo.Play();//PLAY FINISH SOUND
            cam.transform.parent = null;//CAMERA NOT CHILDREN ANYMORE
            score += Convert.ToInt32(gl.maxTowerLenght);//ADD SCORE FOR END GAME
            level++;//INCREASE LEVEL
            if (score > bestscore) {
                PlayerPrefs.SetInt("bestscore", score);
                PlayerPrefs.Save();
            }
            PlayerPrefs.SetInt("level", level);//INCREASE LEVEL FOR PLAYERPREFS
            PlayerPrefs.Save();//SAVE PLAYERPREFS
        }
        if (coll.gameObject.tag == "gold") //IF WE HIT GOLDS
        {
            auSo.clip = goldLootS;//CHANGE SOUND CLIP
            auSo.Play();//PLAY GOLD SOUND
            Destroy(coll.gameObject);//DESTROY PREFABS
            score += 50;//ADD 50 GOLD 
        }
        if (coll.gameObject.tag == "100")//IF WE HIT 100 IN TARGET
        {
            isBoosted = true; //BALL IS BOOSTED NOW
            ballCyphere.GetComponent<TrailRenderer>().material = flame; //OUR TRAIL FLAMING NOW
            score += 100;//ADD SCORE
            Debug.Log("100");
        }
        if (coll.gameObject.tag == "75")//IF WE HIT 75 IN TARGET
        {
            score += 75; //ADD SCORE
            Debug.Log("75");
        }
        if (coll.gameObject.tag == "50")//IF WE HIT 50 IN TARGET
        {
            score += 50;//ADD SCORE
            Debug.Log("50");
        }
    }
    private void OnTriggerExit(Collider coll) {
        if (coll.gameObject.tag == "stand") //IF WE EXIT STAND TRIGGER OPEN FOR BOUNCE ON IT
        {
            coll.gameObject.GetComponent<MeshRenderer>().enabled = true; 
            coll.gameObject.GetComponent<BoxCollider>().enabled = true;

        }
        if (coll.gameObject.tag == "break") {
            gameObject.GetComponent<SphereCollider>().isTrigger = false;
        }
    }
    public void restartGame() { // RESTART GAME PANEL BUTTON
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void resetLevels()
    {
        PlayerPrefs.SetInt("level", 0);
        PlayerPrefs.Save();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void openSettings()
    {
        Time.timeScale = 0;
        settingsPanel.SetActive(true);


    }
    public void backToGame() {
        Time.timeScale = 1;
        settingsPanel.SetActive(false);
    }

}
