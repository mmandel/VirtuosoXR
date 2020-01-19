using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EffectDrumPad : MonoBehaviour
{


    public Animator OnBeatAnimator;
    public string OnBeatTrigger = "";

    public bool burst = false; // For debuggingg

    private void Update()
    {
        if (burst)
        {
            EmitHit();
        }
    }

    /// <summary>
    /// Burst the particles
    /// </summary>
    /// <param name="index"></param>
    public void EmitHit()
    {
        ParticleSystem ps = this.gameObject.GetComponentInChildren<ParticleSystem>();

        ParticleSystem.EmitParams emitOverride = new ParticleSystem.EmitParams();
        emitOverride.startLifetime = 0.5f;
        ps.Emit(emitOverride, 500);

        ScaleHit();
        EmitFXChain();
    }

   /// <summary>
   /// Scale the drum
   /// </summary>
   public void ScaleHit()
   {
      OnBeatAnimator.SetTrigger("Hit");
   }

   public void EmitMiss()
   {
       ScaleMiss();
   }

   /// <summary>
   /// Scale the drum
   /// </summary>
   public void ScaleMiss()
   {
      OnBeatAnimator.SetTrigger("Miss");
   }

   public ParticleSystem[] particleSystems;

    /// <summary>
    /// Trigger all particle systems
    /// </summary>
    public void EmitFXChain()
    {
        for (int i = 0; i < particleSystems.Length; i++) { 

            ParticleSystem ps = particleSystems[i];

            ParticleSystem.EmitParams emitOverride = new ParticleSystem.EmitParams();
            emitOverride.startLifetime = 0.5f;
            ps.Emit(emitOverride, 500);
        }
    }

    public void Start()
    {
        particleSystems = this.gameObject.GetComponentsInChildren<ParticleSystem>();
    }
}
