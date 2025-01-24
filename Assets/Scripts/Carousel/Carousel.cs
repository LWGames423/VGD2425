using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Carousel : MonoBehaviour
{
    public InputAction carouselAction;
    public InputAction carouselSet;
    
    private float _actionInput;
    private float _setInput;

    [Header("Parts Setup")]
    public List<CarouselEntry> masterList = new List<CarouselEntry>();
    public List<CarouselEntry> entries = new List<CarouselEntry>();
    public int firstLevelIndex;

    [Space] [SerializeField] private ScrollRect scrollRect;

    [Space] [SerializeField] private RectTransform contentBoxHorizontal;
    [SerializeField] private Image carouselEntryPrefab;
    private List<Image> _imagesForEntries = new List<Image>();

    [Header("Animation Setup")] [SerializeField, Range(0.25f, 1f)]
    private float duration = 0.5f;

    [SerializeField] private AnimationCurve easeCurve;

    [Header("Info Setup")] [SerializeField]
    private CarouselTextBox textBoxController;

    public int currentIndex = 0;
    private Coroutine _scrollCoroutine;

    private void OnEnable()
    {
        carouselAction.Enable();
        carouselSet.Enable();
    }

    private void OnDisable()
    {
        carouselAction.Disable();
        carouselSet.Disable();
    }

    private void Reset()
    {
        scrollRect = GetComponentInChildren<ScrollRect>();
        textBoxController = GetComponentInChildren<CarouselTextBox>();
    }


    private void Start()
    {
        entries = CreateEntriesList();
        CopyImagesForEntries();
        var headline = entries[0].CharName;
        var description = entries[0].Description;
        textBoxController.SetTextWithoutFade(headline, description);
    }

    private void Update()
    {
        if (entries.Count <= 1)
        {
            _actionInput = 0;
        }
        else
        {
            _actionInput = carouselAction.ReadValue<float>();
        }
        _setInput = carouselSet.ReadValue<float>();
        if (_actionInput == 0) return;
        if (_actionInput > 0 && carouselAction.WasPressedThisFrame())
        {
            ScrollToNext();
        }

        if (_actionInput < 0 && carouselAction.WasPressedThisFrame())
        {
            ScrollToPrevious();
        }
    }

    private List<CarouselEntry> CreateEntriesList()
    {
        List<CarouselEntry> newEntries = new();
        int currentLevel = (SceneManager.GetActiveScene().buildIndex - firstLevelIndex) + 1;
        switch (currentLevel)
        {
            case 1:
                newEntries.Add(masterList[0]);
                newEntries.Add(masterList[1]);
                break;
            case 2:
            case 3:
                for (int i = 0; i < 2*(currentLevel-1)+1; i++)
                {
                    newEntries.Add(masterList[i]);
                }
                break;
            default:
                newEntries = masterList;
                break;
        }

        return newEntries;
    }

    public void AddEntries(CarouselEntry newEntry)
    {
        entries.Add(newEntry);
        CopyImagesForEntries();
    }

    private void CopyImagesForEntries()
    {
        _imagesForEntries = new();
        foreach (var entry in entries)
        {
            Image carouselEntry = Instantiate(carouselEntryPrefab, contentBoxHorizontal);
            carouselEntry.sprite = entry.EntryGraphic;
            _imagesForEntries.Add(carouselEntry);
        }
    }

    public void ScrollToNext()
    {
        currentIndex = (currentIndex + 1) % _imagesForEntries.Count;
        ScrollTo(currentIndex);
    }

    public void ScrollToPrevious()
    {
        currentIndex = (currentIndex - 1 + _imagesForEntries.Count) % _imagesForEntries.Count;
        ScrollTo(currentIndex);
    }

    private void ScrollTo(int index)
    {
        currentIndex = index;
        float targetHorizontalPosition = (float)currentIndex / (_imagesForEntries.Count - 1);

        if (_scrollCoroutine != null)
            StopCoroutine(_scrollCoroutine);
            
        _scrollCoroutine = StartCoroutine(LerpToPos(targetHorizontalPosition));

        var headline = entries[currentIndex].CharName;
        var description = entries[currentIndex].Description;

        textBoxController.SetText(headline, description, duration);
    }

    private IEnumerator LerpToPos(float targetHorizontalPosition)
    {
        float elapsedTime = 0f;
        float initialPos = scrollRect.horizontalNormalizedPosition;

        if (duration > 0)
        {
            while (elapsedTime <= duration)
            {
                float easeValue = easeCurve.Evaluate(elapsedTime / duration);

                float newPosition = Mathf.Lerp(initialPos, targetHorizontalPosition, easeValue);

                scrollRect.horizontalNormalizedPosition = newPosition;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        scrollRect.horizontalNormalizedPosition = targetHorizontalPosition;
    }
}