using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using System.Collections;


public class CarouselTextBox : MonoBehaviour
{
    [FormerlySerializedAs("headline")] [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text description;
            
    [SerializeField] private bool fadeText = true;
            
    private float _fadeDuration = 0.5f;
    private float _halfFadeDuration => _fadeDuration * 0.5f;
            
    private Coroutine _fadeCoroutine;
            
    public void SetTextWithoutFade(string headlineText, string descriptionText)
    {
        characterName.SetText(headlineText);
        description.SetText(descriptionText);
                
        characterName.alpha = 1;
        description.alpha = 1;
    }
            
    public void SetText(string headlineText, string descriptionText, float fadingDuration = 0f)
    {
        if (!fadeText || fadingDuration <= 0)
        {
            SetTextWithoutFade(headlineText, descriptionText);
            return;
        }
                
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
                    
            characterName.alpha = 1;
            description.alpha = 1;
        }
                
        _fadeDuration = fadingDuration;
        _fadeCoroutine = StartCoroutine(FadeText(headlineText, descriptionText));
    }

    private IEnumerator FadeText(string headlineText, string descriptionText)
    {
        float time = 0;
        while (time < _halfFadeDuration)
        {
            time += Time.deltaTime;
            float lerpValue = 1 - (time / _halfFadeDuration);
            characterName.alpha = lerpValue;
            description.alpha = lerpValue;
            yield return null;
        }

        characterName.SetText(headlineText);
        description.SetText(descriptionText);
        time = 0;
        while (time < _halfFadeDuration)
        {
            time += Time.deltaTime;
            float lerpValue = time / _halfFadeDuration;
            characterName.alpha = lerpValue;
            description.alpha = lerpValue;
            yield return null;
        }

        characterName.alpha = 1;
        description.alpha = 1;

    }
}
