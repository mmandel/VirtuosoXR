using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLocation : MonoBehaviour
{
    private SphereCollider sphereCollider;
   private EffectDrumPad effectDrumPad;
    public GameObject touching;
    public bool inButtonRange;
    [Tooltip("Which drum am I in the authoring?")]
    public int drumIdx = 0;

   [Header("Debug")]
   public bool EnableDebugPlayerSwipeKey = true;
   public KeyCode DebugPlayerSwipeKey = KeyCode.Space;


   private void Start()
   {
      effectDrumPad = this.gameObject.GetComponent<EffectDrumPad>();
   }

   /// <summary>
   /// Check if anything enters
   /// </summary>
   /// <param name="other"></param>
   void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Collider>().GetType() == typeof(BoxCollider));
        {
            if (other.gameObject.tag == "Hand")
            {
                inButtonRange = true;
                touching = other.gameObject;

                HandSwiped();
            }
        }
    }

    /// <summary>
    /// Called when person is touching a button
    /// </summary>
    /// <param name="touching">The game object we’re near</param>
    public void HandSwiped()
    {
      //ignore when in the place drums state
      if (ProgressionMgr.I && (ProgressionMgr.I.GetCurState() == ProgressionState.PlaceDrums))
         return;

      // On hit
      if (RhythmGameplayMgr.I && RhythmGameplayMgr.I.TriggerPlayerSwing(drumIdx))
      {
         effectDrumPad.EmitHit();
      }

      // On miss!
      else
      {
         effectDrumPad.EmitMiss();
      }
    }

    /// <summary>
    /// Check if anything exits
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            inButtonRange = false;
            touching = null;

        }
    }


    public float timeNearButton = 0f;
    bool timing = false;

    /// <summary>
    /// See how long the user has been triggering the button
    /// </summary>
    /// <returns></returns>
    IEnumerable NearButtonTimer ()
    {
        timeNearButton = 0f;

        while (timing)
        {
            timeNearButton += Time.deltaTime;
        }

        yield return null;
    }


    private void Update()
    {
      if (EnableDebugPlayerSwipeKey && Input.GetKeyDown(DebugPlayerSwipeKey))
      {
         HandSwiped();
      }
   }
}

