using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Transform ChestPoint;
    bool hasLevelUp;
    public List<WallParent> WallParents = new List<WallParent>();

    private void Awake()
    {
        Instance = this;
        if (!PlayerPrefs.HasKey("Level"))
            PlayerPrefs.SetInt("Level", 1);

        if (!PlayerPrefs.HasKey("CollectedChest"))
            PlayerPrefs.SetInt("CollectedChest", 0);


        hasLevelUp = false;

    }
    private void Start()
    {
        PrepareScene();
    }
    void PrepareScene()
    {
        if (PlayerPrefs.GetInt("CollectedChest") == 0)
            ObjectPool.Instance.SpawnObject(ObjectTypes.Chest, ChestPoint.position);

        foreach (WallParent item in WallParents)
        {
            item.SetChildWalls((GetLevel() * 9), (GetLevel() * 2));
        }
    }
    public int GetLevel() { return PlayerPrefs.GetInt("Level"); }

    public void LevelUp()
    {
        if (hasLevelUp)
            return;


        hasLevelUp = true;
        PlayerPrefs.SetInt("Level", GetLevel() + 1);
        PlayerPrefs.SetInt("CollectedChest", 0);

    }


}
