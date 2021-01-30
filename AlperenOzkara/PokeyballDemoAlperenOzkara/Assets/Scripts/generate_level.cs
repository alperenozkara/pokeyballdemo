using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class generate_level : MonoBehaviour
{
    //GAMEOBJECTS
    public GameObject[] obstacles;
    public GameObject mainTower;
    public GameObject finishLine;
    public GameObject targetPrefab,goldPrefab,breakPrefab,cloudPrefab;
    //NUMERICS
    public int maxObstacle;
    public float maxTowerLenght;
    public int level;
    //Color
    public Color[] colors;
    
    void Awake() {
        
        level = PlayerPrefs.GetInt("level") + 1; //GET CURRENT LEVEL BEFORE GENERATE MAP
    }
    void Start()
    {

        //FOR THE CREATING LEVELS
        //COLORS CAN BE CHANGED FROM EDITOR
        switch (level)
        {
            //case x: GenerateLevel(priority,id,id2,count,height,width,spacing,targetcount,breakable) GenarateTower(lenght,width)
            case 1: GenerateLevel(2, 0, 1, 10, 2f, 1.5f, 4f,3,0); GenerateTower(100f, 1f, colors[0]); return;
            case 2: GenerateLevel(1, 0, 1, 17, 2f, 1.25f, 6f, 3,1); GenerateTower(170f, 1f, colors[3]); return;
            case 3: GenerateLevel(4, 0, 1, 13, 2f, 1f, 4f, 3,3); GenerateTower(130f, 0.75f, colors[2]); return;
            case 4: GenerateLevel(3, 0, 1, 10, 2f, 1.5f, 5f, 3,1); GenerateTower(100f, 1f, colors[1]); return;
            case 5: GenerateLevel(5, 0, 1, 25, 2f, 1.25f, 5f, 4,4); GenerateTower(200f, 1f, colors[4]); return;
            case 6: GenerateLevel(6, 0, 1, 15, 2f, 1.5f, 6f, 3,1); GenerateTower(150f, 1f, colors[2]); return;
            case 7: GenerateLevel(3, 0, 1, 40, 1f, 2.25f, 3f, 3,3); GenerateTower(100f, 2f, colors[3]); return;
            case 8: GenerateLevel(5, 0, 1, 20, 2f, 1.5f, 5f, 3,4); GenerateTower(50f, 1f, colors[1]); return;
            case 9: GenerateLevel(6, 0, 1, 15, 2f, 1.5f, 6f, 3,1); GenerateTower(150f, 1f, colors[0]); return;
            case 10: GenerateLevel(3, 0, 1, 30, 2f, 1.5f, 5f, 3,3); GenerateTower(235f, 1f, colors[1]); return;
            case 11: GenerateLevel(5, 0, 1, 20, 2f, 2.25f, 5f, 3,0); GenerateTower(195f, 2f, colors[3]); return;
            case 12: GenerateLevel(7, 0, 1, 30, 2f, 1.5f, 3f, 1,0); GenerateTower(150f, 1f, colors[2]); return;
            case 13: GenerateLevel(3, 0, 1, 35, 2f, 2.25f, 3f, 3,0); GenerateTower(225f, 2f, colors[4]); return;
            case 14: GenerateLevel(5, 0, 1, 20, 2f, 1.5f, 5f, 2,2); GenerateTower(185f, 1f, colors[1]); return;
            case 15: GenerateLevel(6, 0, 1, 22, 5f, 2.25f, 3f, 3,3); GenerateTower(275f, 2f, colors[1]); return;
            case 16: GenerateLevel(3, 0, 1, 50, 2f, 1.5f, 5f, 3,4); GenerateTower(300f, 1f, colors[3]); return;
            case 17: GenerateLevel(9, 0, 1, 33, 2f, 2.25f, 5f, 4,2); GenerateTower(405f, 2f, colors[4]); return;
            case 18: GenerateLevel(4, 0, 1, 15, 2f, 0.75f, 3f, 3,4); GenerateTower(130f, 0.5f, colors[2]); return;
            case 19: GenerateLevel(3, 0, 1, 45, 2f, 1.5f, 2f, 3,2); GenerateTower(300f, 1f, colors[1]); return;
            case 20: GenerateLevel(5, 0, 1, 50, 1f, 1.5f, 5f, 2,1); GenerateTower(350f, 1f, colors[0]); return;
            case 21: GenerateLevel(6, 0, 1, 22, 5f, 2.25f, 3f, 3,7); GenerateTower(275f, 2f, colors[0]); return;
            case 22: GenerateLevel(3, 0, 1, 50, 2f, 1.5f, 5f, 3,0); GenerateTower(300f, 1f, colors[2]); return;
            case 23: GenerateLevel(9, 0, 1, 33, 2f, 2.25f, 5f, 4,3); GenerateTower(405f, 2f, colors[1]); return;
            case 24: GenerateLevel(3, 0, 1, 15, 2f, 1.5f, 3f, 3,2); GenerateTower(130f, 1f, colors[0]); return;
            case 25: GenerateLevel(3, 0, 1, 45, 2f, 0.75f, 2f, 3,5); GenerateTower(100f, 0.5f, colors[4]); return;
            case 26: GenerateLevel(5, 0, 1, 50, 1f, 1.5f, 5f, 2,3); GenerateTower(350f, 1f, colors[2]); return;
            case 27: GenerateLevel(3, 0, 1, 50, 0.5f, 1.5f, 5f, 2, 2); GenerateTower(300f, 1f, colors[0]); return;
            case 28: GenerateLevel(5, 0, 1, 20, 2f, 3.75f, 5f, 2, 2); GenerateTower(185f, 4f, colors[1]); return;
            case 29: GenerateLevel(8, 0, 1, 22, 5f, 2.25f, 3f, 3, 3); GenerateTower(275f, 2f, colors[1]); return;
            case 30: GenerateLevel(2, 0, 1, 50, 2f, 1.5f, 5f, 3, 4); GenerateTower(300f, 1f, colors[3]); return;
            case 31: GenerateLevel(3, 0, 1, 33, 2f, 2.25f, 5f, 4, 2); GenerateTower(405f, 2f, colors[4]); return;
            case 32: GenerateLevel(4, 0, 1, 15, 2f, 1.5f, 3f, 3, 4); GenerateTower(130f, 1f, colors[2]); return;
            case 33: GenerateLevel(3, 0, 1, 45, 2f, 1.5f, 2f, 3, 2); GenerateTower(300f, 1f, colors[1]); return;
            case 34: GenerateLevel(5, 0, 1, 50, 1f, 1.25f, 5f, 2, 4); GenerateTower(350f, 0.5f, colors[0]); return;
            case 35: GenerateLevel(6, 0, 1, 22, 5f, 2.25f, 3f, 3, 0); GenerateTower(275f, 2f, colors[0]); return;
            case 36: GenerateLevel(11, 0, 1, 50, 2f, 1.5f, 5f, 3, 3); GenerateTower(300f, 1f, colors[2]); return;
            case 37: GenerateLevel(9, 0, 1, 46, 2f, 2.25f, 5f, 4, 3); GenerateTower(405f, 2f, colors[1]); return;
            case 38: GenerateLevel(1, 0, 1, 15, 2f, 1.5f, 3f, 3, 2); GenerateTower(130f, 1f, colors[0]); return;
            case 39: GenerateLevel(3, 0, 1, 45, 2f, 0.75f, 2f, 3, 1); GenerateTower(300f, 0.5f, colors[4]); return;
            case 40: GenerateLevel(5, 0, 1, 50, 1f, 1.5f, 5f, 2, 3); GenerateTower(350f, 1f, colors[2]); return;
            case 41: GenerateLevel(3, 0, 1, 33, 2f, 2.25f, 5f, 4, 2); GenerateTower(405f, 2f, colors[4]); return;
            case 42: GenerateLevel(2, 0, 1, 15, 2f, 1.5f, 3f, 3, 4); GenerateTower(130f, 1f, colors[2]); return;
            case 43: GenerateLevel(3, 0, 1, 45, 2f, 1.25f, 2f, 1, 2); GenerateTower(100f, 0.5f, colors[1]); return;
            case 44: GenerateLevel(5, 0, 1, 50, 1f, 1.5f, 5f, 2, 4); GenerateTower(300f, 1f, colors[0]); return;
            case 45: GenerateLevel(6, 0, 1, 22, 5f, 2.25f, 3f, 3, 0); GenerateTower(275f, 2f, colors[0]); return;
            case 46: GenerateLevel(7, 0, 1, 50, 2f, 1.5f, 5f, 2, 2); GenerateTower(300f, 1f, colors[2]); return;
            case 47: GenerateLevel(5, 0, 1, 46, 2f, 2.25f, 5f, 2, 3); GenerateTower(405f, 2f, colors[1]); return;
            case 48: GenerateLevel(1, 0, 1, 15, 1f, 1.5f, 3f, 3, 1); GenerateTower(130f, 0.75f, colors[0]); return;
            case 49: GenerateLevel(3, 0, 1, 30, 2f, 0.75f, 2f, 3, 1); GenerateTower(300f, 0.5f, colors[4]); return;
            case 50: GenerateLevel(2, 0, 1, 50, 1f, 1.5f, 5f, 2, 3); GenerateTower(350f, 1f, colors[2]); return;

        }
    }
    void Update()
    {

    }
   
    void GenerateTower(float towerlenght, float towerwidth, Color color) {
        GameObject tower = Instantiate(mainTower, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;//SPAWN TOWER
        tower.transform.localScale = new Vector3(towerwidth, towerlenght, 2.5f);//SET TOWER LENGHT
        tower.GetComponentInChildren<MeshRenderer>().material.color = color;//CHANGE TOWER COLOR
        maxTowerLenght = towerlenght;
        
        GameObject finish = Instantiate(finishLine, new Vector3(0, maxTowerLenght, -1.74f), Quaternion.identity) as GameObject;//SPAWN FINISH LINE TOP POINT OF TOWER
        finish.transform.eulerAngles = new Vector3(0, 90, 0);//SET ROTATION FOR FINISHLINE
    }
    void GenerateLevel(int priority,int id,int id2,int count,float height,float width,float spacing,int targetCount,int breakable)
    {
        
        float y = 5;
        float bStartPoint = 10;
        float spYpoint = (maxTowerLenght / priority);

        for (int c = 0; c < Random.Range(10, 20); c++) {
            GameObject cloud = Instantiate(cloudPrefab, new Vector3(Random.Range(-32.4f,1f), Random.Range(0,maxTowerLenght+30f), Random.Range(43,55)), Quaternion.identity) as GameObject;
        }
        if (breakable > 0) {//SPAWN BRICKS
            for (int b = 0; b < breakable; b++) {
                bStartPoint += spacing *priority;//INCREASE SPACING
                GameObject breakO = Instantiate(breakPrefab, new Vector3(0, bStartPoint, -3), Quaternion.identity) as GameObject;//SPAWN BRICKS
            }
        }
        //SPAWN COLLECTABLE GOLDS
        for (int g2 = 0; g2 < 2; g2++)
        {
            for (int g = 0; g < (count / 3); g++)
            {
                
                spYpoint += 1.5f;//INCREASE SPACING
                GameObject goold = Instantiate(goldPrefab, new Vector3(0, spYpoint, -2.86f), Quaternion.identity) as GameObject;//SPAWN GOLDS
            }
            spYpoint *= priority / 3f; //INCREASE SPACING AGAIN

        }
        //SPAWN OBSTACLES
        for (int i = 0; i < count; i++)
            {
            //SPAWN DANGER OR METAL SURFACE
            if (i % priority == 0)
            {
                
                spacing += (priority/2); //INCREASE SPACING
                id = 1; //SWITCH OBJECT TO DANGER SURFACE
            }
            else {
                id = 0; //SWITCH OBJECT TO METAL SURFACE
            }
            if (i % id2 == 2)
            {
                id = 2; //SWITCH OBJECT TO EMPTY SURFACE
            }

            y += spacing;
            GameObject obs = Instantiate(obstacles[id], new Vector3(0, y, 0), Quaternion.identity) as GameObject; //SPAWN OBSTACLES
            obs.transform.localScale = new Vector3(width, Random.Range(height-1f,height+1f), 3); //SET SCALE OBSTACLE
            obs.name = obs.name + i; //SET NAME

            //SPAWN TARGETS
            if (i % (targetCount*2) == 0)
            {
                GameObject target = Instantiate(targetPrefab, new Vector3(0, y + (spacing / 2), -1.3f), Quaternion.identity) as GameObject;//SPAWN TARGET    
                target.name = target.name + i;//SET NAME
                target.transform.eulerAngles = new Vector3(0, 180, 0);//SET TARGET ROTATION
            }
        }
        
    }
    
    
}
