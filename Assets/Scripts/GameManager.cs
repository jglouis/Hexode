using UnityEngine;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;

//This script will be the master script concerning the game rules
public class GameManager : MonoBehaviour {
  
  //singleton
  private static GameManager instance;
  
  //get the singleton
  public static GameManager Instance {
    get {
      if (instance == null) {
        // This is where the magic happens.
        //  FindObjectOfType(...) returns the first GameMaster object in the scene.
        instance =  FindObjectOfType(typeof (GameManager)) as GameManager;
      }
      
      // If it is still null, create a new instance
      if (instance == null) {
        GameObject obj = new GameObject("Game Master");
        obj.AddComponent(typeof(NetworkView));              
        instance = obj.AddComponent(typeof (GameManager)) as GameManager;
        obj.networkView.observed = obj.GetComponent<GameManager>();
        Debug.Log ("Could not locate an GameManager object. Singleton created on fly.");
      }
      
      return instance;
    }
  }
}
