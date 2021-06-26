using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    ColorGrading colorGrading;
    public PostProcessVolume volume;
    
    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGetSettings(out colorGrading);
        DarkWorld();
    }

    public void setup(bool cor, int RR, int RG, int RB, int GR, int GG, int GB, int BR, int BG, int BB){
        
        if(cor){
            colorGrading.mixerRedOutRedIn.value += RR;
            colorGrading.mixerRedOutGreenIn.value += RG;
            colorGrading.mixerRedOutBlueIn.value += RB;

            colorGrading.mixerGreenOutRedIn.value += GR;
            colorGrading.mixerGreenOutGreenIn.value += GG;
            colorGrading.mixerGreenOutBlueIn.value += GB;

            colorGrading.mixerBlueOutRedIn.value += BR;
            colorGrading.mixerBlueOutGreenIn.value += BG;
            colorGrading.mixerBlueOutBlueIn.value += BB;
        }else{
            colorGrading.mixerRedOutRedIn.value = RR;
            colorGrading.mixerRedOutGreenIn.value = RG;
            colorGrading.mixerRedOutBlueIn.value = RB;

            colorGrading.mixerGreenOutRedIn.value = GR;
            colorGrading.mixerGreenOutGreenIn.value = GG;
            colorGrading.mixerGreenOutBlueIn.value = GB;

            colorGrading.mixerBlueOutRedIn.value = BR;
            colorGrading.mixerBlueOutGreenIn.value = BG;
            colorGrading.mixerBlueOutBlueIn.value = BB;
        
        //_bloom.intensity.value = 100;
    }
    }
    public void DarkWorld(){
        setup(false, 10 ,10 ,10 ,10 ,10 ,10 ,10 ,10 ,10);
    }

}
